using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;

namespace BTS.Web.Infrastructure.Helpers
{
    public class IgnoreErrorPropertiesResolver : DefaultContractResolver
    {

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);
            if (new string[] {
                    "InputStream",
                    "Filter",
                    "Length",
                    "Position",
                    "ReadTimeout",
                    "WriteTimeout",
                    "LastActivityDate",
                    "LastUpdatedDate",
                    "Session"
                }.Contains(property.PropertyName))
            {
                property.Ignored = true;
            }
            return property;
        }
    }
    public static class RequestHelpers
    {
        public static string GetClientIpAddress(HttpRequest request)
        {
            try
            {
                var userHostAddress = request.UserHostAddress;

                // Attempt to parse.  If it fails, we catch below and return "0.0.0.0"
                // Could use TryParse instead, but I wanted to catch all exceptions
                IPAddress.Parse(userHostAddress);

                var xForwardedFor = request.ServerVariables["X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(xForwardedFor))
                    xForwardedFor = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                if (string.IsNullOrEmpty(xForwardedFor))
                    return userHostAddress;

                // Get a list of public ip addresses in the X_FORWARDED_FOR variable
                var publicForwardingIps = xForwardedFor.Split(',').Where(ip => !IsPrivateIpAddress(ip)).ToList();

                // If we found any, return the last one, otherwise return the user host address
                return publicForwardingIps.Any() ? publicForwardingIps.Last() : userHostAddress;
            }
            catch (Exception)
            {
                // Always return all zeroes for any failure (my calling code expects it)
                return "0.0.0.0";
            }
        }

        private static bool IsPrivateIpAddress(string ipAddress)
        {
            // http://en.wikipedia.org/wiki/Private_network
            // Private IP Addresses are: 
            //  24-bit block: 10.0.0.0 through 10.255.255.255
            //  20-bit block: 172.16.0.0 through 172.31.255.255
            //  16-bit block: 192.168.0.0 through 192.168.255.255
            //  Link-local addresses: 169.254.0.0 through 169.254.255.255 (http://en.wikipedia.org/wiki/Link-local_address)

            var ip = IPAddress.Parse(ipAddress);
            var octets = ip.GetAddressBytes();

            var is24BitBlock = octets[0] == 10;
            if (is24BitBlock) return true; // Return to prevent further processing

            var is20BitBlock = octets[0] == 172 && octets[1] >= 16 && octets[1] <= 31;
            if (is20BitBlock) return true; // Return to prevent further processing

            var is16BitBlock = octets[0] == 192 && octets[1] == 168;
            if (is16BitBlock) return true; // Return to prevent further processing

            var isLinkLocalAddress = octets[0] == 169 && octets[1] == 254;
            return isLinkLocalAddress;
        }

        //This will serialize the Request object based on the level that you determine
        public static string SerializeRequest(HttpRequest request, int AuditingLevel)
        {
            string[] keys;
            string RequestFormData = "";
            if (request.HttpMethod == "POST")
            {
                // The action is a POST.
                keys = request.Form.AllKeys;
                for (int i = 0; i < keys.Length; i++)
                {
                    RequestFormData += keys[i] + ": " + request.Form[keys[i]] + Environment.NewLine;
                }
            }
            else
            {
                keys = request.QueryString.AllKeys;
                for (int i = 0; i < keys.Length; i++)
                {
                    RequestFormData += keys[i] + ": " + request.QueryString.GetValues(keys[i]).FirstOrDefault() + "\n";
                }
            }

            //Using one of the many available Settings for the JsonSerializer
            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };

            switch (AuditingLevel)
            {
                //No Request Data will be serialized
                case 0:
                default:
                    return "";
                //Basic Request Serialization - just stores Data
                case 1:
                    return JsonConvert.SerializeObject(new { RequestFormData }, Formatting.Indented, jsonSerializerSettings);
                //Middle Level - Customize to your Preferences
                case 2:
                    string RequestCookiesData = "";
                    keys = request.Cookies.AllKeys;
                    for (int i = 0; i < keys.Length; i++)
                    {
                        RequestCookiesData += keys[i] + ": " + request.Cookies[keys[i]] + Environment.NewLine;
                    }

                    string RequesHeadersData = "";
                    keys = request.Headers.AllKeys;
                    for (int i = 0; i < keys.Length; i++)
                    {
                        RequesHeadersData += keys[i] + ": " + request.Headers[keys[i]] + Environment.NewLine;
                    }
                    return JsonConvert.SerializeObject(new { RequestFormData, RequestCookiesData, RequesHeadersData}, Formatting.Indented, jsonSerializerSettings);
                //Highest Level - Serialize the entire Request object
                case 3:
                    //We can't simply just Encode the entire request string due to circular references as well
                    //as objects that cannot "simply" be serialized such as Streams, References etc.
                    //return Json.Encode(request);
                    return JsonConvert.SerializeObject(request, Formatting.Indented);
            }
        }


    }
}