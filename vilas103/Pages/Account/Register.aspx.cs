using Data.DataModels;
using Data.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Web;
using vilas103.Model;

namespace vilas103 {
    public partial class RegisterModule : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
        }

        protected void RegisterButton_Click(object sender, EventArgs e) {
            // DXCOMMENT: Your Registration logic 
            
            ApplicationUserManager manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new ApplicationUser() {
                UserName = RegisterUserNameTextBox.Text,
                FullName = FullNameTextBox.Text,
                Email = EmailTextBox.Text,
            };
            IdentityResult result = manager.Create(user, PasswordButtonEdit.Text);
            if (result.Succeeded)
            {
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                string code = manager.GenerateEmailConfirmationToken(user.Id);
                string callbackUrl = IdentityHelpers.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");

                AuthHelper.SignIn(user.UserName, PasswordButtonEdit.Text, false);
                IdentityHelpers.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            }
            else
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }
    }
}