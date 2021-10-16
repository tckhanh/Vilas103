using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data.DataModels
{
    public class ApplicationRoleStore
  : RoleStore<ApplicationRole, string, ApplicationUserRole>,
  IQueryableRoleStore<ApplicationRole, string>,
  IRoleStore<ApplicationRole, string>, IDisposable
    {
        public ApplicationRoleStore()
            : base(new IdentityDbContext())
        {
            base.DisposeContext = true;
        }

        public ApplicationRoleStore(CFContext context)
            : base(context)
        {
        }
    }
}