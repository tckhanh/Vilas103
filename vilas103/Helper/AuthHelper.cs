using BTS.Web.Infrastructure.Helpers;
using Data.DataModels;
using Data.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using vilas103.Helper;

namespace vilas103.Model
{
    public static class AuthHelper
    {
        public static void saveLogInUserInfo(ClaimsIdentity item)
        {
            if (item != null)
            {
                var temp = new UserInfoVM()
                {
                    UserName = item.Name,
                    FullName = item.Name,
                    Email = item.Name,
                    //ImagePath = item.Claims.ImagePath,
                    //Roles = item.Roles.ToList().ToString()
                };
                HttpContext.Current.Session["User"] = item;
            }
        }


        public static bool SignIn(string userName, string password, bool RememberMeCheckBox)
        {
            // Validate the user password
            var manager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signinManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationSignInManager>();

            // This doen't count login failures towards account lockout
            // To enable password failures to trigger lockout, change to shouldLockout: true
            var result = signinManager.PasswordSignIn(userName, password, isPersistent: false, shouldLockout: true);

            switch (result)
            {
                case SignInStatus.Success:
                    CustomSignIn(userName, password, RememberMeCheckBox);
                    IdentityHelpers.RedirectToReturnUrl(HttpContext.Current.Request.QueryString["ReturnUrl"], HttpContext.Current.Response);
                    break;
                case SignInStatus.LockedOut:
                    HttpContext.Current.Response.Redirect("~/Account/Lockout.aspx");
                    break;
                case SignInStatus.RequiresVerification:
                    HttpContext.Current.Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn.aspx?ReturnUrl={0}&RememberMe={1}",
                                                    HttpContext.Current.Request.QueryString["ReturnUrl"],
                                                    false),
                                      true);
                    break;
                case SignInStatus.Failure:
                default:
                    return false;
            }
            return true;
        }

        public static bool CustomSignIn(string userName, string password, bool RememberMeCheckBox)
        {

            ApplicationUserManager manager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationSignInManager signinManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationSignInManager>();

            var user = manager.Find<ApplicationUser, string>(userName, password);

            if (user != null && user.Locked == false)
            {   // Validate the user password
                IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                //authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                ClaimsIdentity identity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                //Claims added to ClaimsIdentity getting lost in ASP.NET Core Identity System

                //identity.AddClaim(new Claim("FullName", user.FullName ?? ""));
                //identity.AddClaim(new Claim("Email", user.Email ?? ""));
                //identity.AddClaim(new Claim("ImagePath", user.ImagePath ?? ""));

                identity.AddClaims(new[] {
                    new Claim("FullName", user.FullName ?? ""),
                    new Claim("Email", user.Email ?? ""),
                    new Claim("ImagePath", user.ImagePath ?? ""),});

                AuthenticationProperties props = new AuthenticationProperties();
                props.IsPersistent = RememberMeCheckBox;
                authenticationManager.SignIn(props, identity);

                UserInfoVM userItem = new UserInfoVM()
                {
                    UserName = user.UserName ?? "",
                    FullName = user.FullName ?? "",
                    Email = user.Email ?? "",
                    ImagePath = user.ImagePath ?? "",
                    Roles = string.Join(";", identity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList()),
                    IsAuthenticated = identity.IsAuthenticated
                };
                HttpContext.Current.Session["User"] = userItem;

                //HttpContext.Current.Session["User"] = user;  // Mock user data



                // Nếu chọn lưu thông tin, đưa vào cookie
                if (RememberMeCheckBox == true)
                {
                    HttpContext.Current.Response.Cookies["USERNAME"].Value = userName;
                    HttpContext.Current.Response.Cookies["USERNAME"].Expires = DateTime.Now.AddMonths(1);
                    HttpContext.Current.Response.Cookies["PASSWORD"].Value = password;
                    HttpContext.Current.Response.Cookies["PASSWORD"].Expires = DateTime.Now.AddMonths(1);
                }
                // Nếu không chọn lưu thông tin, xóa cookie
                else
                {
                    HttpContext.Current.Response.Cookies["USERNAME"].Expires = DateTime.Now.AddMonths(-1);
                    HttpContext.Current.Response.Cookies["PASSWORD"].Expires = DateTime.Now.AddMonths(-1);
                }
                //chuyển đến trang chính của hệ thống
                //Context.User = mUserInfo;

                AuditHelpers.Log(0, identity.Name);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void SignOut()
        {
            HttpContext.Current.Session["User"] = null;
            IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            //authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);

            HttpContext.Current.Session.Clear();  // This may not be needed -- but can't hurt
            HttpContext.Current.Session.Abandon();

            // Clear authentication cookie
            HttpCookie rFormsCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            rFormsCookie.Expires = DateTime.Now.AddYears(-1);
            HttpContext.Current.Response.Cookies.Add(rFormsCookie);

            // Clear session cookie 
            HttpCookie rSessionCookie = new HttpCookie("ASP.NET_SessionId", "");
            rSessionCookie.Expires = DateTime.Now.AddYears(-1);
            HttpContext.Current.Response.Cookies.Add(rSessionCookie);
        }

        public static UserInfoVM getLogInUserInfo()
        {
            UserInfoVM userItem = (UserInfoVM)HttpContext.Current.Session["User"];
            if (userItem == null)
            {
                ClaimsIdentity userIdentity = (ClaimsIdentity)HttpContext.Current.User.Identity;
                userItem = new UserInfoVM()
                {
                    UserName = userIdentity.Name,
                    FullName = userIdentity.FindFirst("FullName")?.Value,
                    Email = userIdentity.FindFirst("Email")?.Value,
                    ImagePath = userIdentity.FindFirst("ImagePath")?.Value,
                    Roles = string.Join(";", userIdentity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList()),
                    IsAuthenticated = userIdentity.IsAuthenticated
                };
                HttpContext.Current.Session["User"] = userItem;
            }            
            return userItem;
        }

        public static string getStrLogInUserInfo()
        {
            return new JavaScriptSerializer().Serialize(getLogInUserInfo());
        }

        private static ApplicationUser CreateDefaultUser()
        {
            return new ApplicationUser
            {
                UserName = "JBell",
                FullName = "Julia",
                Email = "julia.bell@example.com",
                ImagePath = "~/Content/Photo/Julia_Bell.jpg"
            };
        }
    }
}