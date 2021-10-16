using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Lib.ExcelLib
{
    public static class EPPlusExtensions
    {
        public static void HowtouseGetVal(string fullFileName)
        {
            ExcelPackage excelPackage = new ExcelPackage(new FileInfo(fullFileName));
            var worksheet = excelPackage.Workbook.Worksheets[0];
            var rowCount = worksheet.Dimension.Rows;
            var columnCount = worksheet.Dimension.Columns;

            for (int row = 1; row <= rowCount; row++)
            {
                for (int col = 1; col <= columnCount; col++)
                {
                    string val = worksheet.Cells[row, col].GetVal();
                }
            }
        }

        public static string GetVal(this ExcelRange @this)
        {
            if (@this.Merge)
            {
                var idx = @this.Worksheet.GetMergeCellId(@this.Start.Row, @this.Start.Column);
                string mergedCellAddress = @this.Worksheet.MergedCells[idx - 1];
                string firstCellAddress = @this.Worksheet.Cells[mergedCellAddress].Start.Address;
                return @this.Worksheet.Cells[firstCellAddress].Value?.ToString()?.Trim() ?? "";
            }
            else
            {
                return @this.Value?.ToString()?.Trim() ?? "";
            }
        }

        public static string GetMergedRangeAddress(this ExcelRange @this)
        {
            if (@this.Merge)
            {
                var idx = @this.Worksheet.GetMergeCellId(@this.Start.Row, @this.Start.Column);
                return @this.Worksheet.MergedCells[idx - 1]; //the array is 0-indexed but the mergeId is 1-indexed...
            }
            else
            {
                return @this.Address;
            }
        }       
    }
}