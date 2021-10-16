<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="BuiltInMailMerge.aspx.cs"
    Inherits="RichEditDemoBuiltInMailMerge" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentHolder" runat="Server">
    <dx:ASPxRichEdit ID="DemoRichEdit" ActiveTabIndex="5"
        runat="server" Width="100%" Height="500px" ShowConfirmOnLosingChanges="false"
        OnPreRender="DemoRichEdit_PreRender">
    </dx:ASPxRichEdit>
</asp:Content>
