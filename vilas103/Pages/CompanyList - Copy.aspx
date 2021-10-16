<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Root.master" CodeBehind="CompanyList.aspx.cs" Inherits="vilas103.CompanyList" Title="CompanyList" %>

<%@ Register Assembly="DevExpress.Web.v20.1, Version=20.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data.Linq" TagPrefix="dx" %>

<asp:Content runat="server" ContentPlaceHolderID="Head">
    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/GridView.css") %>' />
    <script type="text/javascript" src='<%# ResolveUrl("~/Content/Company.js") %>'></script>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="PageToolbar">
    <dx:ASPxMenu runat="server" ID="PageToolbar" ClientInstanceName="pageToolbar"
        ItemAutoWidth="false" ApplyItemStyleToTemplates="true" ItemWrap="false"
        AllowSelectItem="false" SeparatorWidth="0"
        Width="100%" CssClass="page-toolbar">
        <ClientSideEvents ItemClick="onPageToolbarItemClick" />
        <SettingsAdaptivity Enabled="true" EnableAutoHideRootItems="true"
            EnableCollapseRootItemsToIcons="true" CollapseRootItemsToIconsAtWindowInnerWidth="600" />
        <ItemStyle CssClass="item" VerticalAlign="Middle" />
        <ItemImage Width="16px" Height="16px" />
        <Items>
            <dx:MenuItem Enabled="false">
                <Template>
                    <h1>Grid View</h1>
                </Template>
            </dx:MenuItem>
            <dx:MenuItem Name="New" Text="New" Alignment="Right" AdaptivePriority="2">
                <Image Url="Content/Images/add.svg" />
            </dx:MenuItem>
            <dx:MenuItem Name="Edit" Text="Edit" Alignment="Right" AdaptivePriority="2">
                <Image Url="Content/Images/edit.svg" />
            </dx:MenuItem>
            <dx:MenuItem Name="Delete" Text="Delete" Alignment="Right" AdaptivePriority="2">
                <Image Url="Content/Images/delete.svg" />
            </dx:MenuItem>
            <dx:MenuItem Name="Export" Text="Export" Alignment="Right" AdaptivePriority="2">
                <Image Url="Content/Images/export.svg" />
            </dx:MenuItem>
            <dx:MenuItem Name="ToggleFilterPanel" Text="" GroupName="Filter" Alignment="Right" AdaptivePriority="1">
                <Image Url="Content/Images/search.svg" UrlChecked="Content/Images/search-selected.svg" />
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
        OnInitNewRow="GridView_InitNewRow" AutoGenerateColumns="False">
        <EditFormLayoutProperties>
            <Items>
                <dx:GridViewTabbedLayoutGroup ColSpan="1">
                    <Items>
                        <dx:GridViewLayoutGroup Caption="Thông tin đơn vị" ColCount="3" ColSpan="1" ColumnCount="3">
                            <Items>
                                <dx:GridViewColumnLayoutItem ColSpan="2" ColumnName="Company Name" ColumnSpan="2">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Fast Company Name">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="3" ColumnName="Address" ColumnSpan="3">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Phone No">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Fax No">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Contract">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="2" ColumnName="Tax Company Name" ColumnSpan="2">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Tax Code">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="3" ColumnName="Tax Address" ColumnSpan="3">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Contact Name">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Contact Phone">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Tax Email">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Username">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Password">
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
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Last Login Date">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Fail Login Date">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Fail Login Times">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Blocked Date">
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
            <dx:GridViewCommandColumn VisibleIndex="0" SelectAllCheckboxMode="AllPages" Width="52px" ShowSelectCheckbox="True">
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="CompanyID" VisibleIndex="1" Visible="false" EditFormSettings-Visible="False" >
<EditFormSettings Visible="False"></EditFormSettings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="CompanyName" VisibleIndex="3" EditFormSettings-VisibleIndex ="2" Width="400px" >
<EditFormSettings VisibleIndex="2"></EditFormSettings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="FastCompanyName" VisibleIndex="2" EditFormSettings-VisibleIndex ="1" Width="100px" >
<EditFormSettings VisibleIndex="1"></EditFormSettings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Address" VisibleIndex="4" Width="400px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="PhoneNo" VisibleIndex="5" Visible="false" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex ="4" >
<EditFormSettings Visible="True" VisibleIndex="4"></EditFormSettings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="FaxNo" VisibleIndex="6" Visible="false" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex ="5" >
<EditFormSettings Visible="True" VisibleIndex="5"></EditFormSettings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ContactName" VisibleIndex="7" Visible="false" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex ="6" >
<EditFormSettings Visible="True" VisibleIndex="6"></EditFormSettings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ContactPhone" VisibleIndex="8" Visible="false" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex ="7" >
<EditFormSettings Visible="True" VisibleIndex="7"></EditFormSettings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="TaxCompanyName" VisibleIndex="9" Visible="false" EditFormSettings-Visible="False">
<EditFormSettings Visible="False"></EditFormSettings>
            </dx:GridViewDataTextColumn> 
            <dx:GridViewDataTextColumn FieldName="TaxAddress" VisibleIndex="10" Visible="false" EditFormSettings-Visible="False">
