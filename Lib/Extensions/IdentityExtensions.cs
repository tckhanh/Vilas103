using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace Lib.Infrastructure.Extensions
{
    public static class IdentityExtensions
    {
        public static List<string> Roles(this ClaimsIdentity identity)
        {
            return identity.Claims
                           .Where(c => c.Type == ClaimTypes.Role)
                           .Select(c => c.Value)
                           .ToList();
        }

        public static string getImagePath(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("user");
            }
            var claimsIdentity = identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                return claimsIdentity.FindFirst("ImagePath")?.Value;
            }
            return null;
        }

        public static string getUserField(this IIdentity identity, string fieldName)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("user");
            }
            var claimsIdentity = identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                return claimsIdentity.FindFirst(fieldName)?.Value;
            }
            return null;
        }
    }
}