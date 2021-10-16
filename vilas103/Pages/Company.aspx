<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/Root.master" CodeBehind="Company.aspx.cs" Inherits="vilas103.Company" Title="Company" %>

<%@ Register Assembly="DevExpress.Web.v20.1, Version=20.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data.Linq" TagPrefix="dx" %>

<asp:Content runat="server" ContentPlaceHolderID="Head">
    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/GridView.css") %>' />
    <script type="text/javascript" src='<%# ResolveUrl("~/Content/Scripts/CommonGridView.js") %>'></script>
    <script type="text/javascript" src='<%# ResolveUrl("~/Content/Scripts/Company.js") %>'></script>
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
            <dx:MenuItem Enabled="false" Text="THÔNG TIN KHÁCH HÀNG">
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
                <dx:GridViewTabbedLayoutGroup ColSpan="1" ActiveTabIndex="1">
                    <Items>
                        <dx:GridViewLayoutGroup Caption="Thông tin đơn vị" ColCount="3" ColSpan="1" ColumnCount="3">
                            <Items>
                                <dx:GridViewColumnLayoutItem ColSpan="2" ColumnName="Name" ColumnSpan="2">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Id">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="3" ColumnName="Address" ColumnSpan="3">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="PhoneNo">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="FaxNo">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Contract">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="2" ColumnName="TaxName" ColumnSpan="2">
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
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="UserName" Name="UserName">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Password" Name="Password">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="ConfirmPassword" Name="ConfirmPassword">
                                </dx:GridViewColumnLayoutItem>
                            </Items>
                            <CellStyle Font-Bold="True">
                            </CellStyle>
                        </dx:GridViewLayoutGroup>
                        <dx:GridViewLayoutGroup Caption="Thông tin đăng nhập" ColCount="2" ColSpan="1" ColumnCount="2" Name="InfoTab">
                            <Items>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Actived">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Blocked">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="LastLoginDate">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="FailLoginDate">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="FailLoginTimes">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="BlockedDate">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="CreatedBy">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="CreatedDate">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="UpdatedBy">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="UpdatedDate">
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
                    <dx:GridViewCommandColumnCustomButton ID="Download" Image-ToolTip="Tai File dang ky" Image-Url="~/Content/Images/export.svg" Image-Width="10px">
                        <Image ToolTip="Tai File dang ky" Width="10px" Url="~/Content/Images/export.svg"></Image>
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="Id" VisibleIndex="1" Visible="false" EditFormSettings-Visible="False">
                <EditFormSettings Visible="False"></EditFormSettings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="3" Width="350px">
                <PropertiesTextEdit ClientInstanceName="EflName">
                    <ClientSideEvents LostFocus="EflNameLostFocus" />
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Id" VisibleIndex="2" Width="100px" />
            <dx:GridViewDataTextColumn FieldName="Address" VisibleIndex="4" Width="400px">
                <PropertiesTextEdit ClientInstanceName="EflAddress">
                    <ClientSideEvents LostFocus="EflAddressLostFocus" />
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="PhoneNo" VisibleIndex="5" Visible="false" />
            <dx:GridViewDataTextColumn FieldName="FaxNo" VisibleIndex="6" Visible="false" />
            <dx:GridViewDataTextColumn FieldName="ContactName" VisibleIndex="7" Visible="false" />
            <dx:GridViewDataTextColumn FieldName="ContactPhone" VisibleIndex="8" Visible="false" />
            <dx:GridViewDataTextColumn FieldName="TaxName" VisibleIndex="9" Visible="false">
                <PropertiesTextEdit>
                    <ValidationSettings ValidationGroup="TaxGroup">
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="TaxAddress" VisibleIndex="10" Visible="false">
                <PropertiesTextEdit>
                    <ValidationSettings ValidationGroup="TaxGroup">
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="TaxCode" VisibleIndex="11" Visible="false" />
            <dx:GridViewDataTextColumn FieldName="TaxEmail" VisibleIndex="12" Visible="false" />
            <dx:GridViewDataTextColumn FieldName="Contract" VisibleIndex="13" Visible="false" />
            <dx:GridViewDataTextColumn FieldName="UserName" VisibleIndex="14" ReadOnly="True" />
            <dx:GridViewDataCheckColumn FieldName="Actived" VisibleIndex="17" Visible="false" />
            <dx:GridViewDataCheckColumn FieldName="Blocked" VisibleIndex="18" Visible="false" />
            <dx:GridViewDataDateColumn FieldName="BlockedDate" VisibleIndex="19" Visible="false" />
            <dx:GridViewDataDateColumn FieldName="LastLoginDate" VisibleIndex="20" Visible="false" />
            <dx:GridViewDataDateColumn FieldName="FailLoginDate" VisibleIndex="21" Visible="false" />
            <dx:GridViewDataTextColumn FieldName="FailLoginTimes" VisibleIndex="22" Visible="false" />
            <dx:GridViewDataTextColumn FieldName="CreatedBy" VisibleIndex="23" Visible="false" />
            <dx:GridViewDataDateColumn FieldName="CreatedDate" VisibleIndex="24" Visible="false" />
            <dx:GridViewDataTextColumn FieldName="UpdatedBy" VisibleIndex="25" Visible="false" />
            <dx:GridViewDataDateColumn FieldName="UpdatedDate" VisibleIndex="26" Visible="false" />
            <dx:GridViewDataTextColumn FieldName="Password" VisibleIndex="15" Visible="False">
                <PropertiesTextEdit Password="True">
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ConfirmPassword" VisibleIndex="16" Visible="False">
                <PropertiesTextEdit Password="True">
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
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

    <asp:ObjectDataSource ID="GridViewDataSource" runat="server" DataObjectTypeName="Data.DataVMs.CompanyVM"
        TypeName="Data.Providers.CompanyProvider"
        SelectMethod="GetAll" UpdateMethod="Update" InsertMethod="AddSuitTransaction" DeleteMethod="DeleteSuitTransaction"></asp:ObjectDataSource>

    <%--OnSelecting="GridViewDataSource_Selecting"--%>

    <asp:ObjectDataSource ID="ContactsDataSource" runat="server" DataObjectTypeName=" vilas103.Model.Contact"
        TypeName=" vilas103.Model.DataProvider"
        SelectMethod="GetContacts"></asp:ObjectDataSource>


</asp:Content>

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
