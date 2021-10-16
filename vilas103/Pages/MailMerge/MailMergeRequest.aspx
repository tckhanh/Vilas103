<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/Root.master" CodeBehind="MailMergeRequest.aspx.cs" Inherits="vilas103.MailMerge.MailMergeRequest" %>

<%@ Register Assembly="DevExpress.Web.ASPxRichEdit.v20.1, Version=20.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxRichEdit" TagPrefix="dx" %>

<asp:Content runat="server" ContentPlaceHolderID="Head">
    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/GridView.css") %>' />
    <script type="text/javascript" src='<%# ResolveUrl("~/MailMerge/MailMerge.js") %>'></script>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="PageContent" runat="server">
    <dx:ASPxRichEdit ID="DemoRichEdit" runat="server" ReadOnly="true" RibbonMode="None" ShowStatusBar="false"
        Settings-HorizontalRuler-Visibility="Hidden" ShowConfirmOnLosingChanges="false"
        Width="100%" Height="500px">
    </dx:ASPxRichEdit>
</asp:Content>
