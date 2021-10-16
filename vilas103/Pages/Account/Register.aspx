<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/Root.master" CodeBehind="Register.aspx.cs" Inherits="vilas103.RegisterModule" Title="Register" %>

<asp:Content runat="server" ContentPlaceHolderID="Head">
    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/SignInRegister.css") %>' />
    <script type="text/javascript" src='<%# ResolveUrl("~/Content/Scripts/SignInRegister.js") %>'></script>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="PageContent" runat="server">
    <div class="formLayout-verticalAlign">
        <div class="formLayout-container">
            <dx:ASPxTabControl ID="SignInRegisterTabControl" runat="server" Width="100%" TabAlign="Justify" Paddings-Padding="0">
                <Tabs>
                    <dx:Tab Text="Sign In" NavigateUrl="SignIn.aspx"></dx:Tab>
                    <dx:Tab Text="Register" NavigateUrl="Register.aspx"></dx:Tab>
                </Tabs>
            </dx:ASPxTabControl>

            <dx:ASPxFormLayout runat="server" ID="FormLayout" ClientInstanceName="formLayout" CssClass="formLayout" UseDefaultPaddings="False">
                <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" />
                <SettingsItemCaptions Location="Top" />
                <Styles LayoutGroup-Cell-Paddings-Padding="0" LayoutItem-Paddings-PaddingBottom="8" >
<LayoutItem>
<Paddings PaddingBottom="8px"></Paddings>
</LayoutItem>

<LayoutGroup>
<Cell>
<Paddings Padding="0px"></Paddings>
</Cell>
</LayoutGroup>
                </Styles>
                <Items>
                    <dx:LayoutGroup ShowCaption="False" GroupBoxDecoration="None" Paddings-Padding="16">
<Paddings Padding="16px"></Paddings>
                        <Items>
                            <dx:LayoutItem Caption="User name">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox ID="RegisterUserNameTextBox" runat="server" Width="100%">
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="true" ErrorTextPosition="Bottom" ErrorDisplayMode="ImageWithText">
                                                <RequiredField IsRequired="true" ErrorText="User name is required" />
                                            </ValidationSettings>
                                            <ClientSideEvents Init="function(s, e){ s.Focus(); }" />
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Full Name">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox ID="FullNameTextBox" runat="server" Width="100%">
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="true" ErrorTextPosition="Bottom" ErrorDisplayMode="ImageWithText">
                                                <RequiredField IsRequired="true" ErrorText="Full name is required" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Email">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox ID="EmailTextBox" runat="server" Width="100%">
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="true" ErrorTextPosition="Bottom" ErrorDisplayMode="ImageWithText">
                                                <RegularExpression ErrorText="Invalid e-mail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                                <RequiredField IsRequired="true" ErrorText="Email is required" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Password">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxButtonEdit ID="PasswordButtonEdit" runat="server" ClientInstanceName="passwordButtonEdit" Width="100%" Password="true" ClearButton-DisplayMode="Never">

<ClearButton DisplayMode="Never"></ClearButton>

                                            <ButtonStyle Border-BorderWidth="0" Width="6" CssClass="eye-button" HoverStyle-BackColor="Transparent" PressedStyle-BackColor="Transparent">
<PressedStyle BackColor="Transparent"></PressedStyle>

<HoverStyle BackColor="Transparent"></HoverStyle>
                                            </ButtonStyle>
                                            <ButtonTemplate>
                                                <div></div>
                                            </ButtonTemplate>
                                            <Buttons>
                                                <dx:EditButton>
                                                </dx:EditButton>
                                            </Buttons>
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="true" ErrorTextPosition="Bottom" ErrorDisplayMode="ImageWithText">
                                                <RequiredField IsRequired="true" ErrorText="Password is required" />
                                            </ValidationSettings>
                                            <ClientSideEvents ButtonClick="onPasswordButtonEditButtonClick" Validation="onPasswordValidation" />
                                        </dx:ASPxButtonEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Confirm password">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxButtonEdit ID="ConfirmPasswordButtonEdit" runat="server" ClientInstanceName="confirmPasswordButtonEdit" Width="100%" Password="true" ClearButton-DisplayMode="Never">

<ClearButton DisplayMode="Never"></ClearButton>

                                            <ButtonStyle Border-BorderWidth="0" Width="6" CssClass="eye-button" HoverStyle-BackColor="Transparent" PressedStyle-BackColor="Transparent">
<PressedStyle BackColor="Transparent"></PressedStyle>

<HoverStyle BackColor="Transparent"></HoverStyle>
                                            </ButtonStyle>
                                            <ButtonTemplate>
                                                <div></div>
                                            </ButtonTemplate>
                                            <Buttons>
                                                <dx:EditButton>
                                                </dx:EditButton>
                                            </Buttons>
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="true" ErrorTextPosition="Bottom" ErrorDisplayMode="ImageWithText">
                                                <RequiredField IsRequired="true" ErrorText="Confirm your password" />
                                            </ValidationSettings>
                                            <ClientSideEvents ButtonClick="onPasswordButtonEditButtonClick" Validation="onPasswordValidation" />
                                        </dx:ASPxButtonEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="" ShowCaption="False">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="ErrorMessage" runat="server">
                                        </dx:ASPxLabel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                        </Items>
                    </dx:LayoutGroup>
                    <dx:LayoutGroup GroupBoxDecoration="HeadingLine" ShowCaption="False">
                        <Paddings PaddingTop="13" PaddingBottom="13" />
                        <GroupBoxStyle CssClass="formLayout-groupBox" />
                        <Items>
                            <dx:LayoutItem ShowCaption="False" HorizontalAlign="Center" Paddings-Padding="0">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxButton ID="RegisterButton" runat="server" Width="200" Text="Create an account" OnClick="RegisterButton_Click"></dx:ASPxButton>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>

<Paddings Padding="0px"></Paddings>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                </Items>
            </dx:ASPxFormLayout>

        </div>
    </div>

</asp:Content>
