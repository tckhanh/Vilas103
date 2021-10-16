using BTS.Data.Logs;
using BTS.Data.Repositories;
using BTS.Model.Models;
using BTS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace BTS.Web.Infrastructure.Helpers
{
    public static class AuditHelpers
    {
        public static void Log(int AuditingLevel, string UserName = "Anonymous")
        {
            //Stores the Request in an Accessible object
            var request = HttpContext.Current.Request;           

            //Generate the appropriate key based on the user's Authentication Cookie
            //This is overkill as you should be able to use the Authorization Key from
            //Forms Authentication to handle this. 
            HttpSessionState sessionValue = HttpContext.Current.Session;
            string sessionIdentifier = string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(sessionValue.SessionID.ToString())).Select(s => s.ToString("x2")));
            //string sessionIdentifier = string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(request.Cookies[FormsAuthentication.FormsCookieName].Value)).Select(s => s.ToString("x2")));

            //Generate an audit
            Audit audit = new Audit()
            {
                SessionID = sessionIdentifier,
                IPAddress = RequestHelpers.GetClientIpAddress(request),
                URLAccessed = request.RawUrl,
                TimeAccessed = DateTime.Now,
                UserName = (request.IsAuthenticated) ? HttpContext.Current.User.Identity.Name : UserName,
                //HttpContext.Current.Controller.GetType().Name,
                //HttpContext.Current.ActionDescriptor.ActionName,
                //HttpContext.Current.ActionDescriptor.GetParameters(),

                Data = RequestHelpers.SerializeRequest(request, AuditingLevel)
            };

            //Stores the Audit in the Database
            LogDbFactory _dbFactory = new LogDbFactory();
            AuditService _auditService = new AuditService(new AuditRepository(_dbFactory), new LogUnitOfWork(_dbFactory));
            _auditService.Create(audit);
            _auditService.Save();
        }
    }
}