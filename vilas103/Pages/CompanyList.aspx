<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/Root.master" CodeBehind="CompanyList.aspx.cs" Inherits="vilas103.CompanyList" Title="CompanyList" %>

<%@ Register Assembly="DevExpress.Web.v20.1, Version=20.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data.Linq" TagPrefix="dx" %>

<asp:Content runat="server" ContentPlaceHolderID="Head">
    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/GridView.css") %>' />
    <script type="text/javascript" src='<%# ResolveUrl("~/Content/Scripts/Company.js") %>'></script>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="PageToolbar">
    <dx:ASPxMenu runat="server" ID="PageToolbar" ClientInstanceName="pageToolbar"
        ItemAutoWidth="false" ApplyItemStyleToTemplates="true" ItemWrap="false"
        AllowSelectItem="false" SeparatorWidth="0"
        Width="100%" CssClass="page-toolbar" OnItemClick="PageToolbar_ItemClick" Theme="Youthful">
        <ClientSideEvents ItemClick="onPageToolbarItemClick" />
        <SettingsAdaptivity Enabled="true" EnableAutoHideRootItems="true"
            EnableCollapseRootItemsToIcons="true" CollapseRootItemsToIconsAtWindowInnerWidth="600" />
        <ItemStyle CssClass="item" VerticalAlign="Middle" />
        <ItemImage Width="16px" Height="16px" />
        <Items>
            <dx:MenuItem Enabled="false">
                <Template>
                    <h1>THÔNG TIN KHÁCH HÀNG</h1>
                </Template>
            </dx:MenuItem>
            <dx:MenuItem Name="New" Text="Thêm" Alignment="Right" AdaptivePriority="2">
                <Image Url="~/Content/Images/add.svg" />
            </dx:MenuItem>
            <dx:MenuItem Name="Clone" Text="Nhân bản" Alignment="Right" AdaptivePriority="2">
                <Image Url="~/Content/Images/clone.svg" />
            </dx:MenuItem>
            <dx:MenuItem Name="Edit" Text="Sửa" Alignment="Right" AdaptivePriority="2">
                <Image Url="~/Content/Images/edit.svg" />
            </dx:MenuItem>
            <dx:MenuItem Name="Delete" Text="Xóa" Alignment="Right" AdaptivePriority="2">
                <Image Url="~/Content/Images/delete.svg" />
            </dx:MenuItem>
            <dx:MenuItem Name="Export" Text="Xuất" Alignment="Right" AdaptivePriority="2">
                <Image Url="~/Content/Images/export.svg" />
            </dx:MenuItem>
            <dx:MenuItem Name="ToggleFilterPanel" Text="" GroupName="Filter" Alignment="Right" AdaptivePriority="1">
                <Image Url="~/Content/Images/search.svg" UrlChecked="~/Content/Images/search-selected.svg" />
            </dx:MenuItem>
        </Items>
    </dx:ASPxMenu>
    <dx:ASPxPanel runat="server" ID="FilterPanel" ClientInstanceName="filterPanel"
        Collapsible="true" CssClass="filter-panel">
        <SettingsCollapsing ExpandEffect="Slide" AnimationType="Slide" ExpandButton-Visible="false" />
        <PanelCollection>
            <dx:PanelContent>
                <dx:ASPxButtonEdit runat="server" ID="SearchButtonEdit" ClientInstanceName="searchButtonEdit" ClearButton-DisplayMode="Always" Caption="Search" Width="100%" />
            </dx:PanelContent>
        </PanelCollection>
        <ClientSideEvents Expanded="onFilterPanelExpanded" Collapsed="adjustPageControls" />
    </dx:ASPxPanel>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="PageContent" runat="server">
    <dx:ASPxGridView runat="server" ID="GridView" ClientInstanceName="gridView"
        KeyFieldName="CompanyID" EnablePagingGestures="False"
        CssClass="grid-view" Width="100%"
        DataSourceID="GridViewDataSource"
        OnCustomCallback="GridView_CustomCallback"
        OnInitNewRow="GridView_InitNewRow" AutoGenerateColumns="False" OnRowValidating="GridView_RowValidating" OnCellEditorInitialize="GridView_CellEditorInitialize" OnHtmlRowPrepared="GridView_HtmlRowPrepared" OnStartRowEditing="GridView_StartRowEditing" OnEditFormLayoutCreated="GridView_EditFormLayoutCreated" OnDataBound="GridView_DataBound1" EnableTheming="True" OnHtmlRowCreated="GridView_HtmlRowCreated" Theme="SoftOrange" OnBatchUpdate="GridView_BatchUpdate" OnCustomButtonCallback="GridView_CustomButtonCallback">
        <SettingsExport EnableClientSideExportAPI="True" ExportSelectedRowsOnly="true" PaperKind="A4" >
        </SettingsExport>
        <EditFormLayoutProperties AlignItemCaptionsInAllGroups="true">
            <Items>
                <dx:GridViewTabbedLayoutGroup ColSpan="1">
                    <Items>
                        <dx:GridViewLayoutGroup Caption="Thông tin đơn vị" ColCount="3" ColSpan="1" ColumnCount="3">
                            <Items>
                                <dx:GridViewColumnLayoutItem ColSpan="2" ColumnName="CompanyName" ColumnSpan="2">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="FastCompanyName">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="3" ColumnName="Address" ColumnSpan="3">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="PhoneNo">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="FaxNo">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Contract">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="2" ColumnName="TaxCompanyName" ColumnSpan="2">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="TaxCode">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="3" ColumnName="TaxAddress" ColumnSpan="3">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="ContactName">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="ContactPhone">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="TaxEmail">
                                </dx:GridViewColumnLayoutItem>
                            </Items>
                            <CellStyle Font-Bold="True">
                            </CellStyle>
                        </dx:GridViewLayoutGroup>
                        <dx:GridViewLayoutGroup Caption="Thông tin đăng nhập" ColCount="3" ColSpan="1" ColumnCount="3">
                            <Items>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Actived">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Blocked">
                                </dx:GridViewColumnLayoutItem>
                                <dx:EmptyLayoutItem ColSpan="1">
                                </dx:EmptyLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="LastLoginDate">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="FailLoginDate">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="FailLoginTimes">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="BlockedDate">
                                </dx:GridViewColumnLayoutItem>
                            </Items>
                            <CellStyle Font-Bold="True">
                            </CellStyle>
                        </dx:GridViewLayoutGroup>
                    </Items>
                </dx:GridViewTabbedLayoutGroup>
                <dx:EditModeCommandLayoutItem ColSpan="1" HorizontalAlign="Right">
                </dx:EditModeCommandLayoutItem>
            </Items>
        </EditFormLayoutProperties>
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" SelectAllCheckboxMode="AllPages" Width="52px" ButtonRenderMode="Image" ShowSelectCheckbox="True">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="Download" Image-ToolTip="Tai File dang ky" Image-Url="~/Content/Images/export.svg" Image-Width="10px">
<Image ToolTip="Tai File dang ky" Width="10px" Url="~/Content/Images/export.svg"></Image>
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
            <%--            <dx:GridViewDataTextColumn FieldName="CompanyID" VisibleIndex="1" Visible="false" EditFormSettings-Visible="False">
                <EditFormSettings Visible="False"></EditFormSettings>
            </dx:GridViewDataTextColumn>--%>
            <dx:GridViewDataTextColumn FieldName="CompanyName" VisibleIndex="2" Width="400px">
                <PropertiesTextEdit ClientInstanceName="EflCompanyName">
                    <ClientSideEvents LostFocus="EflCompanyNameLostFocus" />
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="FastCompanyName" VisibleIndex="1" Width="100px" />
            <dx:GridViewDataTextColumn FieldName="Address" VisibleIndex="3" Width="400px">
                <PropertiesTextEdit ClientInstanceName="EflAddress">
                    <ClientSideEvents LostFocus="EflAddressLostFocus" />
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="PhoneNo" VisibleIndex="4" Visible="false" />
            <dx:GridViewDataTextColumn FieldName="FaxNo" VisibleIndex="5" Visible="false" />
            <dx:GridViewDataTextColumn FieldName="ContactName" VisibleIndex="6" Visible="false" />
            <dx:GridViewDataTextColumn FieldName="ContactPhone" VisibleIndex="7" Visible="false" />
            <dx:GridViewDataTextColumn FieldName="TaxCompanyName" VisibleIndex="8" Visible="false" >
                <PropertiesTextEdit>
                    <ValidationSettings ValidationGroup="TaxGroup">
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="TaxAddress" VisibleIndex="9" Visible="false">
                                <PropertiesTextEdit>
                    <ValidationSettings ValidationGroup="TaxGroup">
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="TaxCode" VisibleIndex="10" Visible="false" />
            <dx:GridViewDataTextColumn FieldName="TaxEmail" VisibleIndex="11" Visible="false" />
            <dx:GridViewDataTextColumn FieldName="Contract" VisibleIndex="12" Visible="false" />
            <dx:GridViewDataTextColumn FieldName="UserName" VisibleIndex="13" />
            <dx:GridViewDataCheckColumn FieldName="Actived" VisibleIndex="14" Visible="false"/>
            <dx:GridViewDataCheckColumn FieldName="Blocked" VisibleIndex="15" Visible="false" />
            <dx:GridViewDataDateColumn FieldName="BlockedDate" VisibleIndex="16" Visible="false" />
            <dx:GridViewDataDateColumn FieldName="LastLoginDate" VisibleIndex="17" Visible="false" />
            <dx:GridViewDataDateColumn FieldName="FailLoginDate" VisibleIndex="18" Visible="false" />
            <dx:GridViewDataTextColumn FieldName="FailLoginTimes" VisibleIndex="19" Visible="false" />
            <dx:GridViewDataTextColumn FieldName="CreateStaffID" VisibleIndex="20" Visible="false" />
            <dx:GridViewDataDateColumn FieldName="CreateDate" VisibleIndex="21" Visible="false" />
            <dx:GridViewDataTextColumn FieldName="ModifyStaffID" VisibleIndex="22" Visible="false" />
            <dx:GridViewDataDateColumn FieldName="ModifyDate" VisibleIndex="23" Visible="false" />
        </Columns>
        <SettingsBehavior AllowFocusedRow="true" AllowSelectByRowClick="true" AllowEllipsisInText="True" AllowDragDrop="false" />
        <SettingsSearchPanel CustomEditorID="SearchButtonEdit" />
        <SettingsEditing Mode="EditForm">
        </SettingsEditing>
        <Settings VerticalScrollBarMode="Hidden" HorizontalScrollBarMode="Auto" ShowHeaderFilterButton="true" ShowGroupPanel="True" />
        <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="800">
        </SettingsAdaptivity>
        <SettingsPager PageSize="15" EnableAdaptivity="true">
            <PageSizeItemSettings Visible="true"></PageSizeItemSettings>
        </SettingsPager>
        <SettingsCommandButton>
            <UpdateButton RenderMode="Button" Text="Cập nhật">
            </UpdateButton>
            <CancelButton RenderMode="Button" Text="Hủy bỏ">
            </CancelButton>
        </SettingsCommandButton>
        <Styles>
            <Header BackColor="#FFCC66" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
            </Header>
            <AlternatingRow Enabled="True">
            </AlternatingRow>
        </Styles>
        <ClientSideEvents BatchEditStartEditing="batchEditStartEditing" BeginCallback="beginCallback" Init="onGridViewInit" SelectionChanged="onGridViewSelectionChanged" CustomButtonClick="customButtonClick" />
    </dx:ASPxGridView>

    <asp:ObjectDataSource ID="GridViewDataSource" runat="server" DataObjectTypeName="Data.DataVMs.StandardTypeVM"
        TypeName="Data.Providers.StandardTypeProvider"
        SelectMethod="GetAll" UpdateMethod="Update" InsertMethod="Add" DeleteMethod="Delete"></asp:ObjectDataSource>

</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="LeftPanelContent">
    <h3 class="leftpanel-section section-caption">CHỌN LỌC</h3>
    <dx:ASPxNavBar runat="server" ID="FiltersNavBar" ClientInstanceName="filtersNavBar"
        AllowSelectItem="True" ShowGroupHeaders="False"
        Width="100%" CssClass="filters-navbar">
        <ItemStyle CssClass="item" />
        <Groups>
            <dx:NavBarGroup Text="Khách hàng đăng ký">
                <Items>
                    <dx:NavBarItem Text="Chờ phê duyệt" Selected="true" Name="Filter1" />
                    <dx:NavBarItem Text="Đang bị khoá" Name="Filter2" />
                    <dx:NavBarItem Text="Đã bị huỷ" Name="Filter3" />
                    <dx:NavBarItem Text="Đăng nhập bị lỗi" Name="Filter4" />
                    <dx:NavBarItem Text="Tất cả " Name="All" />
                </Items>
            </dx:NavBarGroup>
        </Groups>
        <ClientSideEvents ItemClick="onFiltersNavBarItemClick" />
    </dx:ASPxNavBar>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="RightPanelContent">
    <div class="settings-content">
        <h2>Settings</h2>
        <p>Place your content here</p>
    </div>
</asp:Content>
