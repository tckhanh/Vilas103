using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace Data.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Add<T>(this IEnumerable<T> e, T value)
        {
            foreach (var cur in e)
            {
                yield return cur;
            }
            yield return value;
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> source, string tableName = null)
        {
            if (source == null) throw new ArgumentNullException("source");

            var properties = TypeDescriptor.GetProperties(typeof(T))
                .Cast<PropertyDescriptor>()
                .ToList();

            var result = new DataTable(tableName);
            result.BeginInit();

            foreach (var prop in properties)
            {
                //result.Columns.Add(prop.Name, prop.PropertyType);
                result.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            result.EndInit();
            result.BeginLoadData();

            foreach (T item in source)
            {
                object[] values = properties.Select(p => p.GetValue(item)).ToArray();
                result.Rows.Add(values);
            }

            result.EndLoadData();

            return result;
        }

        public static DataSet ToDataSet<T>(this IEnumerable<T> source, string dataSetName = null, string tableName = null)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (string.IsNullOrEmpty(dataSetName)) dataSetName = "NewDataSet";

            var result = new DataSet(dataSetName);
            result.Tables.Add(source.ToDataTable(tableName));
            return result;
        }
        public static DataTable ToDataTable_Old<T>(this IEnumerable<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                tb.Columns.Add(prop.Name, prop.PropertyType);
            }

            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }

        public static DataTable ToDataTable<T>(this List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                tb.Columns.Add(prop.Name, prop.PropertyType);
            }

            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }

        public static IEnumerable<object> GetValues<T>(this IEnumerable<T> items, string propertyName)
        {
            List<Object> result = new List<object>();
            Type type = typeof(T);
            var prop = type.GetProperty(propertyName);
            foreach (var item in items)
                result.Add(prop.GetValue(item, null));
            return result;
        }

        public static DataTable ToPivotTable<T, TColumn, TRow, TData>(
    this IEnumerable<T> source,
    Func<T, TColumn> columnSelector,
    Expression<Func<T, TRow>> rowSelector,
    Func<IEnumerable<T>, TData> dataSelector)
        {
            DataTable table = new DataTable();
            var rowName = ((MemberExpression)rowSelector.Body).Member.Name;
            table.Columns.Add(new DataColumn(rowName));
            var columns = source.Select(columnSelector).Distinct().OrderBy(x=> x.ToString());

            foreach (var column in columns)
                table.Columns.Add(new DataColumn(column.ToString()));

            var rows = source.GroupBy(rowSelector.Compile())
                             .Select(rowGroup => new
                             {
                                 Key = rowGroup.Key,
                                 Values = columns.GroupJoin(
                                     rowGroup,
                                     c => c,
                                     r => columnSelector(r),
                                     (c, columnGroup) => dataSelector(columnGroup))
                             }).OrderBy(x=> x.Key);

            foreach (var row in rows)
            {
                var dataRow = table.NewRow();
                var items = row.Values.Cast<object>().ToList();
                items.Insert(0, row.Key);
                dataRow.ItemArray = items.ToArray();
                table.Rows.Add(dataRow);
            }

            return table;
        }

        public static dynamic[] ToPivotArray<T, TColumn, TRow, TData>(
    this IEnumerable<T> source,
    Func<T, TColumn> columnSelector,
    Expression<Func<T, TRow>> rowSelector,
    Func<IEnumerable<T>, TData> dataSelector)
        {
            var arr = new List<object>();
            var cols = new List<string>();
            String rowName = ((MemberExpression)rowSelector.Body).Member.Name;
            var columns = source.Select(columnSelector).Distinct();

            cols = (new[] { rowName }).Concat(columns.Select(x => x.ToString())).ToList();

            var rows = source.GroupBy(rowSelector.Compile())
                             .Select(rowGroup => new
                             {
                                 Key = rowGroup.Key,
                                 Values = columns.GroupJoin(
                                     rowGroup,
                                     c => c,
                                     r => columnSelector(r),
                                     (c, columnGroup) => dataSelector(columnGroup))
                             }).ToArray();

            foreach (var row in rows)
            {
                var items = row.Values.Cast<object>().ToList();
                items.Insert(0, row.Key);
                var obj = GetAnonymousObject(cols, items);
                arr.Add(obj);
            }
            return arr.ToArray();
        }

        private static dynamic GetAnonymousObject(IEnumerable<string> columns, IEnumerable<object> values)
        {
            IDictionary<string, object> eo = new ExpandoObject() as IDictionary<string, object>;
            int i;
            for (i = 0; i < columns.Count(); i++)
            {
                eo.Add(columns.ElementAt<string>(i), values.ElementAt<object>(i));
            }
            return eo;
        }
    }
}