<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MailMergeViaDocumentServer.aspx.cs" Inherits="RichEditDemoMailMergeViaDocumentServer" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentHolder" runat="Server">
    <dx:ASPxRichEdit ID="DemoRichEdit" runat="server"  ReadOnly="true" RibbonMode="None" ShowStatusBar="false"
        Settings-HorizontalRuler-Visibility="Hidden" ShowConfirmOnLosingChanges="false"
        Width="100%" Height="500px">
    </dx:ASPxRichEdit>
</asp:Content>
<asp:Content ID="ControlOptions" ContentPlaceHolderID="ControlOptionsTopHolder" runat="server">
    <div class="options">
        <div class="options-item">
            <dx:ASPxComboBox ID="cmbCity" runat="server" AutoPostBack="true" Caption="City"
                ValueType="System.String" SelectedIndex="0" Theme="MaterialCompactOrange">
                <Items>
                    <dx:ListEditItem Text="London" Value="London" />
                    <dx:ListEditItem Text="Seattle" Value="Seattle" />
                    <dx:ListEditItem Text="Tacoma" Value="Tacoma" />
                    <dx:ListEditItem Text="Kirkland" Value="Kirkland" />
                    <dx:ListEditItem Text="Redmond" Value="Redmond" />
                </Items>
            </dx:ASPxComboBox>
        </div>
    </div>
</asp:Content>
