<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/Root.master" CodeBehind="Standard.aspx.cs" Inherits="vilas103.Standard" Title="Standard" %>

<%@ Register Assembly="DevExpress.Web.v20.1, Version=20.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data.Linq" TagPrefix="dx" %>

<asp:Content runat="server" ContentPlaceHolderID="Head">
    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/GridView.css") %>' />
    <script type="text/javascript" src='<%# ResolveUrl("~/Content/Scripts/CommonGridView.js") %>'></script>
    <script type="text/javascript" src='<%# ResolveUrl("~/Content/Scripts/Standard.js") %>'></script>

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
            <dx:MenuItem Enabled="false" Text="TIÊU CHUẨN">
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
        OnInitNewRow="GridView_InitNewRow" AutoGenerateColumns="False" OnRowValidating="GridView_RowValidating" OnCellEditorInitialize="GridView_CellEditorInitialize" OnHtmlRowPrepared="GridView_HtmlRowPrepared" OnStartRowEditing="GridView_StartRowEditing" OnEditFormLayoutCreated="GridView_EditFormLayoutCreated" OnDataBound="GridView_DataBound1" EnableTheming="True" OnHtmlRowCreated="GridView_HtmlRowCreated" Theme="Office2003Blue" OnBatchUpdate="GridView_BatchUpdate" OnCustomButtonCallback="GridView_CustomButtonCallback" OnContextMenuItemClick="GridView_ContextMenuItemClick" OnFillContextMenuItems="GridView_FillContextMenuItems" OnCustomJSProperties="GridView_CustomJSProperties" SettingsBehavior-AllowFocusedRow="True" SettingsBehavior-AllowSelectSingleRowOnly="False" OnCancelRowEditing="GridView_CancelRowEditing" OnCommandButtonInitialize="GridView_CommandButtonInitialize" OnContextMenuItemVisibility="GridView_ContextMenuItemVisibility1" OnRowDeleted="GridView_RowDeleted" OnRowInserted="GridView_RowInserted" OnRowUpdated="GridView_RowUpdated" OnRowUpdating="GridView_RowUpdating">
        <SettingsExport EnableClientSideExportAPI="True" PaperKind="A4">
        </SettingsExport>
        <EditFormLayoutProperties AlignItemCaptionsInAllGroups="true">
            <Items>
                <dx:GridViewTabbedLayoutGroup ColSpan="1">
                    <Items>
                        <dx:GridViewLayoutGroup Caption="Thông tin tiêu chuẩn" ColCount="2" ColSpan="1" ColumnCount="2">
                            <Items>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Id">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="StdTypeId">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="2" ColumnName="Name" ColumnSpan="2">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="2" ColumnName="Abstract" ColumnSpan="2">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="IssueDate">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="ValidDate">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="FileName">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem Caption="Chọn File" ColSpan="1">
                                    <Template>
                                        <dx:ASPxUploadControl ID="uploader" ClientInstanceName="uc" runat="server"
                                            UploadMode="Advanced"
                                            Width="280px"
                                            FileUploadMode="OnPageLoad"
                                            ShowProgressPanel="True"
                                            OnFileUploadComplete="uploader_FileUploadComplete" BrowseButton-Text="Chọn File" Theme="Office2003Blue" AdvancedModeSettings-FileListPosition="Bottom" RemoveButton-Text="Bỏ File" ClientSideEvents-TextChanged="onTextChanged" ClientSideEvents-FileUploadComplete="onFileUploadComplete" Native="False" ClientSideEvents-FileInputCountChanged="onFileInputCountChanged" OnInit="uploader_Init">
                                            <AdvancedModeSettings EnableFileList="False" EnableDragAndDrop="False" />
                                            <ValidationSettings MaxFileSize="4194304" AllowedFileExtensions=".jpg,.jpeg,.png,.doc,.pdf,.docx,.xls,.xlsx,.csv" />
                                            <BrowseButton Text="Chọn File">
                                            </BrowseButton>
                                            <RemoveButton Text="Bỏ File">
                                            </RemoveButton>
                                        </dx:ASPxUploadControl>
                                    </Template>
                                </dx:GridViewColumnLayoutItem>
                                
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="FeePrice">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="FeeDoc">
                                </dx:GridViewColumnLayoutItem>
                                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="LabID">
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
                <dx:GridViewLayoutGroup ColSpan="1" ColumnCount="3" GroupBoxDecoration="None">
                    <Items>
                        <dx:EmptyLayoutItem ColSpan="1">
                        </dx:EmptyLayoutItem>
                        <dx:EmptyLayoutItem ColSpan="1">
                        </dx:EmptyLayoutItem>
                        <dx:GridViewColumnLayoutItem ColSpan="1" HorizontalAlign="Center" ShowCaption="False">
                            <Template>
                                <dx:ASPxButton runat="server" ID="UpdateButton" Text="Cập nhật" CssClass="custom-btn" AutoPostBack="false" ForeColor="Black" OnInit="UpdateButton_Init">
                                    <ClientSideEvents Click="onUpdateBtnClick" />
                                </dx:ASPxButton>
                                <dx:ASPxButton runat="server" ID="CancelButton" Text="Hủy bỏ" CssClass="custom-btn" AutoPostBack="false" ForeColor="Black">
                                    <ClientSideEvents Click="onCancelBtnClick" />
                                </dx:ASPxButton>
                            </Template>
                        </dx:GridViewColumnLayoutItem>
                    </Items>
                </dx:GridViewLayoutGroup>
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
            <dx:GridViewDataTextColumn FieldName="LabID" VisibleIndex="12" ShowInCustomizationForm="True" Visible="False">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="CreatedBy" VisibleIndex="14" Visible="False" />
            <dx:GridViewDataDateColumn FieldName="CreatedDate" VisibleIndex="15" Visible="false" />
            <dx:GridViewDataTextColumn FieldName="UpdatedBy" VisibleIndex="16" Visible="false" />
            <dx:GridViewDataDateColumn FieldName="UpdatedDate" ShowInCustomizationForm="True" Visible="False" VisibleIndex="17">
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataCheckColumn FieldName="Actived" VisibleIndex="18" Visible="false">
            </dx:GridViewDataCheckColumn>
            <dx:GridViewDataCheckColumn FieldName="Blocked" VisibleIndex="19" Visible="false" />

            <dx:GridViewDataTextColumn FieldName="Id" Width="150px" VisibleIndex="2">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Name" Width="400px" VisibleIndex="3">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="URL" Visible="False" VisibleIndex="9">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="FileName" Visible="False" VisibleIndex="7">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="FeeDoc" Visible="False" VisibleIndex="10">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="FeePrice" Width="100px" VisibleIndex="11">
                <PropertiesTextEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:N0}">
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn FieldName="IssueDate" Width="100px" VisibleIndex="5">
                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" DisplayFormatInEditMode="True">
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataDateColumn FieldName="ValidDate" Width="100px" VisibleIndex="6">
                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" DisplayFormatInEditMode="True">
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataMemoColumn FieldName="Abstract" VisibleIndex="4" Visible="False">
            </dx:GridViewDataMemoColumn>

            <dx:GridViewDataComboBoxColumn FieldName="StdTypeId" VisibleIndex="1" Width="200px" Visible="False">
                <PropertiesComboBox DataSourceID="ObjectDataSourceStdType" TextField="Name" ValueField="Id">
                </PropertiesComboBox>
                <EditFormSettings Visible="False" />
            </dx:GridViewDataComboBoxColumn>

        </Columns>
        <SettingsBehavior AllowFocusedRow="true" AllowSelectByRowClick="true" ProcessColumnMoveOnClient="True" EnableCustomizationWindow="True" ConfirmDelete="True" />
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

    <asp:ObjectDataSource ID="GridViewDataSource" runat="server" DataObjectTypeName="Data.DataVMs.StandardVM"
        TypeName="Data.Providers.StandardProvider"
        SelectMethod="GetAll" UpdateMethod="Update" InsertMethod="Add"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceStdType" runat="server" DataObjectTypeName="Data.DataVMs.StdTypeVM"
        TypeName="Data.Providers.StdTypeProvider"
        SelectMethod="GetAll" UpdateMethod="Update" InsertMethod="Add">
        <SelectParameters>
            <asp:Parameter DefaultValue="true" Name="Actived" Type="Boolean" />
            <asp:Parameter DefaultValue="false" Name="Blocked" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <%--OnSelecting="GridViewDataSource_Selecting"--%>
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
