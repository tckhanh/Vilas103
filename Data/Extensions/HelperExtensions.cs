using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Data.Extensions
{
    public static class HelperExtensions
    {
        public static MvcHtmlString ClaimToString(this HtmlHelper helper, string type)
        {
            string returnItem = string.Empty;

            List<Claim> claims = GetClaims(helper);

            if (claims == null || claims.Count == 0)
            {
                throw new InvalidProgramException("There was a problem with the user passed in. It has no claims ");
            }
            else
            {
                Claim claim = claims.FirstOrDefault(c => c.Type == type);

                if (claim != null)
                {
                    returnItem = claim.Value;
                }
            }

            MvcHtmlString returnString = new MvcHtmlString(returnItem);

            return returnString;
        }

        private static List<Claim> GetClaims(HtmlHelper helper)
        {
            ClaimsPrincipal principal = helper.ViewContext.HttpContext.User as ClaimsPrincipal;
            if (principal == null)
            {
                throw new InvalidCastException("The current principal is not a claims principal.");
            }
            List<Claim> returnList = new List<Claim>();

            returnList = principal.Claims.ToList();

            return returnList;
        }
    }
}