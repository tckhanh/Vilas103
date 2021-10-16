using Data.DataModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Data.Infrastructure
{
    public static class RepositoryBase<T> where T : class
    {
        #region Properties
        private static readonly object mylock = new object();
        private static CFContext dbContext = null;
        private static IDbSet<T> dbSet = DbContext.Set<T>();

        public static CFContext DbContext
        {
            //get { return dbContext ?? (dbContext = new DbFactory().Init()); }
            get
            {
                if (dbContext == null)
                {
                    lock (mylock)
                    {
                        if (dbContext == null)
                        {
                            dbContext = new DbFactory().Init();
                        }
                    }
                }
                return dbContext;
            }
        }

        #endregion


        #region Implementation
        public static T Add(T entity)
        {
            return dbSet.Add(entity);
        }

        public static void Update(T entity)
        {
            dbSet.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
        }

        public static T Delete(T entity)
        {
            return dbSet.Remove(entity);
        }
        public static T Delete(int id)
        {
            var entity = dbSet.Find(id);
            return dbSet.Remove(entity);
        }

        public static T Delete(string id)
        {
            var entity = dbSet.Find(id);
            return dbSet.Remove(entity);
        }
        public static void DeleteMulti(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbSet.Remove(obj);
        }

        public static T GetSingleById(int id)
        {
            return dbSet.Find(id);
        }

        public static T GetSingleById(string id)
        {
            return dbSet.Find(id);
        }

        public static IEnumerable<T> GetMany(Expression<Func<T, bool>> where, string includes)
        {
            return dbSet.Where(where).ToList();
        }


        public static int Count(Expression<Func<T, bool>> where)
        {
            return dbSet.Count(where);
        }

        public static bool Exists(Expression<Func<T, bool>> where)
        {
            if (dbSet.Count(where) > 0)
                return true;
            else
                return false;
        }


        public static IEnumerable<T> GetAll(string[] includes = null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = dbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.AsQueryable();
            }

            return dbContext.Set<T>().AsQueryable();
        }

        public static T GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            if (includes != null && includes.Count() > 0)
            {
                var query = dbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.FirstOrDefault(expression);
            }
            return dbContext.Set<T>().FirstOrDefault(expression);
        }

        public static IEnumerable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = dbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.Where<T>(predicate).AsQueryable<T>();
            }

            return dbContext.Set<T>().Where<T>(predicate).AsQueryable<T>();
        }

        public static IEnumerable<T> GetMultiPaging(Expression<Func<T, bool>> predicate, out int total, string[] includes = null, int index = 0, int size = 20)
        {
            int skipCount = index * size;
            IQueryable<T> _resetSet;

            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = dbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                _resetSet = predicate != null ? query.Where<T>(predicate).AsQueryable() : query.AsQueryable();
            }
            else
            {
                _resetSet = predicate != null ? dbContext.Set<T>().Where<T>(predicate).AsQueryable() : dbContext.Set<T>().AsQueryable();
            }

            _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return _resetSet.AsQueryable();
        }

        public static bool CheckContains(Expression<Func<T, bool>> predicate)
        {
            return dbContext.Set<T>().Count<T>(predicate) > 0;
        }

        public static void SaveChanges()
        {
            dbContext.SaveChanges();
        }
        #endregion
    }
}
