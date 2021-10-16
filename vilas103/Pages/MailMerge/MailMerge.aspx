<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/Root.master" CodeBehind="MailMerge.aspx.cs" Inherits="vilas103.MailMerge.MailMerge" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v20.1, Version=20.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxRichEdit.v20.1, Version=20.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxRichEdit" TagPrefix="dx" %>

<asp:Content runat="server" ContentPlaceHolderID="Head">
    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/GridView.css") %>' />
        <script type="text/javascript" src='<%# ResolveUrl("~/Content/Scripts/MailMergeTreeList.js") %>'></script>

    <script type="text/javascript">
        
    </script>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="PageContent" runat="server">
    <div class="text-content" runat="server" id="TextContent">
        <dx:ASPxRichEdit ID="DemoRichEdit" runat="server" Width="100%" Height="600px" ShowConfirmOnLosingChanges="False" OnPreRender="DemoRichEdit_PreRender" WorkDirectory="~\App_Data\WorkDirectory" ClientInstanceName="DemoRichEdit" OnCallback="DemoRichEdit_Callback">
            <Settings>
                <AutoCorrect DetectUrls="True" ReplaceTextAsYouType="True"></AutoCorrect>

                <RangePermissions Visibility="Auto"></RangePermissions>
            </Settings>
            <SettingsDocumentSelector>
                <FileListSettings View="Details">
                </FileListSettings>
            </SettingsDocumentSelector>
            <ClientSideEvents CallbackError="OnExceptionOccurred" EndCallback="DemoRichEditEndCallback" />
        </dx:ASPxRichEdit>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="LeftPanelContent">
    <h3 class="leftpanel-section section-caption">THAO TÁC</h3>
    <dx:ASPxTreeList runat="server" Width="100%" ID="tree" ClientInstanceName="tree"
        OnNodeInserting="tree_NodeInserting" OnVirtualModeCreateChildren="tree_VirtualModeCreateChildren"
        OnVirtualModeNodeCreating="tree_VirtualModeNodeCreating" OnCustomJSProperties="tree_CustomJSProperties"
        OnNodeUpdating="tree_NodeUpdating" OnNodeDeleting="tree_NodeDeleting" OnNodeValidating="tree_NodeValidating"
        OnCustomCallback="tree_CustomCallback" OnProcessDragNode="tree_ProcessDragNode"
        OnCustomNodeSort="tree_CustomNodeSort" AutoGenerateColumns="False">
        <Columns>
            <dx:TreeListTextColumn FieldName="Name" SortIndex="0" SortOrder="Ascending">
                <CellStyle>
                    <Paddings Padding="1" />
                </CellStyle>
                <EditCellStyle>
                    <Paddings Padding="1" />
                </EditCellStyle>
                <DataCellTemplate>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 20px">
                                <dx:ASPxImage runat="server" Width="21" Height="21" ImageUrl='<%# GetNodeGlyph(Container) %>'
                                    ImageAlign="Top" />
                            </td>
                            <td>
                                <%# Eval("Name") %>
                            </td>
                        </tr>
                    </table>
                </DataCellTemplate>
                <EditCellTemplate>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 20px">
                                <dx:ASPxImage ID="ASPxImage1" runat="server" Width="21" Height="21" ImageUrl='<%# GetNodeGlyph(Container) %>' ImageAlign="Top" />
                            </td>
                            <td>
                                <dx:ASPxTextBox runat="server" ID="ed" Text='<%# Bind("Name") %>' Width="200px">
                                    <ClientSideEvents Init="editor_Init" KeyPress="editor_KeyPress" LostFocus="editor_LostFocus" />
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </EditCellTemplate>
            </dx:TreeListTextColumn>
        </Columns>
        <Toolbars>
            <dx:TreeListToolbar>
                <SettingsAdaptivity Enabled="true" EnableCollapseRootItemsToIcons="true" />
                <Items>
                    <dx:TreeListToolbarItem Command="New" Text="New Folder" Image-IconID="businessobjects_bofolder_16x16" DisplayMode="Image" >
<Image IconID="businessobjects_bofolder_16x16"></Image>
                    </dx:TreeListToolbarItem>
                    <dx:TreeListToolbarItem Command="NewRoot" Text="Add Root" Image-IconID="actions_add_16x16office2013" DisplayMode="Image" >
<Image IconID="actions_add_16x16office2013"></Image>
                    </dx:TreeListToolbarItem>
                    <dx:TreeListToolbarItem Command="Delete" Text="Delete" DisplayMode="Image" />
                    <dx:TreeListToolbarItem Command="Edit" Text="Rename" DisplayMode="Image" />
                    <dx:TreeListToolbarItem Name="Upload" ItemStyle-CssClass="uploadItemStyle" Alignment="Left" AdaptivePriority="0" DisplayMode="Text" >
<ItemStyle CssClass="uploadItemStyle"></ItemStyle>
                        <Template>
                            <dx:ASPxUploadControl runat="server" ID="uploader" ClientInstanceName="uploader" UploadMode="Advanced" AutoStartUpload="true" OnFileUploadComplete="uploader_FileUploadComplete" ShowTextBox="False" ShowUploadButton="True">
                                <ClientSideEvents fileuploadcomplete="uploader_Complete" />
                            </dx:ASPxUploadControl>
                        </Template>
                    </dx:TreeListToolbarItem>
                </Items>
            </dx:TreeListToolbar>
        </Toolbars>
        <ClientSideEvents Init="tree_Init" FocusedNodeChanged="tree_FocusChanged" BeginCallback="tree_BeginCallback"
            EndCallback="tree_EndCallback" NodeClick="tree_NodeClick" StartDragNode="tree_StartDragNode"
            EndDragNode="tree_EndDragNode" CallbackError="OnExceptionOccurred" />
        <Settings ShowColumnHeaders="False" />
        <SettingsBehavior AllowFocusedNode="true" />
        <SettingsEditing AllowNodeDragDrop="true" AllowRecursiveDelete="true" />
    </dx:ASPxTreeList>
<%--    <dx:ASPxFileManager ID="FileManager" runat="server" OnFileUploading="FileManager_FileUploading" OnLoad="FileManager_Load">
        <ClientSideEvents CallbackError="OnExceptionOccurred" SelectedFileChanged="OnSelectedFileChanged" />
        <Settings RootFolder="~/" ThumbnailFolder="~/Thumb/" AllowedFileExtensions=".docx,.doc,.rtf,.txt" />
        <SettingsEditing AllowDownload="True" />
        <SettingsToolbar>
            <Items>
                <dx:FileManagerToolbarRefreshButton ToolTip="Refresh">
                </dx:FileManagerToolbarRefreshButton>
                <dx:FileManagerToolbarDownloadButton ToolTip="Download">
                </dx:FileManagerToolbarDownloadButton>
                <dx:FileManagerToolbarUploadButton ToolTip="Upload">
                </dx:FileManagerToolbarUploadButton>
            </Items>
        </SettingsToolbar>
    </dx:ASPxFileManager>--%>
</asp:Content>
