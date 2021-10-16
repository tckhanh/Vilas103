using AutoMapper;
using Data.DataModels;
using Data.DataVMs;
using Data.Infrastructure;
using Lib.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;

namespace Data.Providers
{
    public static class ApplicationGroupProvider
    {
        public static ApplicationGroup Add(ApplicationGroup appGroup)
        {
            if (RepositoryBase<ApplicationGroup>.CheckContains(x => x.Name == appGroup.Name))
                throw new NameDuplicatedException("Tên không được trùng");
            ApplicationGroup item = RepositoryBase<ApplicationGroup>.Add(appGroup);
            RepositoryBase<ApplicationGroup>.SaveChanges();
            return item;
        }

        public static ApplicationGroup Delete(string Id)
        {
            var appGroup = RepositoryBase<ApplicationGroup>.GetSingleById(Id);
            ApplicationGroup item = RepositoryBase<ApplicationGroup>.Delete(appGroup);
            RepositoryBase<ApplicationGroup>.SaveChanges();
            return item;
        }

        public static IEnumerable<ApplicationGroupVM> GetAll()
        {
            RepositoryBase<ApplicationGroup>.GetAll();
            List<ApplicationGroup> model = RepositoryBase<ApplicationGroup>.GetAll().ToList<ApplicationGroup>();
            //return Mapping.Mapper.Map<IEnumerable<CompanyVM>>(model);
            return Mapper.Map<IEnumerable<ApplicationGroupVM>>(model);
        }

        public static IEnumerable<ApplicationGroup> GetAll(int page, int pageSize, out int totalRow, string filter = null)
        {
            var query = RepositoryBase<ApplicationGroup>.GetAll();
            if (!string.IsNullOrEmpty(filter))
                query = query.Where(x => x.Name.Contains(filter));

            totalRow = query.Count();
            return query.OrderBy(x => x.Name).Skip(page * pageSize).Take(pageSize);
        }

        public static ApplicationGroup GetDetail(string Id)
        {
            return RepositoryBase<ApplicationGroup>.GetSingleById(Id);
        }

        public static void Update(ApplicationGroup appGroup)
        {
            if (RepositoryBase<ApplicationGroup>.CheckContains(x => x.Name == appGroup.Name && x.Id != appGroup.Id))
                throw new NameDuplicatedException("Tên không được trùng");
            RepositoryBase<ApplicationGroup>.Update(appGroup);
        }



        public static bool RemoveUserFromGroups(string userId)
        {
            RepositoryBase<ApplicationUserGroup>.DeleteMulti(x => x.UserId == userId);
            RepositoryBase<ApplicationUserGroup>.SaveChanges();
            return true;
        }

        public static bool RemoveUsersFromGroup(string groupId)
        {
            RepositoryBase<ApplicationUserGroup>.DeleteMulti(x => x.GroupId == groupId);
            RepositoryBase<ApplicationUserGroup>.SaveChanges();
            return true;
        }

        public static bool AddUserToGroups(IEnumerable<ApplicationGroup> groups, string userId)
        {
            foreach (var group in groups)
            {
                RepositoryBase<ApplicationUserGroup>.Add(new ApplicationUserGroup()
                {
                    GroupId = group.Id,
                    UserId = userId,
                });
            }
            RepositoryBase<ApplicationUserGroup>.SaveChanges();
            return true;
        }

        private static bool AddUserToGroup(ApplicationGroup group, string userId)
        {
            ApplicationUserGroup appUserGroup = new ApplicationUserGroup()
            {
                GroupId = group.Id,
                UserId = userId
            };
            if (RepositoryBase<ApplicationUserGroup>.GetSingleByCondition(x => x.GroupId == appUserGroup.GroupId && x.UserId == appUserGroup.UserId) == null)
            {
                RepositoryBase<ApplicationUserGroup>.Add(appUserGroup);
                RepositoryBase<ApplicationUserGroup>.SaveChanges();
            }
            return true;
        }

