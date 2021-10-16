<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/Root.master" CodeBehind="StdType.aspx.cs" Inherits="vilas103.StdType" Title="StdType" %>

<%@ Register Assembly="DevExpress.Web.v20.1, Version=20.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data.Linq" TagPrefix="dx" %>

<asp:Content runat="server" ContentPlaceHolderID="Head">
    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/GridView.css") %>' />
    <script type="text/javascript" src='<%# ResolveUrl("~/Content/Scripts/CommonGridView.js") %>'></script>
    <script type="text/javascript" src='<%# ResolveUrl("~/Content/Scripts/StdType.js") %>'></script>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="PageToolbar">
    <dx:ASPxMenu runat="server" ID="PageToolbar" ClientInstanceName="pageToolbar"
        ItemAutoWidth="false" ApplyItemStyleToTemplates="true" ItemWrap="false"
        AllowSelectItem="false" SeparatorWidth="0"
        Width="100%" CssClass="page-toolbar" OnItemClick="PageToolbar_ItemClick" Theme="Aqua" Font-Bold="True" ForeColor="#000099">
        <ClientSideEvents ItemClick="onPageToolbarItemClick" />
        <SettingsAdaptivity Enabled="true" EnableAutoHideRootItems="true"
            EnableCollapseRootItemsToIcons="true" CollapseRootItemsToIconsAtWindowInnerWidth="600" />
        <ItemStyle CssClass="item" VerticalAlign="Middle" Font-Bold="True" />
        <ItemImage Width="16px" Height="16px" />
        <Items>
            <dx:MenuItem Enabled="false" Text="LOẠI TIÊU CHUẨN">
                <ItemStyle Font-Bold="True" ForeColor="Red" Font-Size="X-Large" />

            </dx:MenuItem>
            <dx:MenuItem Name="NewRow" Text="Thêm" Alignment="Right" AdaptivePriority="2">
                <Image Url="~/Content/Images/add.svg" />
            </dx:MenuItem>
            <dx:MenuItem Name="CloneRow" Text="Nhân bản" Alignment="Right" AdaptivePriority="2">
                <Image Url="~/Content/Images/clone.svg" />
            </dx:MenuItem>
            <dx:MenuItem Name="DetailRow" Text="Chi tiết" Alignment="Right" AdaptivePriority="2">
                <Image Url="~/Content/Images/GridView.svg" />
            </dx:MenuItem>
            <dx:MenuItem Name="EditRow" Text="Sửa" Alignment="Right" AdaptivePriority="2">
                <Image Url="~/Content/Images/edit.svg" />
            </dx:MenuItem>
            <dx:MenuItem Name="DeleteRow" Text="Xóa" Alignment="Right" AdaptivePriority="2">
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
        KeyFieldName="Id" EnablePagingGestures="False"
        CssClass="grid-view" Width="100%"
        DataSourceID="GridViewDataSource"
        OnCustomCallback="GridView_CustomCallback"
        OnInitNewRow="GridView_InitNewRow" AutoGenerateColumns="False" OnRowValidating="GridView_RowValidating" OnCellEditorInitialize="GridView_CellEditorInitialize" OnHtmlRowPrepared="GridView_HtmlRowPrepared" OnStartRowEditing="GridView_StartRowEditing" OnEditFormLayoutCreated="GridView_EditFormLayoutCreated" OnDataBound="GridView_DataBound1" EnableTheming="True" OnHtmlRowCreated="GridView_HtmlRowCreated" Theme="Office2003Blue" OnBatchUpdate="GridView_BatchUpdate" OnCustomButtonCallback="GridView_CustomButtonCallback" OnContextMenuItemClick="GridView_ContextMenuItemClick" OnFillContextMenuItems="GridView_FillContextMenuItems" OnCustomJSProperties="GridView_CustomJSProperties" SettingsBehavior-AllowFocusedRow="True" SettingsBehavior-AllowSelectSingleRowOnly="False" OnCancelRowEditing="GridView_CancelRowEditing" OnCommandButtonInitialize="GridView_CommandButtonInitialize" OnContextMenuItemVisibility="GridView_ContextMenuItemVisibility1" OnRowDeleted="GridView_RowDeleted" OnRowInserted="GridView_RowInserted" OnRowUpdated="GridView_RowUpdated">
        <SettingsExport EnableClientSideExportAPI="True" PaperKind="A4">
        </SettingsExport>
        <EditFormLayoutProperties AlignItemCaptionsInAllGroups="true">
            <Items>
                <dx:GridViewTabbedLayoutGroup ColSpan="1">
                    <Items>
                        <dx:GridViewLayoutGroup Caption="Thông tin loại tiêu chuẩn" ColCount="2" ColSpan="1" ColumnCount="2">
                            <Items>
                                <dx:GridViewColumnLayoutItem ColSpan="2" ColumnName="Name">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Id">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Actived" Name="Actived">
                                </dx:GridViewColumnLayoutItem>
                            </Items>
                            <CellStyle Font-Bold="True">
                            </CellStyle>
                        </dx:GridViewLayoutGroup>
                        <dx:GridViewLayoutGroup Caption="Thông tin cập nhật" ColCount="2" ColSpan="1" ColumnCount="2" Name="InfoTab">
                            <Items>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="CreatedBy">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="CreatedDate">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="UpdatedBy">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="UpdatedDate">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="2" ColumnName="Blocked" ColumnSpan="2">
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
            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit">
            </SettingsAdaptivity>
        </EditFormLayoutProperties>
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" Width="52px" ButtonRenderMode="Image">
                <CustomButtons>
                    <%--<dx:GridViewCommandColumnCustomButton ID="Download" Image-ToolTip="Tai File dang ky" Image-Url="~/Content/Images/export.svg" Image-Width="10px">
                        <Image ToolTip="Tai File dang ky" Width="10px" Url="~/Content/Images/export.svg"></Image>
                    </dx:GridViewCommandColumnCustomButton>--%>
                </CustomButtons>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="Id" Width="200px" VisibleIndex="1" EditFormSettings-Visible="False" ShowInCustomizationForm="True">
