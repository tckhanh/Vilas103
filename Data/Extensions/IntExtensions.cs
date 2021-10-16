using System;
using System.Web;

namespace Data.Extensions
{
    public static class IntExtensions
    {
        public static string FomatStringNo(this int No, int Year, string PostText = "")
        {
            return No.ToString("000#") + "/" + Year + PostText;
        }

    }
}
