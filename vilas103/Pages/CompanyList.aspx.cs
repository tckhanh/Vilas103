using AutoMapper;
using Data.DataVMs;
using Data.Providers;
using DevExpress.Web;
using DevExpress.Web.Demos;
using DevExpress.Web.Office;
using DevExpress.XtraRichEdit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.UI.WebControls;
using vilas103.Helper;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace vilas103
{
    public partial class CompanyList : System.Web.UI.Page
    {
        static bool CloneKey = false;
        public const string documentId = "mailMergeDocId";
        enum NoRequiredFields
        {

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            GridViewFeaturesHelper.SetupGlobalGridViewBehavior(GridView);

            DemoHelper.Instance.ControlAreaMaxWidth = Unit.Pixel(1300);
            //if (!IsPostBack)
                //GridView.StartEdit(2);
        }

        protected void GridView_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            CompanyVM CloneItem = new CompanyVM();
            if (CloneKey == true)
            {
                List<string> selectedIds = GridView.GetSelectedFieldValues("CompanyID").ConvertAll(x => x.ToString());
                CloneItem = Mapper.Map<CompanyVM>(CompanyProvider.GetSingleById(selectedIds.Last()));
                CloneKey = false;
            }
            PropertyInfo myFieldInfo;
            foreach (var myProperty in typeof(CompanyVM).GetProperties())
            {
                myFieldInfo = typeof(CompanyVM).GetProperty(myProperty.Name);
                if (myFieldInfo == null) continue;
                var ttt = myFieldInfo.GetValue(CloneItem);

                e.NewValues[myProperty.Name] = myFieldInfo.GetValue(CloneItem);
            }
        }

        protected void GridView_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "Clone")
            {
                CloneKey = true;
            }
        }

        protected void GridView_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            CompanyVM item = new CompanyVM();

            //foreach (var itemKey in e.NewValues.Keys)
            foreach (GridViewColumn column in GridView.Columns)
            {
                GridViewDataColumn dataColumn = column as GridViewDataColumn;
                if (dataColumn == null) continue;

                PropertyInfo myFieldInfo = typeof(CompanyVM).GetProperty(dataColumn.FieldName);
                if (myFieldInfo == null) continue;

                myFieldInfo.SetValue(item, e.NewValues[dataColumn.FieldName]);

                ValidationContext context = new ValidationContext(item)
                {
                    MemberName = dataColumn.FieldName
                };
                var errors = new List<ValidationResult>();

                var myProperty = item.GetType().GetProperty(dataColumn.FieldName).GetValue(item, null);

                bool isValid = Validator.TryValidateProperty(myProperty, context, errors);

                if (!isValid)
                {
                    //e.Errors[dataColumn] = errors[0].ErrorMessage;
                    AddError(e.Errors, dataColumn, errors[0].ErrorMessage);
                }
                //var myPropertyInfo = item.GetType().GetProperty(dataColumn.FieldName);
                //myFieldInfo.SetValue(item, e.NewValues[itemKey.ToString()]);
            }


            //foreach (GridViewColumn column in GridView.Columns)
            //{
            //    GridViewDataColumn dataColumn = column as GridViewDataColumn;
            //    if (dataColumn == null) continue;

            //    PropertyInfo myProperty = item.GetType().GetProperty(dataColumn.FieldName);

            //    var ctx = new ValidationContext(myProperty);
            //    var errors = new List<ValidationResult>();
            //    var myPropertyInfo = item.GetType().GetProperty(dataColumn.FieldName);

            //    isValid = Validator.TryValidateProperty(myPropertyInfo, ctx, errors);

            //    if (!isValid)
            //    {
            //        e.Errors[dataColumn] = errors.ToString();
            //    }
            //}

            // Annotation.GetAttribute(typeof(CompanyVM));
            // Checks for null values.

            //foreach (GridViewColumn column in GridView.Columns)
            //{
            //    GridViewDataColumn dataColumn = column as GridViewDataColumn;
            //    if (dataColumn == null) continue;
            //    if (e.NewValues[dataColumn.FieldName] == null)
            //    {
            //        // in an array of System.Reflection.MemberInfo objects.
            //        PropertyInfo MyPropertyInfo = typeof(CompanyVM).GetProperty(dataColumn.FieldName);

            //        RequiredAttribute att = (RequiredAttribute)Attribute.GetCustomAttribute(MyPropertyInfo, typeof(RequiredAttribute));
            //        if (att != null)
            //        {
            //            e.Errors[dataColumn] = att.ErrorMessage;
            //        }
            //    }
            //}



            //            if (e.NewValues["ContactName"] != null &&
            //                e.NewValues["ContactName"].ToString().Length< 2)
            //            {
            //                AddError(e.Errors, GridView.Columns["ContactName"],
            //                "Contact Name must be at least two characters long.");
            //    }
            //            if (e.NewValues["CompanyName"] != null &&
            //            e.NewValues["CompanyName"].ToString().Length< 2)
            //            {
            //                AddError(e.Errors, GridView.Columns["CompanyName"],
            //                "Company Name must be at least two characters long.");
            //}

            //            if (e.NewValues["FirstName"] != null && e.NewValues["FirstName"].ToString().Length< 2)
            //            {
            //                AddError(e.Errors, GridView.Columns["FirstName"], "First Name must be at least two characters long.");
            //            }
            //            if (e.NewValues["LastName"] != null && e.NewValues["LastName"].ToString().Length< 2)
            //            {
            //                AddError(e.Errors, GridView.Columns["LastName"], "Last Name must be at least two characters long.");
            //            }
            //            if (e.NewValues["Email"] != null && !e.NewValues["Email"].ToString().Contains("@"))
            //            {
            //                AddError(e.Errors, GridView.Columns["Email"], "Invalid e-mail.");
            //            }

            //            int age = e.NewValues["Age"] != null ? (int)e.NewValues["Age"] : 0;
            //            if (age< 18)
            //            {
            //                AddError(e.Errors, GridView.Columns["Age"], "Age must be greater than or equal 18.");
            //            }
            //            //DateTime arrival = DateTime.MinValue;
            //            //DateTime.TryParse(e.NewValues["ArrivalDate"] == null ? string.Empty : e.NewValues["ArrivalDate"].ToString(), out arrival);
            //            //if (DateTime.Today.Year != arrival.Year || DateTime.Today.Month != arrival.Month)
            //            //{
            //            //    AddError(e.Errors, GridView.Columns["ArrivalDate"], "Arrival date is required and must belong to the current month.");
            //            //}

            //            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
            //                e.RowError = "Please, correct all errors.";



            // Displays the error row if there is at least one error.
            //isValid = Validator.TryValidateObject(item, ctx, errors, true);

            if (e.Errors.Count > 0) e.RowError = "Vui lòng nhập đầy đủ thông tin theo yêu cầu.";
        }

        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }
        protected void GridView_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            //if (!object.Equals(e.RowType, GridViewRowType.Data))
            //    return;

            //bool hasError = string.IsNullOrEmpty(e.GetValue("CompanyName").ToString());
            //hasError = hasError || string.IsNullOrEmpty(e.GetValue("Address").ToString());
            ////hasError = hasError || !e.GetValue("Email").ToString().Contains("@");
            ////hasError = hasError || (int)e.GetValue("Age") < 18;
            ////DateTime arrival = (DateTime)e.GetValue("ArrivalDate");
            ////hasError = hasError || DateTime.Today.Year != arrival.Year || DateTime.Today.Month != arrival.Month;
            ////if (hasError)
            //{
            //    e.Row.ForeColor = System.Drawing.Color.Red;
            //}
        }
        protected void GridView_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            if (!GridView.IsNewRowEditing)
            {
                GridView.DoRowValidation();
            }
            else
            {
            }
        }

        protected void GridView_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            ((ASPxEdit)e.Editor).ValidationSettings.Display = Display.Dynamic;
        }

        protected void GridView_EditFormLayoutCreated(object sender, ASPxGridViewEditFormLayoutEventArgs e)
        {
            // Du thua do da thuc hien o GridView_DataBound roi
            //ASPxGridView gridView = sender as ASPxGridView;
            //foreach (GridViewColumn column in gridView.Columns)
            //{
            //    GridViewDataColumn dataColumn = column as GridViewDataColumn;
            //    if (dataColumn != null)
            //    {
            //        PropertyInfo MyPropertyInfo = typeof(CompanyVM).GetProperty(dataColumn.FieldName);
            //        DisplayAttribute displayAtt = (DisplayAttribute)Attribute.GetCustomAttribute(MyPropertyInfo, typeof(DisplayAttribute));
            //        if (displayAtt != null && displayAtt.Name != null)
            //        {
            //            LayoutItem layoutItem = (LayoutItem)e.FindLayoutItemByColumn(dataColumn.FieldName);
            //            if (layoutItem != null)
            //            {
            //                RequiredAttribute requiredAtt = (RequiredAttribute)Attribute.GetCustomAttribute(MyPropertyInfo, typeof(RequiredAttribute));
            //                if (requiredAtt != null)
            //                {
            //                    layoutItem.Caption = displayAtt.Name + "*";
            //                }
            //                else
            //                {
            //                    layoutItem.Caption = displayAtt.Name;
            //                }
            //            }
            //        }
            //    }
            //}
        }


        protected void GridView_DataBound1(object sender, EventArgs e)
        {
            ASPxGridView gridView = sender as ASPxGridView;
            foreach (GridViewColumn column in gridView.Columns)
            {
                //if (column.GetType().ToString() == "DevExpress.Web.GridViewDataTextColumn")
                //{
                GridViewDataColumn dataColumn = column as GridViewDataColumn;
                if (dataColumn != null)
                {
                    PropertyInfo MyPropertyInfo = typeof(CompanyVM).GetProperty(dataColumn.FieldName);
                    if (MyPropertyInfo != null)
                    {
                        DisplayAttribute displayAtt = (DisplayAttribute)Attribute.GetCustomAttribute(MyPropertyInfo, typeof(DisplayAttribute));
                        if (displayAtt != null && displayAtt.Name != null)
                        {
                            column.Caption = displayAtt.Name;
                        }
                    }
                }
                //}
            }
        }

        protected void GridView_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {

        }

        protected void PageToolbar_ItemCommand(object source, MenuItemCommandEventArgs e)
        {

        }

        protected void PageToolbar_ItemClick(object source, MenuItemEventArgs e)
        {

        }

        protected void GridView_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {
            foreach (var args in e.InsertValues)
            {
                //InsertNewItem(args.NewValues);
            }
            //foreach (var args in e.UpdateValues)
            //    UpdateItem(args.Keys, args.NewValues);
            //foreach (var args in e.DeleteValues)
            //    DeleteItem(args.Keys, args.Values);

            e.Handled = true;
        }

        protected void GridView_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            //GridView.GetCurrentPageRowValues("CompanyID");
            //CloneKey = Convert.ToInt32(GridView.GetSelectedFieldValues("CompanyID").FirstOrDefault());
        }
    }
}