<EditFormSettings Visible="False"></EditFormSettings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Name" Width="600px" VisibleIndex="2" ShowInCustomizationForm="True">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="CreatedBy" VisibleIndex="3" Visible="False" />
            <dx:GridViewDataDateColumn FieldName="CreatedDate" VisibleIndex="4" Visible="false" />
            <dx:GridViewDataTextColumn FieldName="UpdatedBy" VisibleIndex="5" Visible="false" />
            <dx:GridViewDataDateColumn FieldName="UpdatedDate" VisibleIndex="6" Visible="false" />
            <dx:GridViewDataCheckColumn FieldName="Actived" VisibleIndex="7" Visible="false">
            </dx:GridViewDataCheckColumn>
            <dx:GridViewDataCheckColumn FieldName="Blocked" VisibleIndex="8" Visible="false" />

        </Columns>
        <SettingsBehavior AllowFocusedRow="true" AllowSelectByRowClick="true" AllowEllipsisInText="True" ProcessColumnMoveOnClient="True" EnableCustomizationWindow="True" ConfirmDelete="True" />
        <SettingsSearchPanel CustomEditorID="SearchButtonEdit" />
        <SettingsEditing Mode="EditForm">
        </SettingsEditing>
        <Settings VerticalScrollBarMode="Hidden" HorizontalScrollBarMode="Auto" ShowHeaderFilterButton="true" ShowGroupPanel="True" />
        <SettingsContextMenu Enabled="True">
            <RowMenuItemVisibility>
                <ExportMenu Visible="True" />
            </RowMenuItemVisibility>
        </SettingsContextMenu>
        <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="800">
        </SettingsAdaptivity>
        <SettingsPager PageSize="15" EnableAdaptivity="true">
            <PageSizeItemSettings Visible="true"></PageSizeItemSettings>
        </SettingsPager>
        <SettingsResizing ColumnResizeMode="Control" />
        <SettingsCommandButton>
            <UpdateButton RenderMode="Button" Text="Cập nhật">
            </UpdateButton>
            <CancelButton RenderMode="Button" Text="Hủy bỏ">
            </CancelButton>
        </SettingsCommandButton>
        <FormatConditions>
            <dx:GridViewFormatConditionHighlight ApplyToRow="True" Expression="[Actived] = False And [Blocked] = False" FieldName="Actived" Format="Custom">
                <RowStyle Font-Bold="True" ForeColor="#009933" />
            </dx:GridViewFormatConditionHighlight>
            <dx:GridViewFormatConditionHighlight ApplyToRow="True" Expression="[Actived] = True And [Blocked] = True" Format="Custom" FieldName="Blocked">
                <RowStyle Font-Bold="True" Font-Italic="False" Font-Underline="False" ForeColor="#990000" />
            </dx:GridViewFormatConditionHighlight>
            <dx:GridViewFormatConditionHighlight ApplyToRow="True" Expression="[Actived] = False And [Blocked] = True" FieldName="Actived" Format="Custom">
                <RowStyle Font-Italic="True" Font-Strikeout="True" />
            </dx:GridViewFormatConditionHighlight>
            <dx:GridViewFormatConditionHighlight ApplyToRow="True" Expression="[Actived] = True And [Blocked] = False" FieldName="Actived" Format="Custom">
                <RowStyle ForeColor="#0000CC" />
            </dx:GridViewFormatConditionHighlight>
        </FormatConditions>
        <Styles>
            <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
            </Header>
            <AlternatingRow Enabled="True">
            </AlternatingRow>
        </Styles>
        <ClientSideEvents BatchEditStartEditing="batchEditStartEditing" BeginCallback="beginCallback" Init="onGridViewInit" SelectionChanged="onGridViewSelectionChanged" CustomButtonClick="customButtonClick" ContextMenuItemClick="OnContextMenuItemClick" ContextMenu="OnContextMenu" EndCallback="endCallback" FocusedRowChanged="onGridViewFocusedRowChanged" />
    </dx:ASPxGridView>

    <asp:ObjectDataSource ID="GridViewDataSource" runat="server" DataObjectTypeName="Data.DataVMs.StdTypeVM"
        TypeName="Data.Providers.StdTypeProvider"
        SelectMethod="GetAll" UpdateMethod="Update" InsertMethod="Add"></asp:ObjectDataSource>

    <%--OnSelecting="GridViewDataSource_Selecting"--%>
