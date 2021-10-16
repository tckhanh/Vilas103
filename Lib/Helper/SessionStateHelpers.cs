using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.Web.Infrastructure.Helpers
{
    public static class SessionStateHelpers
    {
        public enum SessionStateKeys
        {
            USER_SESSION,
            CREDENTIALS_SESSION,
            CART_SESSION,
            CULTURE_SESSION
        };

        public static object Get(SessionStateKeys key)
        {
            string keyString = Enum.GetName(typeof(SessionStateKeys), key);
            return HttpContext.Current.Session[keyString];
        }
        public static object Set(SessionStateKeys key, object value)
        {
            string keyString = Enum.GetName(typeof(SessionStateKeys), key);
            return HttpContext.Current.Session[keyString] = value;
        }
    }
}