using Data.Mappings;
using DevExpress.Web;
using Lib.Exceptions;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;

namespace vilas103
{
    public class Global_asax : System.Web.HttpApplication
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        void Application_Start(object sender, EventArgs e)
        {
            //foreach (var f in new DirectoryInfo(@"D:\Projects\vilas103").GetFiles("*.cs", SearchOption.AllDirectories))
            //{
            //    string s = File.ReadAllText(f.FullName);
            //    File.WriteAllText(f.FullName, s, Encoding.UTF8);
            //}

            ASPxWebControl.CallbackError += new EventHandler(Application_Error);

            //Log Exceptions Only
            //If you need to log exceptions only, implement an action that processes them.
            //DevExpress.XtraReports.Web.ClientControls.LoggerService.Initialize(ProcessException);

            DevExpress.XtraReports.Web.ClientControls.LoggerService.Initialize(new MyLoggerService());

            ASPxWebControl.BackwardCompatibility.EnableDataAnnotationAttributesSupport = true;

            // Chuyển sang chay ben Configuration method của Startup Class
            //AutoMapperConfiguration.Configure();
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            // ... 
            // Use HttpContext.Current to get a Web request processing helper
            HttpServerUtility server = HttpContext.Current.Server;
            Exception exception = server.GetLastError();
            if (exception is HttpUnhandledException || exception is TargetInvocationException)
            {
                exception = exception.InnerException;
                ASPxWebControl.SetCallbackErrorMessage(exception.Message);
            }

            // Log an exception
            logger.Error(exception);
            // Su dung de hien thi Loi o Trang Error.aspx khi muon hien thi loi o Server Side
            //AddToShowLog(exception.Message, exception.StackTrace);
        }

        void ProcessException(Exception ex, string message)
        {
            // Log exceptions here. For instance:
            System.Diagnostics.Debug.WriteLine("[{0}]: Exception occured. Message: '{1}'. Exception Details:\r\n{2}",
                DateTime.Now, message, ex);
        }

        void AddToShowLog(string message, string stackTrace)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(DateTime.Now.ToLocalTime().ToString());
            sb.AppendLine(message);
            sb.AppendLine();
            sb.AppendLine("Source File: " + HttpContext.Current.Request.RawUrl);
            sb.AppendLine();
            sb.AppendLine("Stack Trace: ");
            sb.AppendLine(stackTrace);
            for (int i = 0; i < 150; i++)
                sb.Append("-");
            sb.AppendLine();
            HttpContext.Current.Session["Log"] += sb.ToString();
            sb.AppendLine();
        }

        void Application_End(object sender, EventArgs e)
        {
            // Code that runs on application shutdown
        }


        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
        }
    }

    public class MyLoggerService : DevExpress.XtraReports.Web.ClientControls.LoggerService
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        //The LoggerService.Error method allows you to process critical exceptions that are raised on the server side.
        public override void Info(string message)
        {
            System.Diagnostics.Debug.WriteLine("[{0}]: Info: '{1}'.", DateTime.Now, message);
            logger.Info(message);
        }

        //The LoggerService.Info method enables you to log document-related errors and information messages,
        //such as “Document creation has been canceled”, “File ‘{ 0}’ was not deleted”, 
        //“The document creation has started” and so on.

        public override void Error(Exception exception, string message)
        {
            System.Diagnostics.Debug.WriteLine("[{0}]: Exception occured. Message: '{1}'. Exception Details:\r\n{2}",
                DateTime.Now, message, exception);
            //Exception ex = new Exception("thu nghiem Nlog");
            logger.Error(exception, message);
        }
    }
}