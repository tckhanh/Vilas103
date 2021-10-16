using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace Lib.ExcelLib
{
    // Class ExcelIO tuong duong nhu Class BaseController
    public class EpplusIO
    {
        public EpplusIO()
        {
        }

        public void LogError(Exception e, string description = "")
        {
            try
            {
                //WebMsgBox.Show(e.Message);

                //Error error = new Error();
                //error.Controller = e.Source;
                //error.CreatedDate = DateTime.Now;
                //error.Message = e.Message;
                //error.Description = description;
                //error.StackTrace = e.StackTrace;
                
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                Trace.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        public DataTable ReadSheet(string fullFileName, string sheetName, bool hasHeader = true)
        {
            DataTable tbl = new DataTable();
            // nếu đường dẫn null hoặc rỗng thì báo không hợp lệ và return hàm
            if (string.IsNullOrEmpty(fullFileName))
            {
                return tbl;
            }

            try
            {
                using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(fullFileName)))
                {
                    // đặt tên người tạo file
                    excelPackage.Workbook.Properties.Author = "VCC2";
                    // đặt tiêu đề cho file
                    excelPackage.Workbook.Properties.Title = "VCC2";

                    foreach (ExcelWorksheet xlWorkSheet in excelPackage.Workbook.Worksheets)
                    {
                        if (xlWorkSheet.Name == sheetName)
                        {
                            int lastColumn = 1;
                            if (hasHeader) {                                
                                for (int i = 1; i <= xlWorkSheet.Dimension.End.Column; i++)
                                {
                                    if (xlWorkSheet.Cells[1, i].Value.ToString().Trim() != "")
                                    {
                                        lastColumn = i;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                lastColumn = xlWorkSheet.Dimension.End.Column;
                            }

                            foreach (var firstRowCell in xlWorkSheet.Cells[1, 1, 1, lastColumn])
                            {
                                tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                            }
                            var startRow = hasHeader ? 2 : 1;
                            for (int rowNum = startRow; rowNum <= xlWorkSheet.Dimension.End.Row; rowNum++)
                            {
                                var wsRow = xlWorkSheet.Cells[rowNum, 1, rowNum, lastColumn];
                                DataRow row = tbl.Rows.Add();
                                foreach (var cell in wsRow)
                                {
                                    row[cell.Start.Column - 1] = cell.Value;
                                }
                            }
                            return tbl;
                        }
                    }
                    return tbl;
                }
            }
            catch (DbEntityValidationException ex)
            {
                string description = "";
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                    description += ($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.\n");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        description += ($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"\n");
                    }
                }

                LogError(ex, description);
                Trace.WriteLine(ex.ToString());
                throw;
            }
            catch (Exception ex)
            {
                LogError(ex);
                Trace.WriteLine(ex.ToString());
                throw;
            }
        }

        public bool FormatColumns(string fullFileName, string[] sheetNames, string[] columnNames, string strFormat)
        {
            // nếu đường dẫn null hoặc rỗng thì báo không hợp lệ và return hàm
            if (string.IsNullOrEmpty(fullFileName))
            {
                return false;
            }

            try
            {
                using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(fullFileName)))
                {
                    // đặt tên người tạo file
                    //excelPackage.Workbook.Properties.Author = "VCC2";

                    // đặt tiêu đề cho file
                    //excelPackage.Workbook.Properties.Title = "VCC2";

                    foreach (ExcelWorksheet xlWorkSheet in excelPackage.Workbook.Worksheets)
                    {
                        if (Array.Exists(sheetNames, x => x == xlWorkSheet.Name)){
                            for (int col = xlWorkSheet.Dimension.Start.Column; col <= xlWorkSheet.Dimension.End.Column; col++)
                            {
                                if (xlWorkSheet.Cells[1, col].Value != null)
                                {
                                    if (Array.Exists(columnNames, x => x == xlWorkSheet.Cells[1, col].Value.ToString()))
                                    {
                                        xlWorkSheet.Column(col).AutoFit();
                                        xlWorkSheet.Column(col).Style.Numberformat.Format = strFormat;
                                    }
                                }
                            }
                        }
                    }

                    excelPackage.Save();
                }
                return true;
            }
            catch (DbEntityValidationException ex)
            {
                string description = "";
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                    description += ($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.\n");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        description += ($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"\n");
                    }
                }

                LogError(ex, description);
                Trace.WriteLine(ex.ToString());
                throw;
            }
            catch (Exception ex)
            {
                LogError(ex);
                Trace.WriteLine(ex.ToString());
                throw;
            }
        }

        public bool AddNewColumns(string fullFileName, string sheetName, string colNames)
        {
            // nếu đường dẫn null hoặc rỗng thì báo không hợp lệ và return hàm
            if (string.IsNullOrEmpty(fullFileName))
            {
                return false;
            }

            try
            {
                using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(fullFileName)))
                {
                    // đặt tên người tạo file
                    excelPackage.Workbook.Properties.Author = "VCC2";

                    // đặt tiêu đề cho file
                    excelPackage.Workbook.Properties.Title = "VCC2";

                    foreach (ExcelWorksheet xlWorkSheet in excelPackage.Workbook.Worksheets)
                    {
                        if (xlWorkSheet.Name == sheetName)
                        {
                            int colCount = xlWorkSheet.Dimension.End.Column;

                            string[] columns = colNames.Split(new char[] { ';' });
                            for (int i = 0; i < columns.Length; i++)
                            {
                                xlWorkSheet.Cells[1, colCount + i + 1].Value = columns[i];
                            }
                        }

                        excelPackage.Save();
                    }
                    return true;
                }
            }
            catch (DbEntityValidationException ex)
            {
                string description = "";
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                    description += ($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.\n");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        description += ($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"\n");
                    }
                }

                LogError(ex, description);
                Trace.WriteLine(ex.ToString());
                throw;
            }
            catch (Exception ex)
            {
                LogError(ex);
                Trace.WriteLine(ex.ToString());
                throw;
            }
        }

        public bool UpdateDataInSheet(string fullFileName, string sheetName, string PrimaryKey, string keyField, string valueFields, DataTable dt)
        {
            // nếu đường dẫn null hoặc rỗng thì báo không hợp lệ và return hàm
            if (string.IsNullOrEmpty(fullFileName))
            {
                return false;
            }

            try
            {
                using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(fullFileName)))
                {
                    // đặt tên người tạo file
                    excelPackage.Workbook.Properties.Author = "VCC2";

                    // đặt tiêu đề cho file
                    excelPackage.Workbook.Properties.Title = "VCC2";

                    foreach (ExcelWorksheet xlWorkSheet in excelPackage.Workbook.Worksheets)
                    {
                        if (xlWorkSheet.Name == sheetName)
                        {
                            int colCount = xlWorkSheet.Dimension.End.Column;
                            int rowCount = xlWorkSheet.Dimension.End.Row;
                            int keyIndex = -1;
                            string[] columns = valueFields.Split(new char[] { ';' });

                            for (int i = 0; i < colCount; i++)
                            {
                                if (xlWorkSheet.Cells[1, i + 1].Value.ToString() == keyField)
                                {
                                    keyIndex = i;
                                }
                            }

                            if (keyIndex >= 0)
                            {
                                for (int j = 1; j < rowCount; j++) // duyet qua tung dong cua file Excel va cap nhat cac cot
                                {
                                    string keyValue = xlWorkSheet.Cells[j + 1, keyIndex + 1].Value.ToString();

                                    dt.PrimaryKey = new DataColumn[] { dt.Columns[PrimaryKey] };

                                    DataRow row = dt.Rows.Find(keyValue);

                                    for (int n = 0; n < colCount; n++)   // duyet qua cac cot cua file Excel
                                    {
                                        for (int m = 0; m < columns.Length; m++)  // duyet qua danh sach cac Field can cap nhat
                                        {
                                            if (xlWorkSheet.Cells[1, n + 1].Value.ToString() == columns[m])
                                            {
                                                xlWorkSheet.Cells[j + 1, n + 1].Value = row[columns[m]]; // cap nhat
                                            }
                                        }
                                    }
                                }
                                excelPackage.Save();
                                return true;
                            }
                        }
                    }
                    return false;
                }
            }
            catch (DbEntityValidationException ex)
            {
                string description = "";
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                    description += ($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.\n");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        description += ($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"\n");
                    }
                }

                LogError(ex, description);
                Trace.WriteLine(ex.ToString());
                throw;
            }
            catch (Exception ex)
            {
                LogError(ex);
                Trace.WriteLine(ex.ToString());
                throw;
            }
        }

        public bool UpdateDataInSheet(string fullFileName, string sheetName, DataTable dt)
        {
            // nếu đường dẫn null hoặc rỗng thì báo không hợp lệ và return hàm
            if (string.IsNullOrEmpty(fullFileName))
            {
                return false;
            }

            try
            {
                using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(fullFileName)))
                {
                    // đặt tên người tạo file
                    excelPackage.Workbook.Properties.Author = "VCC2";
                    // đặt tiêu đề cho file
                    excelPackage.Workbook.Properties.Title = "VCC2";

                    ExcelWorksheet _xlWorkSheet = excelPackage.Workbook.Worksheets.FirstOrDefault(x => x.Name == sheetName);
                    if (_xlWorkSheet == null)
                        _xlWorkSheet = excelPackage.Workbook.Worksheets.Add(sheetName);
                    else
                    {
                        //_xlWorkSheet.Cells.Clear();
                        //DeleteColumns(_xlWorkSheet, 1, _xlWorkSheet.Dimension.End.Column);
                        DeleteRows(_xlWorkSheet, 2, _xlWorkSheet.Dimension.End.Row);
                    }
                    _xlWorkSheet.Cells["A1"].LoadFromDataTable(dt, true);

                    //Autofit with minimum size for the column.
                    double minimumSize = 5;
                    double maximumSize = 50;
                    //_xlWorkSheet.Protection.AllowSelectLockedCells
                    //_xlWorkSheet.Select("A1");
                    _xlWorkSheet.Cells[_xlWorkSheet.Dimension.Address].AutoFitColumns(minimumSize, maximumSize);
                    _xlWorkSheet.Cells[_xlWorkSheet.Dimension.Address].Style.WrapText = true;
                    _xlWorkSheet.Cells[_xlWorkSheet.Dimension.Address].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    _xlWorkSheet.Cells[_xlWorkSheet.Dimension.Address].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    _xlWorkSheet.Cells[_xlWorkSheet.Dimension.Address].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    _xlWorkSheet.Cells[_xlWorkSheet.Dimension.Address].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    _xlWorkSheet.Cells[_xlWorkSheet.Dimension.Address].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    _xlWorkSheet.Cells[_xlWorkSheet.Dimension.Address].AutoFilter = true;                    

                    //fill row 1 with striped orange background

                    _xlWorkSheet.Cells[1, 1, 1, _xlWorkSheet.Dimension.End.Column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    _xlWorkSheet.Cells[1, 1, 1, _xlWorkSheet.Dimension.End.Column].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
                    _xlWorkSheet.Cells[1, 1, 1, _xlWorkSheet.Dimension.End.Column].Style.Font.Bold = true;
                    _xlWorkSheet.Cells[1, 1, 1, _xlWorkSheet.Dimension.End.Column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                    excelPackage.Save();
                    return true;
                }
            }
            catch (DbEntityValidationException ex)
            {
                string description = "";
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                    description += ($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.\n");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        description += ($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"\n");
                    }
                }

                LogError(ex, description);
                Trace.WriteLine(ex.ToString());
                throw;
            }
            catch (Exception ex)
            {
                LogError(ex);
                Trace.WriteLine(ex.ToString());
                throw;
            }
        }

        public bool UpdateColums(string fullFileName, string sheetName, string updateColNames, string updateValue)
        {
            // nếu đường dẫn null hoặc rỗng thì báo không hợp lệ và return hàm
            if (string.IsNullOrEmpty(fullFileName))
            {
                return false;
            }

            try
            {
                using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(fullFileName)))
                {
                    // đặt tên người tạo file
                    excelPackage.Workbook.Properties.Author = "VCC2";

                    // đặt tiêu đề cho file
                    excelPackage.Workbook.Properties.Title = "VCC2";

                    foreach (ExcelWorksheet xlWorkSheet in excelPackage.Workbook.Worksheets)
                    {
                        if (xlWorkSheet.Name == sheetName)
                        {
                            int colCount = xlWorkSheet.Dimension.End.Column;
                            int rowCount = xlWorkSheet.Dimension.End.Row;

                            string[] columns = updateColNames.Split(new char[] { ';' });
                            for (int z = 0; z < columns.Length; z++)
                            {
                                for (int i = 0; i < colCount; i++)
                                {
                                    if (xlWorkSheet.Cells[1, i + 1].Value.ToString() == columns[z])
                                    {
                                        for (int j = 0; j < rowCount; j++)
                                        {
                                            xlWorkSheet.Cells[j + 1, i + 1].Value = updateValue;
                                        }
                                    }
                                }
                            }
                        }
                        excelPackage.Save();
                    }
                    return true;
                }
            }
            catch (DbEntityValidationException ex)
            {
                string description = "";
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                    description += ($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.\n");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        description += ($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"\n");
                    }
                }

                LogError(ex, description);
                Trace.WriteLine(ex.ToString());
                throw;
            }
            catch (Exception ex)
            {
                LogError(ex);
                Trace.WriteLine(ex.ToString());
                throw;
            }
        }

        public bool UpdateCells(string fullFileName, string sheetName, string updateField, string updateValue, string keyField, string keyValue)
        {
            // nếu đường dẫn null hoặc rỗng thì báo không hợp lệ và return hàm
            if (string.IsNullOrEmpty(fullFileName))
            {
                return false;
            }

            try
            {
                using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(fullFileName)))
                {
                    // đặt tên người tạo file
                    excelPackage.Workbook.Properties.Author = "VCC2";

                    // đặt tiêu đề cho file
                    excelPackage.Workbook.Properties.Title = "VCC2";

                    foreach (ExcelWorksheet xlWorkSheet in excelPackage.Workbook.Worksheets)
                    {
                        if (xlWorkSheet.Name == sheetName)
                        {
                            int colCount = xlWorkSheet.Dimension.End.Column;
                            int rowCount = xlWorkSheet.Dimension.End.Row;
                            int indexUpdateCol = -1;
                            int indexKeyCol = -1;

                            for (int i = 0; i < colCount; i++)
                            {
                                if (xlWorkSheet.Cells[1, i + 1].Value.ToString() == updateField)
                                {
                                    indexUpdateCol = i;
                                }

                                if (xlWorkSheet.Cells[1, i + 1].Value.ToString() == keyField)
                                {
                                    indexKeyCol = i;
                                }
                            }

                            if (indexUpdateCol >= 0 && indexKeyCol >= 0)
                            {
                                for (int i = 0; i < rowCount; i++)
                                {
                                    if (xlWorkSheet.Cells[i + 1, indexKeyCol + 1].Value.ToString() == keyValue)
                                    {
                                        xlWorkSheet.Cells[i + 1, indexUpdateCol + 1].Value = updateValue;
                                    }
                                }
                            }
                        }
                        excelPackage.Save();
                    }
                    return true;
                }
            }
            catch (DbEntityValidationException ex)
            {
                string description = "";
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                    description += ($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.\n");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        description += ($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"\n");
                    }
                }

                LogError(ex, description);
                Trace.WriteLine(ex.ToString());
                throw;
            }
            catch (Exception ex)
            {
                LogError(ex);
                Trace.WriteLine(ex.ToString());
                throw;
            }
        }

        private DataTable BuildHeadersFromFirstRowThenRemoveFirstRow(DataTable dt)
        {
            DataRow firstRow = dt.Rows[0];

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (!string.IsNullOrWhiteSpace(firstRow[i].ToString())) // handle empty cell
                    dt.Columns[i].ColumnName = firstRow[i].ToString().Trim();
            }

            dt.Rows.RemoveAt(0);

            return dt;
        }

        public void DeleteColumns(ExcelWorksheet wsSheet, int fromColumn, int toColumn)
        {
            for (int i = toColumn; i >= fromColumn; i--)
            {
                wsSheet.DeleteColumn(fromColumn);
            }            
        }

        public void DeleteRows(ExcelWorksheet wsSheet, int fromRow, int toRow)
        {
            for (int i = toRow; i >= fromRow; i--)
            {
                wsSheet.DeleteRow(fromRow);
            }
        }
    }
}