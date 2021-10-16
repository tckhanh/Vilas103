using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.Web.Infrastructure.Helpers
{
    public enum AppStateKeys
    {
        COUNTER,
        LAST_REQUEST_TIME,
        LAST_REQUEST_URL
    };
    public static class AppStateHelpers
    {
        public static object Get(AppStateKeys key, object defaultValue = null)
        {
            string keyString = Enum.GetName(typeof(AppStateKeys), key);
            if (HttpContext.Current.Application[keyString] == null
            && defaultValue != null)
            {
                HttpContext.Current.Application[keyString] = defaultValue;
            }
            return HttpContext.Current.Application[keyString];
        }
        public static object Set(AppStateKeys key, object value)
        {
            return HttpContext.Current.Application[Enum.GetName(typeof(AppStateKeys),key)] = value;
        }
    }
}