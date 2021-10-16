using BTS.Web.Infrastructure.Helpers;
using Data.DataModels;
using Data.Extensions;
using Data.Helpers;
using DevExpress.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Web;
using vilas103.Model;

namespace vilas103
{
    public partial class SignInModule : System.Web.UI.Page
    {
        string ReturnUrl;
        protected void Page_Load(object sender, EventArgs e)
        {
            ReturnUrl = Request["ReturnUrl"];
        }

        public void loadLicense()
        {
            try
            {
                string licenseFile = Server.MapPath("~/Content/license.key");
                Stimulsoft.Base.StiLicense.LoadFromFile(licenseFile);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

        }
        protected void SignInButton_Click(object sender, EventArgs e)
        {
            FormLayout.FindItemOrGroupByName("GeneralError").Visible = false;
            if (ASPxEdit.ValidateEditorsInContainer(this))
            {
                // DXCOMMENT: You Authentication logic
                if (!AuthHelper.SignIn(UserNameTextBox.Text, PasswordButtonEdit.Text, RememberMeCheckBox.Checked))
                {
                    GeneralErrorDiv.InnerText = "Tên đăng nhập hoặc mật khẩu không đúng.";
                    FormLayout.FindItemOrGroupByName("GeneralError").Visible = true;
                }
                else
                {
                    loadLicense();
                }
                    
            }
        }

        protected void _SignInButton_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Validate the user password
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

                // This doen't count login failures towards account lockout
                // To enable password failures to trigger lockout, change to shouldLockout: true
                var result = signinManager.PasswordSignIn(UserNameTextBox.Text, PasswordButtonEdit.Text, isPersistent: false, shouldLockout: false);

                switch (result)
                {
                    case SignInStatus.Success:
                        IdentityHelpers.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                        break;
                    case SignInStatus.LockedOut:
                        Response.Redirect("~/Account/Lockout.aspx");
                        break;
                    case SignInStatus.RequiresVerification:
                        Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn.aspx?ReturnUrl={0}&RememberMe={1}",
                                                        Request.QueryString["ReturnUrl"],
                                                        false),
                                          true);
                        break;
                    case SignInStatus.Failure:
                    default:
                        UserNameTextBox.ErrorText = "Invalid user";
                        UserNameTextBox.IsValid = false;
                        break;
                }
            }
        }
    }
}