        public static bool AddUserToGroupSuitTransaction(ApplicationGroup group, string userId)
        {
            using (DbContextTransaction transaction = RepositoryBase<ApplicationGroup>.DbContext.Database.BeginTransaction())
            {
                try
                {
                    AddUserToGroup(group, userId);
                    ApplicationUserProvider.removeRolesFromUser(userId);
                    ApplicationUserProvider.AssignRolesToUser(userId, ApplicationRoleProvider.GetLogicRolesByUserId(userId));
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Trace.WriteLine("Error occurred:" + ex.Message);
                    throw;
                }
                return true;
            }
        }

        public static bool AddUserToGroupSuit(ApplicationGroup group, string userId)
        {
            AddUserToGroup(group, userId);
            ApplicationUserProvider.removeRolesFromUser(userId);
            ApplicationUserProvider.AssignRolesToUser(userId, ApplicationRoleProvider.GetLogicRolesByUserId(userId));
            return true;
        }

        public static bool AddUserToGroupsSuit(IEnumerable<ApplicationGroup> groups, string userId)
        {
            AddUserToGroups(groups, userId);
            ApplicationUserProvider.removeRolesFromUser(userId);
            ApplicationUserProvider.AssignRolesToUser(userId, ApplicationRoleProvider.GetLogicRolesByUserId(userId));
            return true;
        }

        private static bool AddRoleGroups(IEnumerable<ApplicationRoleGroup> roleGroups)
        {
            foreach (var roleGroup in roleGroups)
            {
                RepositoryBase<ApplicationRoleGroup>.Add(roleGroup);
            }
            RepositoryBase<ApplicationRoleGroup>.SaveChanges();
            return true;
        }

        public static bool AddRoleGroupsSuit(IEnumerable<ApplicationRoleGroup> roleGroups)
        {
            AddRoleGroups(roleGroups);
            return true;
        }

        public static bool AddRolesToGroup(IEnumerable<ApplicationRole> roles, string groupId)
        {
            foreach (var role in roles)
            {
                RepositoryBase<ApplicationRoleGroup>.Add(new ApplicationRoleGroup()
                {
                    RoleId = role.Id,
                    GroupId = groupId
                });
            }
            RepositoryBase<ApplicationRoleGroup>.SaveChanges();
            return true;
        }

        public static IEnumerable<ApplicationGroup> GetGroupsByUserId(string userId)
        {
            var query = from g in RepositoryBase<ApplicationGroup>.DbContext.ApplicationGroups
                        join ug in RepositoryBase<ApplicationGroup>.DbContext.ApplicationUserGroups
                        on g.Id equals ug.GroupId
                        where ug.UserId == userId
                        select g;
            return query;
        }


        public static IEnumerable<ApplicationGroup> GetGroupsByUser(ApplicationUser user)
        {
            return GetGroupsByUserId(user.Id);
        }
        public static ApplicationGroup GetByID(string Id)
        {
            return RepositoryBase<ApplicationGroup>.GetSingleById(Id);
        }

        public static ApplicationGroup GetByName(string Name)
        {
            return RepositoryBase<ApplicationGroup>.GetSingleByCondition(x => x.Name == Name);
        }

        public static IEnumerable<ApplicationGroup> GetGroupsByRoleId(string roleId)
        {

            var query = from g in RepositoryBase<ApplicationGroup>.DbContext.ApplicationGroups
                        join rg in RepositoryBase<ApplicationGroup>.DbContext.ApplicationRoleGroups
                        on g.Id equals rg.GroupId
                        where rg.RoleId == roleId
                        select g;
            return query;
        }

        public static bool DeleteRoleGroupByGroup(string groupId)
        {
            RepositoryBase<ApplicationRoleGroup>.DeleteMulti(x => x.GroupId == groupId);
            RepositoryBase<ApplicationRoleGroup>.SaveChanges();
            return true;
        }

        public static bool DeleteRoleGroupByRole(string roleId)
        {
            RepositoryBase<ApplicationRoleGroup>.DeleteMulti(x => x.RoleId == roleId);
            RepositoryBase<ApplicationRoleGroup>.SaveChanges();
            return true;
        }

        public static bool AddUserGroups(IEnumerable<ApplicationUserGroup> userGroups)
        {
            foreach (var userGroup in userGroups)
            {
                RepositoryBase<ApplicationUserGroup>.Add(userGroup);
            }
            RepositoryBase<ApplicationUserGroup>.SaveChanges();
            return true;
        }

    }
}