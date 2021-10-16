﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Extensions
{
    public static class Linq
    {
        //public static IEnumerable<TResult> FullOuterGroupJoin<TA, TB, TKey, TResult>(
        //this IEnumerable<TA> a,
        //IEnumerable<TB> b,
        //Func<TA, TKey> selectKeyA,
        //Func<TB, TKey> selectKeyB,
        //Func<IEnumerable<TA>, IEnumerable<TB>, TKey, TResult> projection,
        //IEqualityComparer<TKey> cmp = null)
        //{
        //    cmp = cmp ?? EqualityComparer<TKey>.Default;
        //    var alookup = a.ToLookup(selectKeyA, cmp);
        //    var blookup = b.ToLookup(selectKeyB, cmp);

        //    var keys = new HashSet<TKey>(alookup.Select(p => p.Key), cmp);
        //    keys.UnionWith(blookup.Select(p => p.Key));

        //    var join = from key in keys
        //               let xa = alookup[key]
        //               let xb = blookup[key]
        //               select projection(xa, xb, key);

        //    return join;
        //}

        //public static IEnumerable<TResult> FullOuterJoin<TA, TB, TKey, TResult>(
        //    this IEnumerable<TA> a,
        //    IEnumerable<TB> b,
        //    Func<TA, TKey> selectKeyA,
        //    Func<TB, TKey> selectKeyB,
        //    Func<TA, TB, TKey, TResult> projection,
        //    TA defaultA = default(TA),
        //    TB defaultB = default(TB),
        //    IEqualityComparer<TKey> cmp = null)
        //{
        //    cmp = cmp ?? EqualityComparer<TKey>.Default;
        //    var alookup = a.ToLookup(selectKeyA, cmp);
        //    var blookup = b.ToLookup(selectKeyB, cmp);

        //    var keys = new HashSet<TKey>(alookup.Select(p => p.Key), cmp);
        //    keys.UnionWith(blookup.Select(p => p.Key));

        //    var join = from key in keys
        //               from xa in alookup[key].DefaultIfEmpty(defaultA)
        //               from xb in blookup[key].DefaultIfEmpty(defaultB)
        //               select projection(xa, xb, key);

        //    return join;
        //}

        public static IEnumerable<TResult> LeftOuterJoin<TLeft, TRight, TKey, TResult>(
            this IEnumerable<TLeft> leftItems,
            IEnumerable<TRight> rightItems,
            Func<TLeft, TKey> leftKeySelector,
            Func<TRight, TKey> rightKeySelector,
            Func<TLeft, TRight, TResult> resultSelector)
        {
            return from left in leftItems
                   join right in rightItems on leftKeySelector(left) equals rightKeySelector(right) into temp
                   from right in temp.DefaultIfEmpty()
                   select resultSelector(left, right);
        }

        public static IEnumerable<TResult> RightOuterJoin<TLeft, TRight, TKey, TResult>(
            this IEnumerable<TLeft> leftItems,
            IEnumerable<TRight> rightItems,
            Func<TLeft, TKey> leftKeySelector,
            Func<TRight, TKey> rightKeySelector,
            Func<TLeft, TRight, TResult> resultSelector)
        {
            return from right in rightItems
                   join left in leftItems on rightKeySelector(right) equals leftKeySelector(left) into temp
                   from left in temp.DefaultIfEmpty()
                   select resultSelector(left, right);
        }

        public static IEnumerable<TResult> FullOuterJoinDistinct<TLeft, TRight, TKey, TResult>(
            this IEnumerable<TLeft> leftItems,
            IEnumerable<TRight> rightItems,
            Func<TLeft, TKey> leftKeySelector,
            Func<TRight, TKey> rightKeySelector,
            Func<TLeft, TRight, TResult> resultSelector)
        {
            return leftItems.LeftOuterJoin(rightItems, leftKeySelector, rightKeySelector, resultSelector).Union(leftItems.RightOuterJoin(rightItems, leftKeySelector, rightKeySelector, resultSelector));
        }

        public static IEnumerable<TResult> RightAntiSemiJoin<TLeft, TRight, TKey, TResult>(
            this IEnumerable<TLeft> leftItems,
            IEnumerable<TRight> rightItems,
            Func<TLeft, TKey> leftKeySelector,
            Func<TRight, TKey> rightKeySelector,
            Func<TLeft, TRight, TResult> resultSelector) where TLeft : class
        {
            var hashLK = new HashSet<TKey>(from l in leftItems select leftKeySelector(l));
            return rightItems.Where(r => !hashLK.Contains(rightKeySelector(r))).Select(r => resultSelector((TLeft)null, r));
        }

        public static IEnumerable<TResult> FullOuterJoin<TLeft, TRight, TKey, TResult>(
            this IEnumerable<TLeft> leftItems,
            IEnumerable<TRight> rightItems,
            Func<TLeft, TKey> leftKeySelector,
            Func<TRight, TKey> rightKeySelector,
            Func<TLeft, TRight, TResult> resultSelector) where TLeft : class
        {
            return leftItems.LeftOuterJoin(rightItems, leftKeySelector, rightKeySelector, resultSelector).Concat(leftItems.RightAntiSemiJoin(rightItems, leftKeySelector, rightKeySelector, resultSelector));
        }

        private static Expression<Func<TP, TC, TResult>> CastSMBody<TP, TC, TResult>(LambdaExpression ex, TP unusedP, TC unusedC, TResult unusedRes) => (Expression<Func<TP, TC, TResult>>)ex;

        public static IQueryable<TResult> LeftOuterJoin<TLeft, TRight, TKey, TResult>(
            this IQueryable<TLeft> leftItems,
            IQueryable<TRight> rightItems,
            Expression<Func<TLeft, TKey>> leftKeySelector,
            Expression<Func<TRight, TKey>> rightKeySelector,
            Expression<Func<TLeft, TRight, TResult>> resultSelector) where TLeft : class where TRight : class where TResult : class
        {
            var sampleAnonLR = new { left = (TLeft)null, rightg = (IEnumerable<TRight>)null };
            var parmP = Expression.Parameter(sampleAnonLR.GetType(), "p");
            var parmC = Expression.Parameter(typeof(TRight), "c");
            var argLeft = Expression.PropertyOrField(parmP, "left");
            var newleftrs = CastSMBody(Expression.Lambda(Expression.Invoke(resultSelector, argLeft, parmC), new[] { parmP, parmC }), sampleAnonLR, (TRight)null, (TResult)null);

            return leftItems.AsQueryable().GroupJoin(rightItems, leftKeySelector, rightKeySelector, (left, rightg) => new { left, rightg }).SelectMany(r => r.rightg.DefaultIfEmpty(), newleftrs);
        }

        public static IQueryable<TResult> RightOuterJoin<TLeft, TRight, TKey, TResult>(
            this IQueryable<TLeft> leftItems,
            IQueryable<TRight> rightItems,
            Expression<Func<TLeft, TKey>> leftKeySelector,
            Expression<Func<TRight, TKey>> rightKeySelector,
            Expression<Func<TLeft, TRight, TResult>> resultSelector) where TLeft : class where TRight : class where TResult : class
        {
            var sampleAnonLR = new { leftg = (IEnumerable<TLeft>)null, right = (TRight)null };
            var parmP = Expression.Parameter(sampleAnonLR.GetType(), "p");
            var parmC = Expression.Parameter(typeof(TLeft), "c");
            var argRight = Expression.PropertyOrField(parmP, "right");
            var newrightrs = CastSMBody(Expression.Lambda(Expression.Invoke(resultSelector, parmC, argRight), new[] { parmP, parmC }), sampleAnonLR, (TLeft)null, (TResult)null);

            return rightItems.GroupJoin(leftItems, rightKeySelector, leftKeySelector, (right, leftg) => new { leftg, right }).SelectMany(l => l.leftg.DefaultIfEmpty(), newrightrs);
        }

        public static IQueryable<TResult> FullOuterJoinDistinct<TLeft, TRight, TKey, TResult>(
            this IQueryable<TLeft> leftItems,
            IQueryable<TRight> rightItems,
            Expression<Func<TLeft, TKey>> leftKeySelector,
            Expression<Func<TRight, TKey>> rightKeySelector,
            Expression<Func<TLeft, TRight, TResult>> resultSelector) where TLeft : class where TRight : class where TResult : class
        {
            return leftItems.LeftOuterJoin(rightItems, leftKeySelector, rightKeySelector, resultSelector).Union(leftItems.RightOuterJoin(rightItems, leftKeySelector, rightKeySelector, resultSelector));
        }

        private static Expression<Func<TP, TResult>> CastSBody<TP, TResult>(LambdaExpression ex, TP unusedP, TResult unusedRes) => (Expression<Func<TP, TResult>>)ex;

        public static IQueryable<TResult> RightAntiSemiJoin<TLeft, TRight, TKey, TResult>(
            this IQueryable<TLeft> leftItems,
            IQueryable<TRight> rightItems,
            Expression<Func<TLeft, TKey>> leftKeySelector,
            Expression<Func<TRight, TKey>> rightKeySelector,
            Expression<Func<TLeft, TRight, TResult>> resultSelector) where TLeft : class where TRight : class where TResult : class
        {
            var sampleAnonLgR = new { leftg = (IEnumerable<TLeft>)null, right = (TRight)null };
            var parmLgR = Expression.Parameter(sampleAnonLgR.GetType(), "lgr");
            var argLeft = Expression.Constant(null, typeof(TLeft));
            var argRight = Expression.PropertyOrField(parmLgR, "right");
            var newrightrs = CastSBody(Expression.Lambda(Expression.Invoke(resultSelector, argLeft, argRight), new[] { parmLgR }), sampleAnonLgR, (TResult)null);

            return rightItems.GroupJoin(leftItems, rightKeySelector, leftKeySelector, (right, leftg) => new { leftg, right }).Where(lgr => !lgr.leftg.Any()).Select(newrightrs);
        }

        public static IQueryable<TResult> FullOuterJoin<TLeft, TRight, TKey, TResult>(
            this IQueryable<TLeft> leftItems,
            IQueryable<TRight> rightItems,
            Expression<Func<TLeft, TKey>> leftKeySelector,
            Expression<Func<TRight, TKey>> rightKeySelector,
            Expression<Func<TLeft, TRight, TResult>> resultSelector) where TLeft : class where TRight : class where TResult : class
        {
            return leftItems.LeftOuterJoin(rightItems, leftKeySelector, rightKeySelector, resultSelector).Concat(leftItems.RightAntiSemiJoin(rightItems, leftKeySelector, rightKeySelector, resultSelector));
        }
    }
}