<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="vilas103.Pages.ErrorPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:LinkButton ID="BackLinkButton" runat="server" Text="Back to Example" PostBackUrl="~/Default.aspx"></asp:LinkButton><br />
        <br />
        Error log:
    <dx:ASPxMemo ID="Memo" runat="server" Height="400px" Width="100%">
    </dx:ASPxMemo>
        <asp:LinkButton ID="ClearLinkButton" runat="server" Text="Clear" OnClick="ClearLinkButton_Click"></asp:LinkButton>
    </form>
</body>
</html>