<EditFormSettings Visible="False"></EditFormSettings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="TaxCode" VisibleIndex="11" Visible="false" EditFormSettings-Visible="False">
<EditFormSettings Visible="False"></EditFormSettings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="TaxEmail" VisibleIndex="12" Visible="false" >
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Contract" VisibleIndex="13" Visible="false" EditFormSettings-Visible="False">
<EditFormSettings Visible="False"></EditFormSettings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Username" VisibleIndex="14" Visible="false" EditFormSettings-Visible="False">
<EditFormSettings Visible="False"></EditFormSettings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Password" VisibleIndex="15" Visible="false" EditFormSettings-Visible="False">
<EditFormSettings Visible="False"></EditFormSettings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataCheckColumn FieldName="Actived" VisibleIndex="16" EditFormSettings-Visible="False">
<EditFormSettings Visible="False"></EditFormSettings>
            </dx:GridViewDataCheckColumn>
            <dx:GridViewDataCheckColumn FieldName="Blocked" VisibleIndex="17" Visible="false" EditFormSettings-Visible="False">
<EditFormSettings Visible="False"></EditFormSettings>
            </dx:GridViewDataCheckColumn>
            <dx:GridViewDataDateColumn FieldName="BlockedDate" VisibleIndex="18" Visible="false" EditFormSettings-Visible="False">
<EditFormSettings Visible="False"></EditFormSettings>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataDateColumn FieldName="LastLoginDate" VisibleIndex="19" Visible="false" EditFormSettings-Visible="False">
<EditFormSettings Visible="False"></EditFormSettings>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataDateColumn FieldName="FailLoginDate" VisibleIndex="20" Visible="false" EditFormSettings-Visible="False">
<EditFormSettings Visible="False"></EditFormSettings>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn FieldName="FailLoginTimes" VisibleIndex="21" Visible="false" EditFormSettings-Visible="False">
<EditFormSettings Visible="False"></EditFormSettings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="CreateStaffID" VisibleIndex="22" Visible="false" EditFormSettings-Visible="False">
<EditFormSettings Visible="False"></EditFormSettings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn FieldName="CreateDate" VisibleIndex="23" Visible="false" EditFormSettings-Visible="False">
<EditFormSettings Visible="False"></EditFormSettings>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn FieldName="ModifyStaffID" VisibleIndex="24" Visible="false" EditFormSettings-Visible="False">
<EditFormSettings Visible="False"></EditFormSettings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn FieldName="ModifyDate" VisibleIndex="25" Visible="false" EditFormSettings-Visible="False" >
<EditFormSettings Visible="False"></EditFormSettings>
            </dx:GridViewDataDateColumn>
        </Columns>
        <SettingsBehavior AllowFocusedRow="true" AllowSelectByRowClick="true" AllowEllipsisInText="True" AllowDragDrop="false" />
        <SettingsSearchPanel CustomEditorID="SearchButtonEdit" />
        <Settings VerticalScrollBarMode="Hidden" HorizontalScrollBarMode="Auto" ShowHeaderFilterButton="true" />
        <SettingsPager PageSize="15" EnableAdaptivity="true">
            <PageSizeItemSettings Visible="true"></PageSizeItemSettings>
        </SettingsPager>

    </dx:ASPxGridView>

    <asp:ObjectDataSource ID="GridViewDataSource" runat="server" DataObjectTypeName="Data.DataVMs.CompanyVM"
        TypeName="Data.Providers.CompanyProvider"
        SelectMethod="GetAll" UpdateMethod="Update"></asp:ObjectDataSource>

    <asp:ObjectDataSource ID="ContactsDataSource" runat="server" DataObjectTypeName=" vilas103.Model.Contact"
        TypeName=" vilas103.Model.DataProvider"
        SelectMethod="GetContacts"></asp:ObjectDataSource>


</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="LeftPanelContent">
    <h3 class="leftpanel-section section-caption">Filters</h3>
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
