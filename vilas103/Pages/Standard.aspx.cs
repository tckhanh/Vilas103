using AutoMapper;
using Data.Common;
using Data.DataVMs;
using Data.Providers;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.Web.ASPxThemes;
using DevExpress.Web.Demos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;
using vilas103.Model;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace vilas103
{
    public partial class Standard : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public const string documentId = "mailMergeDocId";
        enum NoRequiredFields
        {

        }
        // Cách tạo lỗi Exception
        // NotImplementedException innerException = new NotImplementedException("NoReport");
        // throw new NotImplementedException("This message has been generated for a GetCallbackErrorMessage() method demonstration.", innerException);

        //protected override void InitializeCulture()
        //{
        //    if (!string.IsNullOrEmpty(GetDXCurrentLanguageValue()))
        //    {
        //        //for regional server standards 
        //        Culture = GetDXCurrentLanguageValue();
        //        //for DevExpress localizable strings 
        //        UICulture = GetDXCurrentLanguageValue();
        //    }
        //}
        protected void Page_Load(object sender, EventArgs e)
        {

            if (HttpContext.Current.Session["Log"] == null)
                HttpContext.Current.Session["Log"] = "";

            ASPxWebControl.RegisterBaseScript(this);
            GridViewFeaturesHelper.SetupGlobalGridViewBehavior(GridView);

            DemoHelper.Instance.ControlAreaMaxWidth = Unit.Pixel(1300);
            UpdatePageToolbarEnable();

            if (!IsPostBack)
            {
                HttpContext.Current.Session["isActBloChanged_Com"] = true;
                HttpContext.Current.Session["isCloneRow_Com"] = false;
                HttpContext.Current.Session["isDetailRow_Com"] = false;

            }
            if (IsCallback)
            {

            }
        }

        protected void GridView_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            StandardVM CloneItem = new StandardVM();
            if (bool.Parse(HttpContext.Current.Session["isCloneRow_Com"].ToString()) == true && GridView.FocusedRowIndex >= 0)
            {
                //List<int> selectedIds = GridView.GetSelectedFieldValues("Id").ConvertAll(id => (int)id);
                //CloneItem = StandardProvider.GetSingleById(selectedIds.Last());

                CloneItem = Mapper.Map<StandardVM>(StandardProvider.GetSingleById(GridView.GetRowValues(GridView.FocusedRowIndex, "Id").ToString()));


                HttpContext.Current.Session["isCloneRow_Com"] = false;

                PropertyInfo myFieldInfo;
                foreach (var myProperty in typeof(StandardVM).GetProperties())
                {
                    myFieldInfo = typeof(StandardVM).GetProperty(myProperty.Name);
                    if (myFieldInfo == null) continue;
                    var ttt = myFieldInfo.GetValue(CloneItem);

                    e.NewValues[myProperty.Name] = myFieldInfo.GetValue(CloneItem);
                }
            }
        }

        protected void GridView_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            switch (e.Parameters)
            {
                case "CloneRow":
                    HttpContext.Current.Session["isCloneRow_Com"] = true;
                    GridView.JSProperties["cpMessage"] = string.Format("OrderDate {0} is later than {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortDateString());
                    break;
                case "DetailRow":
                    HttpContext.Current.Session["isDetailRow_Com"] = true;
                    GridView.JSProperties["cpMessage"] = string.Format("OrderDate {0} is later than {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortDateString());
                    break;
                case "Actived":
                    if (GridView.FocusedRowIndex >= 0)
                    {
                        var itemID = GridView.GetRowValues(GridView.FocusedRowIndex, "Id").ToString();
                        StandardProvider.SetActived(itemID, true);
                        GridView.DataBind();
                        UpdateNavBarName();
                    }
                    GridView.JSProperties["cpMessage"] = string.Format("OrderDate {0} is later than {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortDateString());
                    break;
                case "InActived":
                    if (GridView.FocusedRowIndex >= 0)
                    {
                        var itemID = GridView.GetRowValues(GridView.FocusedRowIndex, "Id").ToString();
                        StandardProvider.SetActived(itemID, false);
                        GridView.DataBind();
                        UpdateNavBarName();
                    }
                    GridView.JSProperties["cpMessage"] = string.Format("OrderDate {0} is later than {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortDateString());
                    break;
                case "Blocked":
                    if (GridView.FocusedRowIndex >= 0)
                    {
                        var itemID = GridView.GetRowValues(GridView.FocusedRowIndex, "Id").ToString();
                        StandardProvider.SetBlocked(itemID, true, User.Identity.Name);
                        GridView.DataBind();
                        UpdateNavBarName();
                    }
                    GridView.JSProperties["cpMessage"] = string.Format("OrderDate {0} is later than {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortDateString());
                    break;
                case "UnBlocked":
                    if (GridView.FocusedRowIndex >= 0)
                    {
                        var itemID = GridView.GetRowValues(GridView.FocusedRowIndex, "Id").ToString();
                        StandardProvider.SetBlocked(itemID, false, User.Identity.Name);
                        GridView.DataBind();
                        UpdateNavBarName();
                    }
                    GridView.JSProperties["cpMessage"] = string.Format("OrderDate {0} is later than {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortDateString());
                    break;
                case "InActivedBlocked":
                    if (GridView.FocusedRowIndex >= 0)
                    {
                        var itemID = GridView.GetRowValues(GridView.FocusedRowIndex, "Id").ToString();
                        StandardProvider.SetActivedBlocked(itemID, false, true, User.Identity.Name);
                        GridView.DataBind();
                        UpdateNavBarName();
                    }
                    GridView.JSProperties["cpMessage"] = string.Format("OrderDate {0} is later than {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortDateString());
                    break;
                case "ActivedUnBlocked":
                    if (GridView.FocusedRowIndex >= 0)
                    {
                        var itemID = GridView.GetRowValues(GridView.FocusedRowIndex, "Id").ToString();
                        StandardProvider.SetActivedBlocked(itemID, true, false, User.Identity.Name);
                        GridView.DataBind();
                        UpdateNavBarName();
                    }
                    GridView.JSProperties["cpMessage"] = string.Format("OrderDate {0} is later than {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortDateString());
                    break;

                case "Delete":
                    if (GridView.FocusedRowIndex >= 0)
                    {
                        var itemID = GridView.GetRowValues(GridView.FocusedRowIndex, "Id").ToString();
                        StandardProvider.Delete(itemID);
                        //StandardProvider.Delete(itemID);
                        GridView.DataBind();
                        UpdateNavBarName();
                    }
                    GridView.JSProperties["cpMessage"] = string.Format("OrderDate {0} is later than {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortDateString());
                    break;

                    //case "Delete":
                    //List<int> selectedIds = GridView.GetSelectedFieldValues("Id").ConvertAll(id => (int)id);
                    //foreach (var item in selectedIds)
                    //{
                    //    StandardProvider.DeleteSuit(item, false);
                    //    HttpContext.Current.Session["isActBloChanged_Com"] = true;
                    //}
                    //GridView.DataBind();
                    //GridView.JSProperties["cpMessage"] = string.Format("OrderDate {0} is later than {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortDateString());
                    //break;
            }
            GridView.JSProperties["cpParameters"] = e.Parameters;
        }

        protected void GridView_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            //ASPxGridView gv = sender as ASPxGridView;
            //GridViewDataColumn column = gv.Columns["URL"] as GridViewDataColumn;
            //ASPxTextBox tb = gv.FindEditRowCellTemplateControl(column, "txtOMContractOrg") as ASPxTextBox;

            StandardVM item = new StandardVM();


            if (GridView.IsNewRowEditing)
            {
                //foreach (var itemKey in e.NewValues.Keys)
                foreach (GridViewColumn column in GridView.Columns)
                {
                    GridViewDataColumn dataColumn = column as GridViewDataColumn;
                    if (dataColumn == null) continue;

                    PropertyInfo myFieldInfo = typeof(StandardVM).GetProperty(dataColumn.FieldName);
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
            }
            else
            {
                //foreach (var itemKey in e.NewValues.Keys)
                foreach (GridViewColumn column in GridView.Columns)
                {
                    GridViewDataColumn dataColumn = column as GridViewDataColumn;
                    if (dataColumn == null) continue;

                    PropertyInfo myFieldInfo = typeof(StandardVM).GetProperty(dataColumn.FieldName);
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
                        if (dataColumn.FieldName != "FastName" && dataColumn.FieldName != "UserName" && dataColumn.FieldName != "Password" && dataColumn.FieldName != "ConfirmPassword")
                            //e.Errors[dataColumn] = errors[0].ErrorMessage;
                            AddError(e.Errors, dataColumn, errors[0].ErrorMessage);
                    }
                    //var myPropertyInfo = item.GetType().GetProperty(dataColumn.FieldName);
                    //myFieldInfo.SetValue(item, e.NewValues[itemKey.ToString()]);
                }
            }
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

            //bool hasError = string.IsNullOrEmpty(e.GetValue("Name").ToString());
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
        }

        protected void GridView_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            ((ASPxEdit)e.Editor).ValidationSettings.Display = Display.Dynamic;
            if (bool.Parse(HttpContext.Current.Session["isDetailRow_Com"].ToString()))
            {
                e.Editor.ReadOnly = true;
            }
        }

        protected void GridView_EditFormLayoutCreated(object sender, ASPxGridViewEditFormLayoutEventArgs e)
        {
            ASPxGridView gridView = sender as ASPxGridView;
            var layoutItemInfo = e.FindLayoutItemOrGroup("InfoTab");
            var layoutItemActived = e.FindLayoutItemOrGroup("Actived");

            layoutItemInfo.Visible = false;

            (GridView.Columns["FileName"] as GridViewDataTextColumn).ReadOnly = true;

            if (gridView.IsNewRowEditing)
            {
                (GridView.Columns["Id"] as GridViewDataTextColumn).ReadOnly = false;
                layoutItemActived.Visible = true;
                return;
            }
            else
            {
                (GridView.Columns["Id"] as GridViewDataTextColumn).ReadOnly = true;


                layoutItemActived.Visible = false;
                return;
            }

            //ASPxGridView gv = sender as ASPxGridView;
            //GridViewDataColumn column = gv.Columns["URL"] as GridViewDataColumn;
            //ASPxTextBox tb = gv.FindEditRowCellTemplateControl(column, "txtOMContractOrg") as ASPxTextBox;


            // Du thua do da thuc hien o GridView_DataBound roi
            //ASPxGridView gridView = sender as ASPxGridView;
            //foreach (GridViewColumn column in gridView.Columns)
            //{
            //    GridViewDataColumn dataColumn = column as GridViewDataColumn;
            //    if (dataColumn != null)
            //    {
            //        PropertyInfo MyPropertyInfo = typeof(StandardVM).GetProperty(dataColumn.FieldName);
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
            // Hiển thị Caption theo khai báo ở DatamodelVM
            ASPxGridView gridView = sender as ASPxGridView;
            foreach (GridViewColumn column in gridView.Columns)
            {
                //if (column.GetType().ToString() == "DevExpress.Web.GridViewDataTextColumn")
                //{
                GridViewDataColumn dataColumn = column as GridViewDataColumn;
                if (dataColumn != null)
                {
                    PropertyInfo MyPropertyInfo = typeof(StandardVM).GetProperty(dataColumn.FieldName);
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
            UpdateNavBarName();

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
            //GridView.GetCurrentPageRowValues("Id");
            //CloneKey = Convert.ToInt32(GridView.GetSelectedFieldValues("Id").FirstOrDefault());
        }

        protected void GridViewDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            //if (User.Identity.IsAuthenticated && Context.Session["User"] != null)
            if (User.Identity.IsAuthenticated)
            {
                GridViewDataSource.SelectMethod = "GetData";
                e.InputParameters.Clear(); // this is a different method with new parameters.
                e.InputParameters.Add("UserName", User.Identity.Name);
            }
            else
            {
                GridViewDataSource.SelectMethod = "GetData";
                e.InputParameters.Clear(); // this is a different method with new parameters.
                e.InputParameters.Add("UserName", "Anonymous");
            }

        }

        protected void UpdatePageToolbarEnable()
        {
            var isAuthenticated = Context.User.Identity.IsAuthenticated;
            PageToolbar.Items.FindByName("NewRow").Enabled = isAuthenticated;
            PageToolbar.Items.FindByName("CloneRow").Enabled = isAuthenticated;
            PageToolbar.Items.FindByName("EditRow").Enabled = isAuthenticated;
            PageToolbar.Items.FindByName("DetailRow").Enabled = isAuthenticated;
            PageToolbar.Items.FindByName("DeleteRow").Enabled = isAuthenticated;
            PageToolbar.Items.FindByName("Export").Enabled = isAuthenticated;
        }

        protected void GridView_FillContextMenuItems(object sender, ASPxGridViewContextMenuEventArgs e)
        {
            switch (e.MenuType)
            {
                case GridViewContextMenuType.Columns:
                    //var menuItem = e.Items.FindByCommand(GridViewContextMenuCommand.HideColumn);
                    //menuItem.Text = CommonConstants.MenuItem_HideColumn;

                    break;
                case GridViewContextMenuType.Rows:

                    GridViewContextMenuItem item = e.Items.FindByCommand(GridViewContextMenuCommand.ExportMenu);
                    item.Image.IconID = IconID.ExportExport16x16office2013;
                    item.Items.Add("Xuất ra File Excel", "CustomExportToXLS").Image.IconID = IconID.ExportExport16x16office2013;

                    item = e.CreateItem(CommonConstants.MenuItem_DetailRow, "DetailRow");
                    item.Image.IconID = IconID.GridGrid16x16office2013;
                    e.Items.Insert(e.Items.IndexOfCommand(GridViewContextMenuCommand.NewRow) + 1, item);

                    item = e.CreateItem(CommonConstants.MenuItem_CloneRow, "CloneRow");
                    item.Image.IconID = IconID.XafActionClonemergeCloneObjectSvg16x16;
                    e.Items.Insert(e.Items.IndexOfCommand(GridViewContextMenuCommand.NewRow) + 1, item);

                    item = e.CreateItem(CommonConstants.MenuItem_Actived, "Actived");
                    item.Image.IconID = IconID.RicheditTrackingchangesAccept16x16;
                    item.BeginGroup = true;
                    e.Items.Insert(e.Items.IndexOfCommand(GridViewContextMenuCommand.Refresh) + 1, item);

                    e.Items.Add(CommonConstants.MenuItem_InActived, "InActived").Image.IconID = IconID.XafActionCancelSvg16x16;
                    e.Items.Add(CommonConstants.MenuItem_Blocked, "Blocked").Image.IconID = IconID.IconbuilderSecurityLockSvg16x16;
                    e.Items.Add(CommonConstants.MenuItem_UnBlocked, "UnBlocked").Image.IconID = IconID.IconbuilderSecurityUnlockSvg16x16;
                    e.Items.Add(CommonConstants.MenuItem_InActivedBlocked, "InActivedBlocked").Image.IconID = IconID.SnapRemoveheader16x16;
                    e.Items.Add(CommonConstants.MenuItem_ActivedUnBlocked, "ActivedUnBlocked").Image.IconID = IconID.SnapSnapinsertheader16x16;

                    //e.Items.Add("Select All", "SelectAll");
                    //e.Items.Add("Deselect All", "DeselectAll");

                    PageToolbar.Items.FindByName("NewRow").Text = e.Items.FindByName("NewRow").Text;
                    e.Items.FindByName("NewRow").Enabled = PageToolbar.Items.FindByName("NewRow").Enabled;

                    PageToolbar.Items.FindByName("EditRow").Text = e.Items.FindByName("EditRow").Text;
                    e.Items.FindByName("EditRow").Enabled = PageToolbar.Items.FindByName("EditRow").Enabled;

                    PageToolbar.Items.FindByName("DeleteRow").Text = e.Items.FindByName("DeleteRow").Text;
                    e.Items.FindByName("DeleteRow").Enabled = PageToolbar.Items.FindByName("DeleteRow").Enabled;

                    PageToolbar.Items.FindByName("Export").Text = e.Items.FindByName("ExportMenu").Text;
                    e.Items.FindByName("ExportMenu").Enabled = PageToolbar.Items.FindByName("Export").Enabled;

                    e.Items.FindByName("CloneRow").Text = PageToolbar.Items.FindByName("CloneRow").Text;
                    e.Items.FindByName("CloneRow").Enabled = PageToolbar.Items.FindByName("CloneRow").Enabled;

                    e.Items.FindByName("DetailRow").Text = PageToolbar.Items.FindByName("DetailRow").Text;
                    e.Items.FindByName("DetailRow").Enabled = PageToolbar.Items.FindByName("DetailRow").Enabled;
                    break;
                default:
                    break;
            }
        }

        protected void GridView_ContextMenuItemClick(object sender, ASPxGridViewContextMenuItemClickEventArgs e)
        {
            if (e.Item.Name == "CustomExportToXLS")
                GridView.ExportXlsToResponse(new DevExpress.XtraPrinting.XlsExportOptionsEx { ExportType = ExportType.WYSIWYG });
        }

        protected void GridView_ContextMenuItemVisibility(object sender, ASPxGridViewContextMenuItemVisibilityEventArgs e)
        {
            e.SetEnabled(e.Items.FindByName("NewRow"), true);

            if (e.MenuType == GridViewContextMenuType.Rows)
            {
                //GridViewContextMenuItem menuItemSelected = e.Items.Find(item => item.Name == "NewRow") as GridViewContextMenuItem;
                //GridViewContextMenuItem menuItemSelectedAndDiscontinued = e.Items.Find(item => item.Name == "OnlySelectedAndDiscontinuedRows") as GridViewContextMenuItem;
                //for (int i = 0; i < GridView.VisibleRowCount; i++)
                //{
                //    e.SetEnabled(menuItemSelected, i, GridView.Selection.IsRowSelected(i));
                //    e.SetEnabled(menuItemSelectedAndDiscontinued, i, Grid.Selection.IsRowSelected(i) && (bool)Grid.GetRowValues(i, "Discontinued"));
                //}
            }
        }

        protected void GridView_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            //UpdateNavBarName();
            //List<bool> ActivedList = new List<bool>();
            //List<bool> BlockedList = new List<bool>();

            //for (int i = 0; i < GridView.VisibleRowCount; i++)
            //{
            //    ActivedList.Add((bool)GridView.GetRowValues(i, "Actived"));
            //    BlockedList.Add((bool)GridView.GetRowValues(i, "Blocked"));
            //}
            //e.Properties["cpActived"] = ActivedList;
            //e.Properties["cpBlocked"] = BlockedList;

            //if (bool.Parse(HttpContext.Current.Session["isActBloChanged_Com"].ToString()))
            //{
            //    HttpContext.Current.Session["isActBloChanged_Com"] = false;

            //    e.Properties["cpAllCount"] = StandardProvider.Count(x => true);
            //    e.Properties["cpInActivedUnBlockedCount"] = StandardProvider.Count(x => x.Actived == false && x.Blocked == false);
            //    e.Properties["cpActivedBlockedCount"] = StandardProvider.Count(x => x.Actived == true && x.Blocked == true);
            //    e.Properties["cpInactivedBlockedCount"] = StandardProvider.Count(x => x.Actived == false && x.Blocked == true);
            //    e.Properties["cpActivedUnBlockedCount"] = StandardProvider.Count(x => x.Actived == true && x.Blocked == false);
            //    e.Properties["cpOwnerCount"] = StandardProvider.Count(x => x.UserName == User.Identity.Name);
            //}
        }

        protected void GridView_CancelRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            HttpContext.Current.Session["isDetailRow_Com"] = false;
        }

        protected void GridView_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            if (e.ButtonType == ColumnCommandButtonType.Update && bool.Parse(HttpContext.Current.Session["isDetailRow_Com"].ToString()))
                e.Enabled = false;
        }

        protected void GridView_ContextMenuItemVisibility1(object sender, ASPxGridViewContextMenuItemVisibilityEventArgs e)
        {

        }

        protected string GetDXCurrentLanguageValue()
        {
            return Request.Cookies["DXCurrentLanguage"] == null ? "" : Request.Cookies["DXCurrentLanguage"].Value;
        }

        protected void GridView_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
        {
            UpdateNavBarName();
        }

        protected void GridView_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        {
            UpdateNavBarName();
        }

        protected void GridView_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
        {
            UpdateNavBarName();
        }

        private void UpdateNavBarName()
        {
            List<bool> ActivedList = new List<bool>();
            List<bool> BlockedList = new List<bool>();

            for (int i = 0; i < GridView.VisibleRowCount; i++)
            {
                ActivedList.Add((bool)GridView.GetRowValues(i, "Actived"));
                BlockedList.Add((bool)GridView.GetRowValues(i, "Blocked"));
            }
            GridView.JSProperties["cpActived"] = ActivedList;
            GridView.JSProperties["cpBlocked"] = BlockedList;


            HttpContext.Current.Session["isActBloChanged_Com"] = false;

            GridView.JSProperties["cpAllCount"] = StandardProvider.Count(x => true);
            GridView.JSProperties["cpInActivedUnBlockedCount"] = StandardProvider.Count(x => x.Actived == false && x.Blocked == false);
            GridView.JSProperties["cpActivedBlockedCount"] = StandardProvider.Count(x => x.Actived == true && x.Blocked == true);
            GridView.JSProperties["cpInactivedBlockedCount"] = StandardProvider.Count(x => x.Actived == false && x.Blocked == true);
            GridView.JSProperties["cpActivedUnBlockedCount"] = StandardProvider.Count(x => x.Actived == true && x.Blocked == false);
            GridView.JSProperties["cpUpdatedByCount"] = StandardProvider.Count(x => x.CreatedBy == User.Identity.Name || x.UpdatedBy == User.Identity.Name);
        }

        protected void uploader_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            string virtualPath = CommonConstants.STANDARD_PATH;
            string filePath = "";

            ASPxUploadControl uc = (ASPxUploadControl)sender;
            GridViewEditItemTemplateContainer container = uc.NamingContainer as GridViewEditItemTemplateContainer;
            if (container != null)
            {
                filePath = Page.MapPath(virtualPath);
                //// Kiem tra Folder tao Folder neu chua co
                if (!Directory.Exists(filePath)) // TODO: needs to be created ONLY from setup phase with Security Permissions
                    Directory.CreateDirectory(filePath);

                // process a file
                var name = e.UploadedFile.FileName;
                e.UploadedFile.SaveAs(Page.MapPath(virtualPath + name));

                //ASPxPageControl pageControl = GridView.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
                //ASPxMemo memo = pageControl.FindControl("notesEditor") as ASPxMemo;
                //memo.Text;

                e.CallbackData = virtualPath + name;
            }
            else
            {
            }
        }

        protected void GridView_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            if (e.NewValues["StdTypeId"] != null && e.NewValues["FileName"] != null)
            {
                string SrcFilePath = Page.MapPath(CommonConstants.STANDARD_PATH + e.NewValues["FileName"].ToString());
                string virtualDestPath = CommonConstants.STANDARD_PATH + e.NewValues["StdTypeId"] + "/";

                string DestFolderPath = Page.MapPath(virtualDestPath);
                //// Kiem tra Folder tao Folder neu chua co
                if (!Directory.Exists(DestFolderPath)) // TODO: needs to be created ONLY from setup phase with Security Permissions
                    Directory.CreateDirectory(DestFolderPath);

                string DestFilePath = Page.MapPath(virtualDestPath + e.NewValues["FileName"].ToString());

                // Ensure that the target does not exist.  
                if (File.Exists(SrcFilePath))
                {
                    if (File.Exists(DestFilePath))
                        File.Delete(DestFilePath);
                    // Move the file.  
                    File.Move(SrcFilePath, DestFilePath);
                    e.NewValues["URL"] = virtualDestPath + "/" + e.NewValues["FileName"];

                    if (e.OldValues["StdTypeId"] != null && e.OldValues["FileName"] != null)
                    {
                        string OldDestPath = Page.MapPath(CommonConstants.STANDARD_PATH + e.OldValues["StdTypeId"] + "/" + e.OldValues["FileName"]);
                        if (File.Exists(OldDestPath))
                            File.Delete(OldDestPath);
                    }
                }
            }
        }

        protected void uploader_Init(object sender, EventArgs e)
        {
            if (bool.Parse(HttpContext.Current.Session["isDetailRow_Com"].ToString()))
            {
                ASPxUploadControl editor = sender as ASPxUploadControl;
                GridViewEditItemTemplateContainer container = editor.NamingContainer as GridViewEditItemTemplateContainer;
                //editor.ClientVisible = container.Grid.IsNewRowEditing; // or use the Visible property
                editor.ClientEnabled = container.Grid.IsNewRowEditing;
            }

        }

        protected void UpdateButton_Init(object sender, EventArgs e)
        {
            if (bool.Parse(HttpContext.Current.Session["isDetailRow_Com"].ToString()))
            {
                ASPxButton editor = sender as ASPxButton;
                GridViewEditItemTemplateContainer container = editor.NamingContainer as GridViewEditItemTemplateContainer;
                //editor.ClientVisible = container.Grid.IsNewRowEditing; // or use the Visible property
                editor.ClientEnabled = container.Grid.IsNewRowEditing;
            }
        }
    }
}