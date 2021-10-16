using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;

namespace Lib.Infrastructure.Extensions
{
    public static class DataTableExtensions
    {
        /// <summary>
        /// Transform a table to a pivot table based on the given information.
        /// </summary>
        /// <param name="dataValues">The table which is going to be transformed to pivot table. The table should be sorted by the key colums, starting from most left part.</param>
        /// <param name="keyColumn">Array of String which contains the columns in dataValues that is going to be used as a keyColumn for display</param>
        /// <param name="pivotNameColumn">The Column name of the dataValues which is going to be displayed as pivot table's column</param>
        /// <param name="pivotValueColumn">The Column name of the dataValues which is going to be used to fill the value for the pivot table's column</param>
        /// <returns></returns>
        public static DataTable Pivot(this DataTable dataValues, string[] keyColumn, string pivotNameColumn, string pivotValueColumn)
        {
            try
            {
                DataTable tmp = new DataTable();
                DataRow r;
                string[] LastKey = new string[keyColumn.Length];
                int i, keyColumnIndex, pValIndex, pNameIndex;
                string s;
                bool FirstRow = true;
                bool keyChanged = false;

                // Add non-pivot columns to the data table:
                pValIndex = dataValues.Columns.IndexOf(pivotValueColumn);
                pNameIndex = dataValues.Columns.IndexOf(pivotNameColumn);

                for (i = 0; i <= dataValues.Columns.Count - 1; i++)
                    if (i != pValIndex && i != pNameIndex)
                    {
                        tmp.Columns.Add(dataValues.Columns[i].ColumnName.ToString(), dataValues.Columns[i].DataType);
                    }
                r = tmp.NewRow();

                // now, fill up the table with the data:
                foreach (DataRow row in dataValues.Rows)
                {
                    // see if we need to start a new row
                    keyColumnIndex = 0;
                    keyChanged = false;
                    if (!FirstRow)
                    {
                        while (!keyChanged && keyColumnIndex < keyColumn.Length)
                        {
                            if (row[keyColumn[keyColumnIndex]].ToString() != LastKey[keyColumnIndex])
                            {
                                keyChanged = true;
                            }
                            keyColumnIndex++;
                        }
                    }
                    else
                    {
                        for (keyColumnIndex = 0; keyColumnIndex < keyColumn.Length; keyColumnIndex++)
                        {
                            LastKey[keyColumnIndex] = row[keyColumn[keyColumnIndex]].ToString();
                        }
                    }

                    if (keyChanged || FirstRow)
                    {
                        // if this isn't the very first row, we need to add the last one to the table
                        if (!FirstRow)
                        {
                            tmp.Rows.Add(r);
                        }
                        r = tmp.NewRow();

                        // Add all non-pivot column values to the new row:
                        for (i = 0; i <= dataValues.Columns.Count - 3; i++)
                        {
                            r[i] = row[tmp.Columns[i].ColumnName];
                        }
                        FirstRow = false;

                        for (keyColumnIndex = 0; keyColumnIndex < keyColumn.Length; keyColumnIndex++)
                        {
                            LastKey[keyColumnIndex] = row[keyColumn[keyColumnIndex]].ToString();
                        }
                    }

                    // assign the pivot values to the proper column; add new columns if needed:
                    s = row[pNameIndex].ToString();
                    if (s.Length > 0)
                    {
                        if (!tmp.Columns.Contains(s) && s != null)
                        {
                            tmp.Columns.Add(s, dataValues.Columns[pValIndex].DataType);
                        }
                        r[s] = row[pValIndex];
                    }
                }
                // add that final row to the datatable:
                tmp.Rows.Add(r);

                // and that's it!
                return tmp;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<dynamic> ToDynamicList(this DataTable dt)
        {
            var list = new List<dynamic>();
            foreach (DataRow row in dt.Rows)
            {
                dynamic dyn = new ExpandoObject();
                list.Add(dyn);
                foreach (DataColumn column in dt.Columns)
                {
                    var dic = (IDictionary<string, object>)dyn;
                    dic[column.ColumnName] = row[column];
                }
            }
            return list;
        }

    }
}