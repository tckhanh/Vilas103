using AutoMapper;
using BTS.Web.Infrastructure.Extensions;
using Data.DataModels;
using Data.DataVMs;
using Data.Infrastructure;
using Lib.Exceptions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Data.Providers
{
    public static class ApplicationUserProvider
    {
        private static ApplicationUserManager _userManager = new ApplicationUserManager(new UserStore<ApplicationUser, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(RepositoryBase<ApplicationUser>.DbContext));
        public static IEnumerable<ApplicationUserVM> GetAll()
        {
            List<ApplicationUser> model = RepositoryBase<ApplicationUser>.GetAll().ToList<ApplicationUser>();
            //return Mapping.Mapper.Map<IEnumerable<ApplicationUserVM>>(model);
            return Mapper.Map<IEnumerable<ApplicationUserVM>>(model);
        }

        public static IEnumerable<ApplicationUserVM> GetData(string UserName)
        {
            List<ApplicationUser> model = RepositoryBase<ApplicationUser>.GetMulti(x => x.UserName == UserName).ToList<ApplicationUser>();
            //return Mapping.Mapper.Map<IEnumerable<ApplicationUserVM>>(model);
            return Mapper.Map<IEnumerable<ApplicationUserVM>>(model);
        }
    
        public static bool IsUsed(int Id)
        {
            //var query1 = from item in DbContext.Requests
            //             where item.ApplicationUserID == Id
            //             select item.RequestID;
            //if (query1.Count() > 0) return true;

            return false;
        }

        public static ApplicationUser GetSingleById(string Id)
        {
            return _userManager.FindById(Id);
        }

        public static ApplicationUser GetSingleByName(string Name)
        {
            return _userManager.FindByName(Name);
        }

        public static bool isExisted(string Name)
        {
            return GetSingleByName(Name) != null;
        }

        public static void Update(ApplicationUserVM item)
        {
            ApplicationUser dbItem = RepositoryBase<ApplicationUser>.GetSingleById(item.Id);
            dbItem.UpdateApplicationUser(item);
            RepositoryBase<ApplicationUser>.Update(dbItem);
            RepositoryBase<ApplicationUser>.SaveChanges();
        }

        public static void Delete(ApplicationUserVM item)
        {
            RepositoryBase<ApplicationUser>.DeleteMulti(x => x.Id == item.Id);
            RepositoryBase<ApplicationUser>.SaveChanges();
        }

        private static void Delete(string Id)
        {
            RepositoryBase<ApplicationUser>.DeleteMulti(x => x.Id == Id);
            RepositoryBase<ApplicationUser>.SaveChanges();
        }

        public static bool DeleteSuit(string Id, bool ForceDelete)
        {
            // kiem tra User có tồn tại
            if (!ForceDelete && (ApplicationGroupProvider.GetGroupsByUserId(Id) != null || ApplicationRoleProvider.GetRealRolesByUserId(Id) != null))
            {
                throw new MyException("User đang được cấp quyền, bạn cần thu hồi các quyền cấp cho User trước khi xoá");
            }

            removeRolesFromUser(Id);
            ApplicationGroupProvider.RemoveUserFromGroups(Id);

            // Delete User
            Delete(Id);

            return true;
        }

        public static bool DeleteSuitTransaction(string Id, bool ForceDelete)
        {
            using (DbContextTransaction transaction = RepositoryBase<ApplicationUser>.DbContext.Database.BeginTransaction())
            {
                try
                {
                    // kiem tra User có tồn tại
                    if (!ForceDelete && (ApplicationGroupProvider.GetGroupsByUserId(Id) != null || ApplicationRoleProvider.GetRealRolesByUserId(Id) != null))
                    {
                        throw new MyException("User đang được cấp quyền, bạn cần thu hồi các quyền cấp cho User trước khi xoá");
                    }

                    removeRolesFromUser(Id);
                    ApplicationGroupProvider.RemoveUserFromGroups(Id);

                    // Delete User
                    Delete(Id);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Trace.WriteLine("Error occurred:" + ex.Message);
                    throw;
                }
            }
            return true;
        }

        public static void SetLocked(int Id, bool value)
        {
            ApplicationUser model = RepositoryBase<ApplicationUser>.GetSingleById(Id);
            model.Locked = value;
            model.UpdatedDate = DateTime.Now;
            model.UpdatedBy = HttpContext.Current.User.Identity.Name;
            RepositoryBase<ApplicationUser>.SaveChanges();
        }


        public static void DeleteMulti(List<string> IDs)
        {
            RepositoryBase<ApplicationUser>.DeleteMulti(x => IDs.Contains(x.Id));
            RepositoryBase<ApplicationUser>.SaveChanges();
        }
        public static ApplicationUser Add(ApplicationUser item, string password = "")
        {
            //ApplicationUser dbItem = new ApplicationUser();
            //dbItem.UpdateApplicationUser(item);

            //InitDatabaseProvider.CreateUser(new CFContext(), dbItem, item.Password);

            // Add User
            item.CreatedDate = DateTime.Now;
            item.CreatedBy = HttpContext.Current.User.Identity.Name;
            ApplicationUser userItem = _userManager.FindByName(item.UserName);

            if (userItem == null)
            {
                IdentityResult chkUser = _userManager.Create(item, password ?? "");
                if (!chkUser.Succeeded)
                {
                    if (chkUser.Errors.Count() == 1)
                    {
                        throw new MyException(chkUser.Errors.FirstOrDefault());
                    }
                    else
                    {
                        var exceptions = new List<ApplicationException>();
                        foreach (var error in chkUser.Errors)
                        {
                            exceptions.Add(new ApplicationException(error));
                        }
                        throw new AggregateException("Aggregate Exception Message", exceptions);
                    }
                }
                userItem = _userManager.FindByName(item.UserName);
                chkUser = _userManager.SetLockoutEnabled(userItem.Id, false);
                RepositoryBase<ApplicationUser>.SaveChanges();
            }
            else
            {
                throw new MyException(String.Format("UserName {0} đã tồn tại rồi", userItem.UserName));
            }
            return userItem;
        }

        public static ApplicationUserVM GetSingleById(int key)
        {
            ApplicationUser model = RepositoryBase<ApplicationUser>.GetSingleById(key);
            return Mapper.Map<ApplicationUserVM>(model);
        }

        public static int Count(Expression<Func<ApplicationUser, bool>> where)
        {
            return RepositoryBase<ApplicationUser>.Count(where);
        }

        public static ICollection<ApplicationUser> GetUsersByGroupId(string groupId)
        {

            var query = from g in RepositoryBase<ApplicationUser>.DbContext.ApplicationGroups
                        join ug in RepositoryBase<ApplicationUser>.DbContext.ApplicationUserGroups
                        on g.Id equals ug.GroupId
                        join u in RepositoryBase<ApplicationUser>.DbContext.Users
                        on ug.UserId equals u.Id
                        where ug.GroupId == groupId
                        select u;
            return query.ToList();
        }

        public static IEnumerable<ApplicationUserGroup> GetLogicUsersByRoleId(string roleId)
        {
            var query = from ug in RepositoryBase<ApplicationUser>.DbContext.ApplicationUserGroups
                        join rg in RepositoryBase<ApplicationUser>.DbContext.ApplicationRoleGroups
                        on ug.GroupId equals rg.GroupId
                        where rg.RoleId == roleId
                        select ug;
            return query;
        }

        public static ICollection<ApplicationUser> GetUsersByGroupIds(string[] groupIds)
        {
            var query = from g in RepositoryBase<ApplicationUser>.DbContext.ApplicationGroups
                        join ug in RepositoryBase<ApplicationUser>.DbContext.ApplicationUserGroups
                        on g.Id equals ug.GroupId
                        join u in RepositoryBase<ApplicationUser>.DbContext.Users
                        on ug.UserId equals u.Id
                        where groupIds.Contains(ug.GroupId)
                        select u;
            return query.ToList();
        }

        public static bool AssignRoleToUser(string userId, string roleName)
        {
            _userManager.AddToRole(userId, roleName);
            return true;
        }

        public static bool AssignRolesToUser(string userId, IEnumerable<ApplicationRole> roles)
        {
            foreach (var role in roles)
            {
                _userManager.AddToRole(userId, role.Name);
            }
            RepositoryBase<ApplicationUser>.SaveChanges();
            return true;
        }

        public static bool RemoveRoleFromUser(string userId, string roleName)
        {
            _userManager.RemoveFromRole(userId, roleName);
            return true;
        }

        public static bool removeRolesFromUser(string userId)
        {
            //Xóa Roles cũ Tạo Roles mới cho User
            var userRoles = _userManager.GetRoles(userId);
            foreach (var role in userRoles)
            {
                _userManager.RemoveFromRole(userId, role);
            }
            RepositoryBase<ApplicationUser>.SaveChanges();
            return true;
        }
    }
}
