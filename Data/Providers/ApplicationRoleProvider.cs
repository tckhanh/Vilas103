using Data.DataModels;
using Data.Infrastructure;
using Lib.Exceptions;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Providers
{
   public static class ApplicationRoleProvider
    {
        public static ApplicationRole Add(ApplicationRole appRole)
        {
            if (RepositoryBase<ApplicationRole>.CheckContains(x => x.Description == appRole.Description))
                throw new NameDuplicatedException("Tên không được trùng");
            return RepositoryBase<ApplicationRole>.Add(appRole);
        }

        public static void Delete(string id)
        {
            RepositoryBase<ApplicationRole>.DeleteMulti(x => x.Id == id);
        }

        public static IEnumerable<ApplicationRole> GetAll()
        {
            return RepositoryBase<ApplicationRole>.GetAll();
        }

        public static IEnumerable<ApplicationRole> GetAll(int page, int pageSize, out int totalRow, string filter = null)
        {
            var query = RepositoryBase<ApplicationRole>.GetAll();
            if (!string.IsNullOrEmpty(filter))
                query = query.Where(x => x.Description.Contains(filter));

            totalRow = query.Count();
            return query.OrderBy(x => x.Description).Skip(page * pageSize).Take(pageSize);
        }

        public static ApplicationRole GetDetail(string id)
        {
            return RepositoryBase<ApplicationRole>.GetSingleByCondition(x => x.Id == id);
        }


        public static void Update(ApplicationRole AppRole)
        {
            if (RepositoryBase<ApplicationRole>.CheckContains(x => x.Description == AppRole.Description && x.Id != AppRole.Id))
                throw new NameDuplicatedException("Tên không được trùng");
            RepositoryBase<ApplicationRole>.Update(AppRole);
        }

        public static IEnumerable<ApplicationRole> GetRolesByGroupId(string groupId)
        {
            var query = from g in RepositoryBase<ApplicationRole>.DbContext.Roles
                        join ug in RepositoryBase<ApplicationRole>.DbContext.ApplicationRoleGroups
                        on g.Id equals ug.RoleId
                        where ug.GroupId == groupId
                        select g;
            return query.Distinct().OrderBy(x => x.Name);
        }

        public static IEnumerable<ApplicationRole> GetLogicRolesByUserId(string userId)
        {
            var query = from g in RepositoryBase<ApplicationRole>.DbContext.Roles
                        join rg in RepositoryBase<ApplicationRole>.DbContext.ApplicationRoleGroups
                        on g.Id equals rg.RoleId
                        join ug in RepositoryBase<ApplicationRole>.DbContext.ApplicationUserGroups
                        on rg.GroupId equals ug.GroupId
                        where ug.UserId == userId
                        select g;
            return query.Distinct().OrderBy(x => x.Name);
        }

        public static async Task<IEnumerable<ApplicationRole>> GetRealRolesByUserId(string userId)
        {
            ApplicationUserManager _userManager = new ApplicationUserManager(new UserStore<ApplicationUser, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(RepositoryBase<ApplicationRole>.DbContext));
            ApplicationRoleManager _roleManager = new ApplicationRoleManager(new ApplicationRoleStore(RepositoryBase<ApplicationRole>.DbContext));
            var roles = await _userManager.GetRolesAsync(userId);
            return _roleManager.Roles.Where(x => roles.Contains(x.Name));
        }
    }
}