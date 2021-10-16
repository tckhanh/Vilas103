using System;
using System.Web;
using System.Web.Security;
using System.Security.Principal;
using vilas103.Helper;

namespace Vilas103.Web
{

    /// <summary>Security Http Module</summary>

    public class SecurityHttpModule : IHttpModule
    {

        public SecurityHttpModule() { }

        /// <summary>Initializes a module and prepares

        /// it to handle requests.</summary>

        /// <param name="context" 
        /// >An <see cref="T:System.Web.HttpApplication" />

        /// that provides access to the methods, properties,

        /// and events common to all application objects within

        /// an ASP.NET application </param>

        public void Init(System.Web.HttpApplication context)
        {
            context.AcquireRequestState += new EventHandler(AcquireRequestState);
        }

        void context_PostAcquireRequestState(object sender, EventArgs e)
        {

        }

        /// <summary>Occurs when a security module

        /// has established the identity of the user.</summary>

        private void AcquireRequestState(Object sender, EventArgs e)
        {
            HttpApplication Application = (HttpApplication)sender;
            HttpRequest Request = Application.Context.Request;
            HttpResponse Response = Application.Context.Response;
            bool allow = false; // Default is not not allow

            SiteMapNode node;

            // Exit if we're on login.aspx,

            // not authenticated, or no siteMapNode exists.

            if (Request.Url.AbsolutePath.ToLower() ==
                FormsAuthentication.LoginUrl.ToLower()) return;
            if (HttpContext.Current.Session == null) return;

            Application.Context.User = HttpContext.Current.Session["User"] as UserInfo;
            node = ((DynamicSiteMapProvider)SiteMap.Provider).FindNode(Request.RawUrl);

            if (node == null) return;
            //if (node.ParentNode != null)
            //    if (Request.UrlReferrer != null)
            //        if (Request.UrlReferrer.AbsolutePath == node.ParentNode.Url)
            //        {
            //            allow = true;
            //        }
            // Check if user is in roles

            if (node.Roles.Count == 0)
            {
                allow = true; // No Roles found, so we allow.

            }
            else
            {
                // Loop through each role and check to see if user is in it.
                if (HttpContext.Current.User == null)
                    allow = false;
                else
                {
                    foreach (string role in node.Roles)
                    {
                        if (Application.Context.User.IsInRole(role)) { allow = true; break; }
                    }
                }
            }
            //check deny
            string strDeny = node["deny"];
            //check deny
            if (strDeny != null)
            {
                string[] lstDeny = strDeny.Split(',');
                for (int i = 0; i < lstDeny.Length; i++)
                {
                    if (lstDeny[i].Trim() == "*")
                    {
                        allow = false;
                        break;
                    }
                    if (Application.Context.User != null)
                    {
                        if (Application.Context.User.IsInRole(lstDeny[i].Trim()))
                        {
                            allow = false;
                            break;
                        }
                    }
                    else
                        if (lstDeny[i].Trim() == "?")
                        {
                            allow = false;
                            break;
                        }
                }
            }
            // Do we deny?
            if (allow == false)
                Response.Redirect(FormsAuthentication.LoginUrl);
        }

        /// <summary>Disposes of the resources (other than memory)

        /// used by the module that implements

        /// <see cref="T:System.Web.IHttpModule" />.</summary>

        public void Dispose() { }


        public string FileName
        {
            get
            {
                return System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath);
            }
        }
    }
}