using System;
using System.Web;

namespace Data.Extensions
{
    public static class StringExtensions
    {
        public static bool IsLocalURL(this string _url)
        {
            bool flag = false;
            try
            {
                var url = new Uri(_url);
                var ctx = HttpContext.Current;
                if (url.Host.Equals(ctx.Request.Url.Host) && url.Port.Equals(ctx.Request.Url.Port))
                    flag = true;
            }
            catch { }
            return flag;
        }

        public static string Left(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            maxLength = Math.Abs(maxLength);

            return (value.Length <= maxLength
                   ? value
                   : value.Substring(0, maxLength)
                   );
        }
    }
}