</asp:Content>

<%--<asp:Content runat="server" ContentPlaceHolderID="LeftPanelContent">
    <dx:ASPxNavBar runat="server" ID="FiltersNavBar" ClientInstanceName="filtersNavBar"
        AllowSelectItem="True" ShowGroupHeaders="true"
        Width="100%" CssClass="filters-navbar" Theme="Aqua" EnableTheming="True" Font-Bold="False">
        <GroupHeaderStyle Font-Bold="True" Font-Size="Larger" ForeColor="Red">
        </GroupHeaderStyle>
        <ItemStyle CssClass="item" Font-Bold="True" />
        <Groups>
            <dx:NavBarGroup Text="CHỌN LỌC" Expanded="True">
                <Items>
                    <dx:NavBarItem Text="Tất cả" Name="All" Selected="true" />
                    <dx:NavBarItem Text="Chưa kích hoạt" Name="InActivedUnBlocked" />
                    <dx:NavBarItem Text="Đang bị khoá" Name="ActivedBlocked" />
                    <dx:NavBarItem Text="Đã bị huỷ" Name="InactivedBlocked" />
                    <dx:NavBarItem Text="Đang sử dụng" Name="ActivedUnBlocked" />
                    <dx:NavBarItem Text="Do User cập nhật" Name="UpdatedBy" />
                </Items>
            </dx:NavBarGroup>
        </Groups>
        <ClientSideEvents ItemClick="onFiltersNavBarItemClick" />
    </dx:ASPxNavBar>
</asp:Content>--%>

<asp:Content runat="server" ContentPlaceHolderID="LeftPanelContent">
    <dx:ASPxNavBar runat="server" ID="ASPxNavBar1" ClientInstanceName="filtersNavBar"
        AllowSelectItem="True" ShowGroupHeaders="true"
        Width="100%" CssClass="filters-navbar" Theme="Aqua" EnableTheming="True" Font-Bold="False" DataSourceID="XmlDataSourceLeftNavBar">
        <GroupHeaderStyle Font-Bold="True" Font-Size="Larger" ForeColor="Red">
        </GroupHeaderStyle>
        <ItemStyle CssClass="item" Font-Bold="True" />
        <ClientSideEvents ItemClick="onFiltersNavBarItemClick" />
    </dx:ASPxNavBar>
    <asp:XmlDataSource ID="XmlDataSourceLeftNavBar" runat="server" DataFile="~/App_Data/LeftNavBar.xml" XPath="/groups/*"></asp:XmlDataSource>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="RightPanelContent">
    <div class="settings-content">
        <h2>Settings</h2>
        <p>Place your content here</p>
    </div>
</asp:Content>
