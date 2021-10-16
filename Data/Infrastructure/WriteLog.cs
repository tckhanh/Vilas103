﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.Web.Infrastructure.Core
{
    public class WriteLog
    {
        private static string PageName = HttpContext.Current.Handler.GetType().Name;
        private static log4net.ILog log = log4net.LogManager.GetLogger(PageName);

        public static void LogError(Exception ex)
        {
            log.Error(ex);
        }

        public static void LogWarning(Exception ex)
        {
            log.Warn(ex);
        }

        public static void LogInfo(Exception ex)
        {
            log.Info(ex);
        }

        public static void LogFatal(Exception ex)
        {
            log.Fatal(ex);
        }
    }
}