<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Root.master" CodeBehind="MailMerge.aspx.cs" Inherits="vilas103.MailMerge.MailMerge" %>

<%@ Register Assembly="DevExpress.Web.ASPxRichEdit.v20.1, Version=20.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxRichEdit" TagPrefix="dx" %>

<asp:Content runat="server" ContentPlaceHolderID="Head">
    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/GridView.css") %>' />
    <script type="text/javascript" src='<%# ResolveUrl("~/MailMerge/MailMerge.js") %>'></script>
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
    <h3 class="leftpanel-section section-caption">CHỌN LỌC</h3>
    <dx:ASPxFileManager ID="FileManager" runat="server" OnFileUploading="FileManager_FileUploading" OnLoad="FileManager_Load">
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
    </dx:ASPxFileManager>
</asp:Content>
