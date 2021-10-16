using Data.DataModels;
using Lib.Extensions;
using DevExpress.Web;
using System;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using vilas103.Model;
using vilas103.Helper;

namespace vilas103
{
    public partial class Root : MasterPage
    {
        public UserInfo mUserInfo
        {
            get
            {
                UserInfo _mUserInfo = Session["User"] as UserInfo;
                return _mUserInfo;
            }
        }
        public bool EnableBackButton { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!string.IsNullOrEmpty(Page.Header.Title))
                Page.Header.Title += " - ";
            Page.Header.Title = Page.Header.Title + " CNMN";

            Page.Header.DataBind();

            UpdateUserMenuItemsVisible();
            HideUnusedContent();
            UpdateUserInfo();
        }
        protected void HideUnusedContent()
        {
            LeftAreaMenu.Items[1].Visible = EnableBackButton;

            bool hasLeftPanelContent = HasContent(LeftPanelContent);
            LeftAreaMenu.Items.FindByName("ToggleLeftPanel").Visible = hasLeftPanelContent;
            LeftPanel.Visible = hasLeftPanelContent;

            bool hasRightPanelContent = HasContent(RightPanelContent);
            RightAreaMenu.Items.FindByName("ToggleRightPanel").Visible = hasRightPanelContent;
            RightPanel.Visible = hasRightPanelContent;

            bool hasPageToolbar = HasContent(PageToolbar);
            PageToolbarPanel.Visible = hasPageToolbar;
        }

        protected bool HasContent(Control contentPlaceHolder)
        {
            if (contentPlaceHolder == null) return false;

            ControlCollection childControls = contentPlaceHolder.Controls;
            if (childControls.Count == 0) return false;

            return true;
        }

        // SignIn/Register

        protected void UpdateUserMenuItemsVisible()
        {
            var isAuthenticated = Context.User.Identity.IsAuthenticated;
            RightAreaMenu.Items.FindByName("SignInItem").Visible = !isAuthenticated;
            RightAreaMenu.Items.FindByName("RegisterItem").Visible = !isAuthenticated;
            RightAreaMenu.Items.FindByName("MyAccountItem").Visible = isAuthenticated;
            RightAreaMenu.Items.FindByName("SignOutItem").Visible = isAuthenticated;
        }

        protected void UpdateUserInfo()
        {
            UserInfoVM objUserInfo = AuthHelper.getLogInUserInfo();

            if (objUserInfo != null)
            {
                //SiteMapDataSource1.SiteMapProvider = objUserInfo.IsNguoiDungCuaVu ? "userProvider" : "unitProvider";
                if (objUserInfo.IsAuthenticated)
                {
                    var myAccountItem = RightAreaMenu.Items.FindByName("MyAccountItem");
                    var userName = (ASPxLabel)myAccountItem.FindControl("UserNameLabel");
                    var email = (ASPxLabel)myAccountItem.FindControl("EmailLabel");
                    var accountImage = (HtmlGenericControl)RightAreaMenu.Items[0].FindControl("AccountImage");
                    userName.Text = string.Format("{0} ({1})", objUserInfo.UserName, objUserInfo.FullName);
                    email.Text = objUserInfo.Email;
                    accountImage.Attributes["class"] = "account-image";


                    if (string.IsNullOrEmpty(objUserInfo.ImagePath))
                    {
                        accountImage.InnerHtml = string.Format("{0}", objUserInfo.FullName[0]).ToUpper();
                    }
                    else
                    {
                        var avatarUrl = (HtmlImage)myAccountItem.FindControl("AvatarUrl");
                        avatarUrl.Attributes["src"] = ResolveUrl(objUserInfo.ImagePath);
                        accountImage.Style["background-image"] = ResolveUrl(objUserInfo.ImagePath);
                    }
                }
            }
            else
            {
            }           
        }

        protected void RightAreaMenu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            if (e.Item.Name == "SignOutItem")
            {
                AuthHelper.SignOut(); // DXCOMMENT: Your Signing out logic
                
                Response.Redirect("~/");
            }
        }

        protected void ApplicationMenu_ItemDataBound(object source, MenuItemEventArgs e)
        {
            e.Item.Image.Url = string.Format("~/Content/Images/{0}.svg", e.Item.Text.ToUnsignString());
            e.Item.Image.UrlSelected = string.Format("~/Content/Images/{0}-white.svg", e.Item.Text.ToUnsignString());
        }
    }
}