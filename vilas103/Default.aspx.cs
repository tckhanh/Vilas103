using NLog;
using System;
using System.IO;

namespace vilas103 {
    public partial class Default : System.Web.UI.Page {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        protected void Page_Load(object sender, EventArgs e) {
            TextContent.InnerHtml = File.ReadAllText(Server.MapPath(@"~/App_Data/Overview.html"));

            TableOfContentsTreeView.DataBind();
            TableOfContentsTreeView.ExpandAll();

            //logger.Info("Hello world");

            //Exception ex = new Exception("thu nghiem Nlog");
            //logger.Error(ex, "Goodbye cruel world");
        }
    }
}