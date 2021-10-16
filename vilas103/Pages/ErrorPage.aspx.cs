using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace vilas103.Pages
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string errorMessage = ASPxWebControl.GetCallbackErrorMessage();
            Response.Output.Write(errorMessage);

            if (HttpContext.Current.Session["Log"] != null)
                Memo.Text = errorMessage + "_" + HttpContext.Current.Session["Log"].ToString();
        }
        protected void ClearLinkButton_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session["Log"] = "";
            Memo.Text = "";
        }
    }
}