<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DocVariable.aspx.cs"
    Inherits="RichEditDemoDocVariable" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentHolder" runat="Server">
    <dx:ASPxRichEdit ID="DemoRichEdit" runat="server" Width="100%" Height="700px" OnCalculateDocumentVariable="DemoRichEdit_CalculateDocumentVariable"
        ActiveTabIndex="4" ShowConfirmOnLosingChanges="false" OnPreRender="DemoRichEdit_PreRender">
    </dx:ASPxRichEdit>
</asp:Content>
