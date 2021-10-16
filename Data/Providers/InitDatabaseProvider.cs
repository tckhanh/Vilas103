
using Data.Common;
using Data.DataModels;
using Data.DataVMs;
using Data.Infrastructure;
using Data.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;

namespace Data
{
    public static class InitDatabaseProvider
    {
        // In this method we will create default User roles and Admin user for login
        public static void GrantDefaultRolesForGroups(CFContext context)
        {
            try
            {
                ApplicationRoleManager _roleManager = new ApplicationRoleManager(new ApplicationRoleStore(context));
                List<string> roleList;
                ApplicationGroup groupItem;

                // Add All Roles to SuperAdmin Groups

                groupItem = context.ApplicationGroups.FirstOrDefault(x => x.Name == CommonConstants.SUPERADMIN_GROUP);
                IEnumerable<string> queryRoles = from g in context.Roles select g.Name;
                roleList = queryRoles.ToList();

                foreach (string roleItem in roleList)
                {
                    ApplicationRole dbRole = _roleManager.FindByName(roleItem);

                    if (dbRole != null && context.ApplicationRoleGroups.FirstOrDefault(x => x.GroupId == groupItem.Id && x.RoleId == dbRole.Id) == null)
                    {
                        ApplicationRoleGroup appRoleGroup = new ApplicationRoleGroup()
                        {
                            GroupId = groupItem.Id,
                            RoleId = dbRole.Id
                        };
                        if (context.ApplicationRoleGroups.FirstOrDefault(x => x.GroupId == appRoleGroup.GroupId && x.RoleId == appRoleGroup.RoleId) == null)
                        {
                            context.ApplicationRoleGroups.Add(appRoleGroup);
                            context.SaveChanges();
                        }
                    }
                }

                groupItem = context.ApplicationGroups.FirstOrDefault(x => x.Name == CommonConstants.DIRECTOR_GROUP);
                roleList = new List<string>()
                    {
                        CommonConstants.System_CanView_Role,
                        CommonConstants.Data_CanView_Role,
                        CommonConstants.Data_CanViewDetail_Role,
                        CommonConstants.Data_CanViewChart_Role,
                        CommonConstants.Data_CanViewStatitics_Role,
                        CommonConstants.Data_CanReset_Role,
                        CommonConstants.Data_CanSign_Role,
                        CommonConstants.Data_CanCancel_Role,
                        CommonConstants.Data_CanExport_Role
                     };
                foreach (string roleItem in roleList)
                {
                    ApplicationRole dbRole = _roleManager.FindByName(roleItem);

                    if (dbRole != null && context.ApplicationRoleGroups.FirstOrDefault(x => x.GroupId == groupItem.Id && x.RoleId == dbRole.Id) == null)
                    {
                        ApplicationRoleGroup appRoleGroup = new ApplicationRoleGroup()
                        {
                            GroupId = groupItem.Id,
                            RoleId = dbRole.Id
                        };
                        if (context.ApplicationRoleGroups.FirstOrDefault(x => x.GroupId == appRoleGroup.GroupId && x.RoleId == appRoleGroup.RoleId) == null)
                        {
                            context.ApplicationRoleGroups.Add(appRoleGroup);
                            context.SaveChanges();
                        }
                    }
                }

                groupItem = context.ApplicationGroups.FirstOrDefault(x => x.Name == CommonConstants.VILAS103_HEADER_GROUP);
                roleList = new List<string>()
                    {
                        CommonConstants.System_CanView_Role,
                        CommonConstants.Data_CanView_Role,
                        CommonConstants.Data_CanViewDetail_Role,
                        CommonConstants.Data_CanViewChart_Role,
                        CommonConstants.Data_CanViewReport_Role,
                        CommonConstants.Data_CanViewStatitics_Role,
                        CommonConstants.Data_CanAdd_Role,
                        CommonConstants.Data_CanImport_Role,
                        CommonConstants.Data_CanExport_Role,
                        CommonConstants.Data_CanEdit_Role,
                        CommonConstants.Data_CanReset_Role,
                        CommonConstants.Data_CanLock_Role,
                        CommonConstants.Data_CanCancel_Role,
                        CommonConstants.Info_CanViewMap_Role,
                        CommonConstants.Info_CanViewDetail_Role,
                        CommonConstants.Info_CanViewChart_Role,
                        CommonConstants.Info_CanViewReport_Role,
                        CommonConstants.Info_CanViewStatitics_Role,
                    };
                foreach (string roleItem in roleList)
                {
                    ApplicationRole dbRole = _roleManager.FindByName(roleItem);
                    if (dbRole != null)
                    {
                        if (context.ApplicationRoleGroups.FirstOrDefault(x => x.GroupId == groupItem.Id && x.RoleId == dbRole.Id) == null)
                        {
                            ApplicationRoleGroup appRoleGroup = new ApplicationRoleGroup()
                            {
                                GroupId = groupItem.Id,
                                RoleId = dbRole.Id
                            };
                            if (context.ApplicationRoleGroups.FirstOrDefault(x => x.GroupId == appRoleGroup.GroupId && x.RoleId == appRoleGroup.RoleId) == null)
                            {
                                context.ApplicationRoleGroups.Add(appRoleGroup);
                                context.SaveChanges();
                            }
                        }
                    }
                }

                groupItem = context.ApplicationGroups.FirstOrDefault(x => x.Name == CommonConstants.VILAS103_GROUP);
                roleList = new List<string>() {
                    CommonConstants.Data_CanView_Role,
                    CommonConstants.Data_CanViewDetail_Role,
                    CommonConstants.Data_CanViewChart_Role,
                    CommonConstants.Data_CanViewReport_Role,
                    CommonConstants.Data_CanAdd_Role,
                    CommonConstants.Data_CanImport_Role,
                    CommonConstants.Data_CanEdit_Role,
                    CommonConstants.Data_CanReset_Role,
                    CommonConstants.Data_CanLock_Role,
                    CommonConstants.Data_CanCancel_Role,
                };
                foreach (string roleItem in roleList)
                {
                    ApplicationRole dbRole = _roleManager.FindByName(roleItem);
                    if (dbRole != null)
                    {
                        if (context.ApplicationRoleGroups.FirstOrDefault(x => x.GroupId == groupItem.Id && x.RoleId == dbRole.Id) == null)
                        {
                            ApplicationRoleGroup appRoleGroup = new ApplicationRoleGroup()
                            {
                                GroupId = groupItem.Id,
                                RoleId = dbRole.Id
                            };
                            if (context.ApplicationRoleGroups.FirstOrDefault(x => x.GroupId == appRoleGroup.GroupId && x.RoleId == appRoleGroup.RoleId) == null)
                            {
                                context.ApplicationRoleGroups.Add(appRoleGroup);
                                context.SaveChanges();
                            }
                        }
                    }
                }

                groupItem = context.ApplicationGroups.FirstOrDefault(x => x.Name == CommonConstants.LAB_GROUP);
                roleList = new List<string>() {
                    CommonConstants.Data_CanView_Role,
                    CommonConstants.Data_CanViewDetail_Role,
                    CommonConstants.Data_CanViewChart_Role,
                    CommonConstants.Data_CanReset_Role,
                };
                foreach (string roleItem in roleList)
                {
                    ApplicationRole dbRole = _roleManager.FindByName(roleItem);
                    if (dbRole != null)
                    {
                        if (context.ApplicationRoleGroups.FirstOrDefault(x => x.GroupId == groupItem.Id && x.RoleId == dbRole.Id) == null)
                        {
                            ApplicationRoleGroup appRoleGroup = new ApplicationRoleGroup()
                            {
                                GroupId = groupItem.Id,
                                RoleId = dbRole.Id
                            };
                            if (context.ApplicationRoleGroups.FirstOrDefault(x => x.GroupId == appRoleGroup.GroupId && x.RoleId == appRoleGroup.RoleId) == null)
                            {
                                context.ApplicationRoleGroups.Add(appRoleGroup);
                                context.SaveChanges();
                            }
                        }
                    }
                }

                groupItem = context.ApplicationGroups.FirstOrDefault(x => x.Name == CommonConstants.STTT_GROUP);
                roleList = new List<string>() {
                    CommonConstants.Data_CanView_Role,
                    CommonConstants.Data_CanViewDetail_Role,
                    CommonConstants.Data_CanViewChart_Role,
                    CommonConstants.Data_CanReset_Role,
                };
                foreach (string roleItem in roleList)
                {
                    ApplicationRole dbRole = _roleManager.FindByName(roleItem);
                    if (dbRole != null)
                    {
                        if (context.ApplicationRoleGroups.FirstOrDefault(x => x.GroupId == groupItem.Id && x.RoleId == dbRole.Id) == null)
                        {
                            ApplicationRoleGroup appRoleGroup = new ApplicationRoleGroup()
                            {
                                GroupId = groupItem.Id,
                                RoleId = dbRole.Id
                            };
                            if (context.ApplicationRoleGroups.FirstOrDefault(x => x.GroupId == appRoleGroup.GroupId && x.RoleId == appRoleGroup.RoleId) == null)
                            {
                                context.ApplicationRoleGroups.Add(appRoleGroup);
                                context.SaveChanges();
                            }
                        }
                    }
                }

                groupItem = context.ApplicationGroups.FirstOrDefault(x => x.Name == CommonConstants.PUBLIC_GROUP);
                roleList = new List<string>() {
                    CommonConstants.Data_CanView_Role,
                    CommonConstants.Data_CanViewDetail_Role,
                    CommonConstants.Data_CanViewChart_Role,
                    CommonConstants.Data_CanReset_Role,
                };
                foreach (string roleItem in roleList)
                {
                    ApplicationRole dbRole = _roleManager.FindByName(roleItem);
                    if (dbRole != null)
                    {
                        if (context.ApplicationRoleGroups.FirstOrDefault(x => x.GroupId == groupItem.Id && x.RoleId == dbRole.Id) == null)
                        {
                            ApplicationRoleGroup appRoleGroup = new ApplicationRoleGroup()
                            {
                                GroupId = groupItem.Id,
                                RoleId = dbRole.Id
                            };
                            if (context.ApplicationRoleGroups.FirstOrDefault(x => x.GroupId == appRoleGroup.GroupId && x.RoleId == appRoleGroup.RoleId) == null)
                            {
                                context.ApplicationRoleGroups.Add(appRoleGroup);
                                context.SaveChanges();
                            }
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                string description = "";
                foreach (DbEntityValidationResult eve in ex.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                    description += ($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.\n");
                    foreach (DbValidationError ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        description += ($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"\n");
                    }
                }

                LogError(ex, description);
            }
            catch (DbUpdateException ex)
            {
                LogError(ex);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
            finally
            {
            }
        }

        public static void GrantDefaultRolesForRecoveryGroup(CFContext context)
        {
            try
            {
                ApplicationRoleManager _roleManager = new ApplicationRoleManager(new ApplicationRoleStore(context));
                List<string> roleList;
                ApplicationGroup groupItem;

                // Add All Roles to Recovery Group

                groupItem = context.ApplicationGroups.FirstOrDefault(x => x.Name == CommonConstants.RECOVERY_GROUP);
                IEnumerable<string> queryRoles = from g in context.Roles select g.Name;
                roleList = queryRoles.ToList();

                foreach (string roleItem in roleList)
                {
                    ApplicationRole dbRole = _roleManager.FindByName(roleItem);

                    if (dbRole != null && context.ApplicationRoleGroups.FirstOrDefault(x => x.GroupId == groupItem.Id && x.RoleId == dbRole.Id) == null)
                    {
                        ApplicationRoleGroup appRoleGroup = new ApplicationRoleGroup()
                        {
                            GroupId = groupItem.Id,
                            RoleId = dbRole.Id
                        };
                        if (context.ApplicationRoleGroups.FirstOrDefault(x => x.GroupId == appRoleGroup.GroupId && x.RoleId == appRoleGroup.RoleId) == null)
                        {
                            context.ApplicationRoleGroups.Add(appRoleGroup);
                            context.SaveChanges();
                        }
                    }
                }

            }
            catch (DbEntityValidationException ex)
            {
                string description = "";
                foreach (DbEntityValidationResult eve in ex.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                    description += ($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.\n");
                    foreach (DbValidationError ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        description += ($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"\n");
                    }
                }

                LogError(ex, description);
            }
            catch (DbUpdateException ex)
            {
                LogError(ex);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
            finally
            {
            }
        }

        public static void SetupRolesGroups(CFContext context)
        {
            try
            {
                // Su dung RoleManager khong dung RoleService
                //var _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new CFContext()));
                //var _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new CFContext()));

                ApplicationUserManager _userManager = new ApplicationUserManager(new UserStore<ApplicationUser, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(context));
                ApplicationRoleManager _roleManager = new ApplicationRoleManager(new ApplicationRoleStore(context));

                ApplicationGroup newGroup;
                ApplicationRole newRole;
                List<string[]> roleList;
                List<string[]> groupList;

                roleList = new List<string[]> {
                    new string[]{
                        CommonConstants.System_CanView_Role,
                        CommonConstants.System_CanView_Description },
                    new string[]{
                        CommonConstants.System_CanViewDetail_Role,
                        CommonConstants.System_CanViewDetail_Description },
                    new string[]{
                        CommonConstants.System_CanViewChart_Role,
                        CommonConstants.System_CanViewChart_Description },
                    new string[]{
                        CommonConstants.System_CanViewStatitics_Role,
                        CommonConstants.System_CanViewStatitics_Description },
                    new string[]{
                        CommonConstants.System_CanAdd_Role,
                        CommonConstants.System_CanAdd_Description },
                    new string[]{
                        CommonConstants.System_CanImport_Role,
                        CommonConstants.System_CanImport_Description },
                    new string[]{
                        CommonConstants.System_CanExport_Role,
                        CommonConstants.System_CanExport_Description },
                    new string[]{
                        CommonConstants.System_CanEdit_Role,
                        CommonConstants.System_CanEdit_Description },
                    new string[]{
                        CommonConstants.System_CanReset_Role,
                        CommonConstants.System_CanReset_Description },
                    new string[]{
                        CommonConstants.System_CanLock_Role,
                        CommonConstants.System_CanLock_Description },
                    new string[]{
                        CommonConstants.System_CanDelete_Role,
                        CommonConstants.System_CanDelete_Description }
                };

                foreach (string[] roleItem in roleList)
                {
                    if (!_roleManager.RoleExists(roleItem.ElementAt(0)))
                    {
                        // first we create Admin rool
                        newRole = new ApplicationRole(roleItem.ElementAt(0), roleItem.ElementAt(1));
                        _roleManager.Create(newRole);
                        context.SaveChanges();
                    }
                }

                roleList = new List<string[]> {
                    new string[]{
                        CommonConstants.Data_CanView_Role,
                        CommonConstants.Data_CanView_Description },
                    new string[]{
                        CommonConstants.Data_CanViewDetail_Role,
                        CommonConstants.Data_CanViewDetail_Description },
                    new string[]{
                        CommonConstants.Data_CanViewChart_Role,
                        CommonConstants.Data_CanViewChart_Description },
                    new string[]{
                        CommonConstants.Data_CanViewReport_Role,
                        CommonConstants.Data_CanViewReport_Description },
                    new string[]{
                        CommonConstants.Data_CanViewStatitics_Role,
                        CommonConstants.Data_CanViewStatitics_Description },
                    new string[]{
                        CommonConstants.Data_CanAdd_Role,
                        CommonConstants.Data_CanAdd_Description },
                    new string[]{
                        CommonConstants.Data_CanImport_Role,
                        CommonConstants.Data_CanImport_Description },
                    new string[]{
                        CommonConstants.Data_CanExport_Role,
                        CommonConstants.Data_CanExport_Description },
                    new string[]{
                        CommonConstants.Data_CanEdit_Role,
                        CommonConstants.Data_CanEdit_Description },
                    new string[]{
                        CommonConstants.Data_CanReset_Role,
                        CommonConstants.Data_CanReset_Description },
                    new string[]{
                        CommonConstants.Data_CanLock_Role,
                        CommonConstants.Data_CanLock_Description },
                    new string[]{
                        CommonConstants.Data_CanCancel_Role,
                        CommonConstants.Data_CanCancel_Description },
                    new string[]{
                        CommonConstants.Data_CanSign_Role,
                        CommonConstants.Data_CanSign_Description},

                    new string[]{
                        CommonConstants.Data_CanDelete_Role,
                        CommonConstants.Data_CanDelete_Description },

                    new string[]{
                        CommonConstants.Info_CanViewMap_Role,
                        CommonConstants.Info_CanView_Description },
                    new string[]{
                        CommonConstants.Info_CanViewDetail_Role,
                        CommonConstants.Info_CanViewDetail_Description },
                    new string[]{
                        CommonConstants.Info_CanViewChart_Role,
                        CommonConstants.Info_CanViewChart_Description },
                    new string[]{
                        CommonConstants.Info_CanViewReport_Role,
                        CommonConstants.Info_CanViewReport_Description },
                    new string[]{
                        CommonConstants.Info_CanViewStatitics_Role,
                        CommonConstants.Info_CanViewStatitics_Description },
                };

                foreach (string[] roleItem in roleList)
                {
                    if (!_roleManager.RoleExists(roleItem.ElementAt(0)))
                    {
                        // first we create Admin rool
                        newRole = new ApplicationRole(roleItem.ElementAt(0), roleItem.ElementAt(1));
                        _roleManager.Create(newRole);
                        context.SaveChanges();
                    }
                }

                groupList = new List<string[]> {
                    new string[]{
                        CommonConstants.SUPERADMIN_GROUP,
                        CommonConstants.SUPERADMIN_GROUP_NAME},
                    new string[]{
                        CommonConstants.DIRECTOR_GROUP,
                        CommonConstants.DIRECTOR_GROUP_NAME},
                    new string[]{
                        CommonConstants.VILAS103_HEADER_GROUP,
                        CommonConstants.VILAS103_HEADER_GROUP_NAME},
                    new string[]{
                        CommonConstants.VILAS103_GROUP,
                        CommonConstants.VILAS103_GROUP_NAME},
                    new string[]{
                        CommonConstants.LAB_GROUP,
                        CommonConstants.LAB_GROUP_NAME},
                    new string[]{
                        CommonConstants.STTT_GROUP,
                        CommonConstants.STTT_GROUP_NAME},
                    new string[]{
                        CommonConstants.PUBLIC_GROUP,
                        CommonConstants.PUBLIC_GROUP_NAME},
                };

                foreach (string[] groupItem in groupList)
                {
                    string groupName = groupItem.ElementAt(0);
                    if (context.ApplicationGroups.Where(x => x.Name == groupName).FirstOrDefault() == null)
                    {
                        newGroup = new ApplicationGroup(groupItem.ElementAt(0), groupItem.ElementAt(1));
                        context.ApplicationGroups.Add(newGroup);
                        context.SaveChanges();
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                string description = "";
                foreach (DbEntityValidationResult eve in ex.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                    description += ($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.\n");
                    foreach (DbValidationError ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        description += ($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"\n");
                    }
                }

                LogError(ex, description);
            }
            catch (DbUpdateException ex)
            {
                LogError(ex);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
            finally
            {
            }
        }

        public static void SetupRecoveryRolesGroups(CFContext context)
        {
            try
            {
                // Su dung RoleManager khong dung RoleService
                //var _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new CFContext()));
                //var _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new CFContext()));

                ApplicationUserManager _userManager = new ApplicationUserManager(new UserStore<ApplicationUser, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(context));
                ApplicationRoleManager _roleManager = new ApplicationRoleManager(new ApplicationRoleStore(context));

                ApplicationGroup newGroup;
                ApplicationRole newRole;
                List<string[]> roleList;
                List<string[]> groupList;

                roleList = new List<string[]> {
                    new string[]{
                        CommonConstants.System_CanView_Role,
                        CommonConstants.System_CanView_Description },
                    new string[]{
                        CommonConstants.System_CanViewDetail_Role,
                        CommonConstants.System_CanViewDetail_Description },
                    new string[]{
                        CommonConstants.System_CanViewChart_Role,
                        CommonConstants.System_CanViewChart_Description },
                    new string[]{
                        CommonConstants.System_CanViewStatitics_Role,
                        CommonConstants.System_CanViewStatitics_Description },
                    new string[]{
                        CommonConstants.System_CanAdd_Role,
                        CommonConstants.System_CanAdd_Description },
                    new string[]{
                        CommonConstants.System_CanImport_Role,
                        CommonConstants.System_CanImport_Description },
                    new string[]{
                        CommonConstants.System_CanExport_Role,
                        CommonConstants.System_CanExport_Description },
                    new string[]{
                        CommonConstants.System_CanEdit_Role,
                        CommonConstants.System_CanEdit_Description },
                    new string[]{
                        CommonConstants.System_CanReset_Role,
                        CommonConstants.System_CanReset_Description },
                    new string[]{
                        CommonConstants.System_CanLock_Role,
                        CommonConstants.System_CanLock_Description },
                    new string[]{
                        CommonConstants.System_CanDelete_Role,
                        CommonConstants.System_CanDelete_Description }
                };

                foreach (string[] roleItem in roleList)
                {
                    if (!_roleManager.RoleExists(roleItem.ElementAt(0)))
                    {
                        // first we create Admin rool
                        newRole = new ApplicationRole(roleItem.ElementAt(0), roleItem.ElementAt(1));
                        _roleManager.Create(newRole);
                        context.SaveChanges();
                    }
                }

                roleList = new List<string[]> {
                    new string[]{
                        CommonConstants.Data_CanView_Role,
                        CommonConstants.Data_CanView_Description },
                    new string[]{
                        CommonConstants.Data_CanViewDetail_Role,
                        CommonConstants.Data_CanViewDetail_Description },
                    new string[]{
                        CommonConstants.Data_CanViewChart_Role,
                        CommonConstants.Data_CanViewChart_Description },
                    new string[]{
                        CommonConstants.Data_CanViewReport_Role,
                        CommonConstants.Data_CanViewReport_Description },
                    new string[]{
                        CommonConstants.Data_CanViewStatitics_Role,
                        CommonConstants.Data_CanViewStatitics_Description },
                    new string[]{
                        CommonConstants.Data_CanAdd_Role,
                        CommonConstants.Data_CanAdd_Description },
                    new string[]{
                        CommonConstants.Data_CanImport_Role,
                        CommonConstants.Data_CanImport_Description },
                    new string[]{
                        CommonConstants.Data_CanExport_Role,
                        CommonConstants.Data_CanExport_Description },
                    new string[]{
                        CommonConstants.Data_CanEdit_Role,
                        CommonConstants.Data_CanEdit_Description },
                    new string[]{
                        CommonConstants.Data_CanReset_Role,
                        CommonConstants.Data_CanReset_Description },
                    new string[]{
                        CommonConstants.Data_CanLock_Role,
                        CommonConstants.Data_CanLock_Description },
                    new string[]{
                        CommonConstants.Data_CanCancel_Role,
                        CommonConstants.Data_CanCancel_Description },
                    new string[]{
                        CommonConstants.Data_CanSign_Role,
                        CommonConstants.Data_CanSign_Description},
                    new string[]{
                        CommonConstants.Data_CanDelete_Role,
                        CommonConstants.Data_CanDelete_Description },

                    new string[]{
                        CommonConstants.Info_CanViewMap_Role,
                        CommonConstants.Info_CanView_Description },
                    new string[]{
                        CommonConstants.Info_CanViewDetail_Role,
                        CommonConstants.Info_CanViewDetail_Description },
                    new string[]{
                        CommonConstants.Info_CanViewChart_Role,
                        CommonConstants.Info_CanViewChart_Description },
                    new string[]{
                        CommonConstants.Info_CanViewReport_Role,
                        CommonConstants.Info_CanViewReport_Description },
                    new string[]{
                        CommonConstants.Info_CanViewStatitics_Role,
                        CommonConstants.Info_CanViewStatitics_Description },
                };

                foreach (string[] roleItem in roleList)
                {
                    if (!_roleManager.RoleExists(roleItem.ElementAt(0)))
                    {
                        // first we create Admin rool
                        newRole = new ApplicationRole(roleItem.ElementAt(0), roleItem.ElementAt(1));
                        _roleManager.Create(newRole);
                        context.SaveChanges();
                    }
                }

                groupList = new List<string[]> {
                    new string[]{
                        CommonConstants.RECOVERY_GROUP,
                        CommonConstants.RECOVERY_GROUP_NAME},
                    new string[]{
                        CommonConstants.SUPERADMIN_GROUP,
                        CommonConstants.SUPERADMIN_GROUP_NAME},
                    new string[]{
                        CommonConstants.DIRECTOR_GROUP,
                        CommonConstants.DIRECTOR_GROUP_NAME},
                    new string[]{
                        CommonConstants.VILAS103_HEADER_GROUP,
                        CommonConstants.VILAS103_HEADER_GROUP_NAME},
                    new string[]{
                        CommonConstants.VILAS103_GROUP,
                        CommonConstants.VILAS103_GROUP_NAME},
                    new string[]{
                        CommonConstants.LAB_GROUP,
                        CommonConstants.LAB_GROUP_NAME},
                    new string[]{
                        CommonConstants.STTT_GROUP,
                        CommonConstants.STTT_GROUP_NAME},
                    new string[]{
                        CommonConstants.PUBLIC_GROUP,
                        CommonConstants.PUBLIC_GROUP_NAME},
                };

                foreach (string[] groupItem in groupList)
                {
                    if (!_roleManager.RoleExists(groupItem.ElementAt(0)))
                    {
                        newGroup = new ApplicationGroup(groupItem.ElementAt(0), groupItem.ElementAt(1));
                        context.ApplicationGroups.Add(newGroup);
                        context.SaveChanges();
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                string description = "";
                foreach (DbEntityValidationResult eve in ex.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                    description += ($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.\n");
                    foreach (DbValidationError ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        description += ($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"\n");
                    }
                }

                LogError(ex, description);
            }
            catch (DbUpdateException ex)
            {
                LogError(ex);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
            finally
            {
            }
        }

        public static void CreateSuperUser(CFContext context)
        {
            try
            {
                // Su dung RoleManager khong dung RoleService
                //var _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new CFContext()));
                //var _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new CFContext()));

                ApplicationUserManager _userManager = new ApplicationUserManager(new UserStore<ApplicationUser, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(context));
                ApplicationRoleManager _roleManager = new ApplicationRoleManager(new ApplicationRoleStore(context));

                ApplicationGroup groupItem;

                // Add Default SuperAdmin User

                ApplicationUser newAppUser = new ApplicationUser()
                {
                    UserName = CommonConstants.SuperAdmin_Name,
                    FullName = CommonConstants.SuperAdmin_FullName,
                    Email = CommonConstants.SuperAdmin_Email,
                    EmailConfirmed = CommonConstants.SuperAdmin_EmailConfirmed,
                    ImagePath = CommonConstants.SuperAdmin_ImagePath,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "System",
                    Locked = false
                };

                ApplicationUser userItem = _userManager.FindByName(CommonConstants.SuperAdmin_Name);

                if (userItem == null)
                {
                    IdentityResult chkUser = _userManager.Create(newAppUser, CommonConstants.SuperAdmin_Password);
                    chkUser = _userManager.SetLockoutEnabled(newAppUser.Id, false);
                }

                //Add default SuperAdmin User to Group SuperAdmin

                userItem = _userManager.FindByName(CommonConstants.SuperAdmin_Name);
                groupItem = context.ApplicationGroups.FirstOrDefault(x => x.Name == CommonConstants.SUPERADMIN_GROUP);
                if (userItem != null && groupItem != null)
                {
                    ApplicationUserGroup appUserGroup = new ApplicationUserGroup()
                    {
                        GroupId = groupItem.Id,
                        UserId = userItem.Id
                    };
                    if (context.ApplicationUserGroups.FirstOrDefault(x => x.GroupId == appUserGroup.GroupId && x.UserId == appUserGroup.UserId) == null)
                    {
                        context.ApplicationUserGroups.Add(appUserGroup);
                        context.SaveChanges();
                    }

                    //Xóa Roles cũ Tạo Roles mới cho User
                    List<string> userRoles = _userManager.GetRoles(userItem.Id).ToList();
                    foreach (string role in userRoles)
                    {
                        _userManager.RemoveFromRole(userItem.Id, role);
                        context.SaveChanges();
                    }

                    userRoles = _userManager.GetRoles(userItem.Id).ToList();

                    Trace.WriteLine("Số Roles còn lại:" + userRoles.Count());

                    //add role to user
                    IQueryable<string> query = from g in context.Roles
                                               join rg in context.ApplicationRoleGroups
                                               on g.Id equals rg.RoleId
                                               join ug in context.ApplicationUserGroups
                                               on rg.GroupId equals ug.GroupId
                                               where ug.UserId == userItem.Id
                                               select g.Name;
                    string[] roles = query.Distinct().ToArray();
                    _userManager.AddToRoles(userItem.Id, roles);
                    context.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                string description = "";
                foreach (DbEntityValidationResult eve in ex.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                    description += ($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.\n");
                    foreach (DbValidationError ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        description += ($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"\n");
                    }
                }

                LogError(ex, description);
            }
            catch (DbUpdateException ex)
            {
                LogError(ex);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
            finally
            {
            }
        }

        public static bool CreateUser(CFContext context, ApplicationUser newAppUser, string password = "")
        {
            //try
            //{
            // Su dung RoleManager khong dung RoleService
            //var _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new CFContext()));
            //var _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new CFContext()));

            ApplicationUserManager _userManager = new ApplicationUserManager(new UserStore<ApplicationUser, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(context));
            ApplicationRoleManager _roleManager = new ApplicationRoleManager(new ApplicationRoleStore(context));

            ApplicationUser userItem = ApplicationUserProvider.Add(newAppUser, password);

            // Default cho newUser thuoc Group PUBLIC_GROUP
            ApplicationGroup groupItem = context.ApplicationGroups.FirstOrDefault(x => x.Name == CommonConstants.PUBLIC_GROUP);
            if (userItem != null && groupItem != null)
            {
                ApplicationGroupProvider.AddUserToGroupSuit(groupItem, userItem.Id);
                //ApplicationUserGroup appUserGroup = new ApplicationUserGroup()
                //{
                //    GroupId = groupItem.Id,
                //    UserId = userItem.Id
                //};
                //if (context.ApplicationUserGroups.FirstOrDefault(x => x.GroupId == appUserGroup.GroupId && x.UserId == appUserGroup.UserId) == null)
                //{
                //    context.ApplicationUserGroups.Add(appUserGroup);
                //    context.SaveChanges();
                //}

                ////Xóa Roles cũ Tạo Roles mới cho User
                //List<string> userRoles = _userManager.GetRoles(userItem.Id).ToList();
                //foreach (string role in userRoles)
                //{
                //    _userManager.RemoveFromRole(userItem.Id, role);
                //    context.SaveChanges();
                //}

                //userRoles = _userManager.GetRoles(userItem.Id).ToList();

                //Trace.WriteLine("Số Roles còn lại:" + userRoles.Count());

                ////add role to user
                //IQueryable<string> query = from g in context.Roles
                //                           join rg in context.ApplicationRoleGroups
                //                           on g.Id equals rg.RoleId
                //                           join ug in context.ApplicationUserGroups
                //                           on rg.GroupId equals ug.GroupId
                //                           where ug.UserId == userItem.Id
                //                           select g.Name;
                //string[] roles = query.Distinct().ToArray();
                //_userManager.AddToRoles(userItem.Id, roles);
                //context.SaveChanges();
            }
            return true;
        }

        public static void CreateRecoveryUser(CFContext context)
        {
            try
            {
                // Su dung RoleManager khong dung RoleService
                //var _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new CFContext()));
                //var _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new CFContext()));

                ApplicationUserManager _userManager = new ApplicationUserManager(new UserStore<ApplicationUser, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(context));
                ApplicationRoleManager _roleManager = new ApplicationRoleManager(new ApplicationRoleStore(context));

                ApplicationGroup groupItem;

                // Add Default SuperAdmin User

                ApplicationUser newAppUser = new ApplicationUser()
                {
                    UserName = CommonConstants.Recovery_Name,
                    FullName = CommonConstants.Recovery_FullName,
                    Email = CommonConstants.Recovery_Email,
                    EmailConfirmed = CommonConstants.Recovery_EmailConfirmed,
                    ImagePath = CommonConstants.Recovery_ImagePath,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "Recovery",
                    Locked = false
                };

                ApplicationUser userItem = _userManager.FindByName(CommonConstants.Recovery_Name);

                if (userItem == null)
                {
                    IdentityResult chkUser = _userManager.Create(newAppUser, CommonConstants.Recovery_Password);
                    chkUser = _userManager.SetLockoutEnabled(newAppUser.Id, false);
                }

                //Add default Recovery User to Group Recovery

                userItem = _userManager.FindByName(CommonConstants.Recovery_Name);
                groupItem = context.ApplicationGroups.FirstOrDefault(x => x.Name == CommonConstants.RECOVERY_GROUP);
                if (userItem != null && groupItem != null)
                {
                    ApplicationUserGroup appUserGroup = new ApplicationUserGroup()
                    {
                        GroupId = groupItem.Id,
                        UserId = userItem.Id
                    };
                    if (context.ApplicationUserGroups.FirstOrDefault(x => x.GroupId == appUserGroup.GroupId && x.UserId == appUserGroup.UserId) == null)
                    {
                        context.ApplicationUserGroups.Add(appUserGroup);
                        context.SaveChanges();
                    }

                    //Xóa Roles cũ Tạo Roles mới cho User
                    List<string> userRoles = _userManager.GetRoles(userItem.Id).ToList();
                    foreach (string role in userRoles)
                    {
                        _userManager.RemoveFromRole(userItem.Id, role);
                        context.SaveChanges();
                    }

                    userRoles = _userManager.GetRoles(userItem.Id).ToList();

                    Trace.WriteLine("Số Roles còn lại:" + userRoles.Count());

                    //add role to user
                    IQueryable<string> query = from g in context.Roles
                                               join rg in context.ApplicationRoleGroups
                                               on g.Id equals rg.RoleId
                                               join ug in context.ApplicationUserGroups
                                               on rg.GroupId equals ug.GroupId
                                               where ug.UserId == userItem.Id
                                               select g.Name;
                    string[] roles = query.Distinct().ToArray();
                    _userManager.AddToRoles(userItem.Id, roles);
                    context.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                string description = "";
                foreach (DbEntityValidationResult eve in ex.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                    description += ($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.\n");
                    foreach (DbValidationError ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        description += ($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"\n");
                    }
                }

                LogError(ex, description);
            }
            catch (DbUpdateException ex)
            {
                LogError(ex);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
            finally
            {
            }
        }

        public static void LogError(Exception e, string description = "")
        {
            try
            {
                Error error = new Error
                {
                    Controller = e.Source,
                    CreatedDate = DateTime.Now,
                    Message = e.Message,
                    Description = description,
                    StackTrace = e.StackTrace
                };
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult eve in ex.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                    foreach (DbValidationError ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                Trace.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }



        public static void CreateFooter(CFContext context)
        {
            if (context.Footers.Count(x => x.Id == CommonConstants.DefaultFooterId) == 0)
            {
            }
        }

        public static void CreateSlide(CFContext context)
        {
            if (context.Slides.Count() == 0)
            {
                List<Slide> listSlide = new List<Slide>()
                {
                    new Slide() {
                        Name ="Slide 1",
                        DisplayOrder =1,
                        Status =true,
                        Url ="#",
                        Image ="/Assets/client/images/bag.jpg",
                        Content =@"	<h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                                <p>Lorem ipsum dolor sit amet, consectetur
                            adipisicing elit, sed do eiusmod tempor incididunt ut labore et </ p >
                        <span class=""on-get"">GET NOW</span>" },
                    new Slide() {
                        Name ="Slide 2",
                        DisplayOrder =2,
                        Status =true,
                        Url ="#",
                        Image ="/Assets/client/images/bag1.jpg",
                    Content=@"<h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>

                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </ p >

                                <span class=""on-get"">GET NOW</span>"},
                };
                context.Slides.AddRange(listSlide);
                context.SaveChanges();
            }
        }

        public static void CreatePage(CFContext context)
        {
            if (context.Pages.Count() == 0)
            {
                try
                {
                    WebPage page = new WebPage()
                    {
                        Name = "Giới thiệu",
                        Alias = "gioi-thieu",
                        Content = @"Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium ",
                        Status = true
                    };
                    context.Pages.Add(page);
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (DbEntityValidationResult eve in ex.EntityValidationErrors)
                    {
                        Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                        foreach (DbValidationError ve in eve.ValidationErrors)
                        {
                            Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        }
                    }
                }
            }
        }

        public static void CreateContactDetail(CFContext context)
        {
            if (context.ContactDetails.Count() == 0)
            {
                try
                {
                    ContactDetail contactDetail = new ContactDetail()
                    {
                        Name = "Shop thời trang TEDU",
                        Address = "Ngõ 77 Xuân La",
                        Email = "tedu@gmail.com",
                        Lat = 21.0633645,
                        Lng = 105.8053274,
                        Phone = "095423233",
                        Website = "http://tedu.com.vn",
                        Other = "",
                        Status = true
                    };
                    context.ContactDetails.Add(contactDetail);
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (DbEntityValidationResult eve in ex.EntityValidationErrors)
                    {
                        Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                        foreach (DbValidationError ve in eve.ValidationErrors)
                        {
                            Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        }
                    }
                }
            }
        }


        public static void CreateConfigTitle()
        {
            CFContext context = RepositoryBase<SysConfig>.DbContext;

            if (!context.SysConfigs.Any(x => x.Id == "HomeTitle"))
            {
                context.SysConfigs.Add(new SysConfig()
                {
                    Id = "HomeTitle",
                    ValueString = "Trang chủ Kiểm định BTS",
                });
            }
            if (!context.SysConfigs.Any(x => x.Id == "HomeMetaKeyword"))
            {
                context.SysConfigs.Add(new SysConfig()
                {
                    Id = "HomeMetaKeyword",
                    ValueString = "Trang chủ Kiểm định BTS",
                });
            }
            if (!context.SysConfigs.Any(x => x.Id == "HomeMetaDescription"))
            {
                context.SysConfigs.Add(new SysConfig()
                {
                    Id = "HomeMetaDescription",
                    ValueString = "Trang chủ Kiểm định BTS",
                });
            }
        }


        public static void CreateStdType()
        {
            CFContext context = RepositoryBase<StdType>.DbContext;
            if (!context.StdTypes.Any(x => x.Id == "QCVN"))
            {
                context.StdTypes.Add(new StdType()
                {
                    Id = "QCVN",
                    Name = "Quy chuẩn kỹ thuật quốc gia",
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                });
            }
            if (!context.StdTypes.Any(x => x.Id == "TCVN"))
            {
                context.StdTypes.Add(new StdType()
                {
                    Id = "TCVN",
                    Name = "Tiêu chuẩn kỹ thuật quốc gia",
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                });
            }
            context.SaveChanges();
        }

        public static void CreatEquGroup()
        {
            CFContext context = RepositoryBase<EquGroup>.DbContext;

            if (!context.EquGroups.Any(x => x.Id == "Mobile Phone"))
            {
                context.EquGroups.Add(new EquGroup()
                {
                    Id = "Mobile Phone",
                    Name = "Máy điện thoại di động",
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                });
            }

            if (!context.EquGroups.Any(x => x.Id == "Tablet"))
            {
                context.EquGroups.Add(new EquGroup()
                {
                    Id = "Tablet",
                    Name = "Máy tính bảng",
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                });
            }

            if (!context.EquGroups.Any(x => x.Id == "Laptop"))
            {
                context.EquGroups.Add(new EquGroup()
                {
                    Id = "Laptop",
                    Name = "Máy tính xách tay",
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                });
            }

            if (!context.EquGroups.Any(x => x.Id == "Printer"))
            {
                context.EquGroups.Add(new EquGroup()
                {
                    Id = "Printer",
                    Name = "Máy in",
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                });
            }

            if (!context.EquGroups.Any(x => x.Id == "Access Point"))
            {
                context.EquGroups.Add(new EquGroup()
                {
                    Id = "Access Point",
                    Name = "Điểm truy cập Wifi",
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                });
            }

            if (!context.EquGroups.Any(x => x.Id == "Extender"))
            {
                context.EquGroups.Add(new EquGroup()
                {
                    Id = "Extender",
                    Name = "Điểm mở rộng truy cập Wifi",
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                });
            }

            if (!context.EquGroups.Any(x => x.Id == "Module"))
            {
                context.EquGroups.Add(new EquGroup()
                {
                    Id = "Module",
                    Name = "Module thu phát sóng",
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                });
            }
            context.SaveChanges();
        }

        public static void CreateStandard()
        {
            CFContext context = RepositoryBase<Standard>.DbContext;

            if (!context.Standards.Any(x => x.Id == "QCVN 54:2020/BTTTT"))
            {
                context.Standards.Add(new Standard()
                {
                    Id = "QCVN 54:2020/BTTTT",
                    Name = "QCVN về thiết bị truyền dữ liệu băng rộng hoạt động trong băng tần 2,4 GHz",
                    StdTypeId = "QCVN",
                    ValidDate = new DateTime(2021, 7, 1),
                    FeeDoc = "Quyết định số 52/QĐ-TTĐLCL ngày 25/05/2021",
                    FeePrice = 6545000,
                    LabID = "VILAS103",
                    Abstract = "QCVN 54:2020/BTTTT được xây dựng trên cơ sở tiêu chuẩn ETSI EN 300 328 V2.2.2 (2019-07) của Viện Tiêu chuẩn Viễn thông Châu Âu(ETSI).",
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                });
            }
            if (!context.Standards.Any(x => x.Id == "QCVN 117:2020/BTTTT (2G-3G-4G)"))
            {
                context.Standards.Add(new Standard()
                {
                    Id = "QCVN 117:2020/BTTTT (2G-3G-4G)",
                    Name = "QCVN về về thiết bị đầu cuối thông tin di động mặt đất - Phần truy nhập vô tuyến (2G-3G-4G)",
                    StdTypeId = "QCVN",
                    IssueDate = new DateTime(2020, 12, 31),
                    ValidDate = new DateTime(2021, 7, 1),
                    FeeDoc = "Quyết định số 54/QĐ-TTĐLCL ngày 25/05/2021",
                    FeePrice = 18010000,
                    LabID = "VILAS103",
                    Abstract = "QCVN 117:2020/BTTTT được xây dựng trên cơ sở tiêu chuẩn ETSI EN 301 908-13 V11.1.1(2016-07), EN 301 908-1 V6.2.1(2013-04), EN 301 908-2 V6.2.1 (2013-10), EN 301 511 V9.0.2(2003-03), TS 151 010-1 V12.2.0(2014-11)  của Viện Tiêu chuẩn Viễn thông Châu Âu(ETSI).",
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                });
            }

            if (!context.Standards.Any(x => x.Id == "QCVN 117:2020/BTTTT (3G-4G)"))
            {
                context.Standards.Add(new Standard()
                {
                    Id = "QCVN 117:2020/BTTTT (3G-4G)",
                    Name = "QCVN về về thiết bị đầu cuối thông tin di động mặt đất - Phần truy nhập vô tuyến (3G-4G)",
                    StdTypeId = "QCVN",
                    IssueDate = new DateTime(2020, 12, 31),
                    ValidDate = new DateTime(2021, 7, 1),
                    FeeDoc = "Quyết định số 54/QĐ-TTĐLCL ngày 25/05/2021",
                    FeePrice = 12340000,
                    LabID = "VILAS103",
                    Abstract = "QCVN 117:2020/BTTTT được xây dựng trên cơ sở tiêu chuẩn ETSI EN 301 908-13 V11.1.1(2016-07), EN 301 908-1 V6.2.1(2013-04), EN 301 908-2 V6.2.1 (2013-10), EN 301 511 V9.0.2(2003-03), TS 151 010-1 V12.2.0(2014-11)  của Viện Tiêu chuẩn Viễn thông Châu Âu(ETSI).",
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                });
            }

            if (!context.Standards.Any(x => x.Id == "QCVN 117:2020/BTTTT (4G)"))
            {
                context.Standards.Add(new Standard()
                {
                    Id = "QCVN 117:2020/BTTTT (4G)",
                    Name = "QCVN về về thiết bị đầu cuối thông tin di động mặt đất - Phần truy nhập vô tuyến (4G)",
                    StdTypeId = "QCVN",
                    IssueDate = new DateTime(2020, 12, 31),
                    ValidDate = new DateTime(2021, 7, 1),
                    FeeDoc = "Quyết định số 54/QĐ-TTĐLCL ngày 25/05/2021",
                    FeePrice = 6670000,
                    LabID = "VILAS103",
                    Abstract = "QCVN 117:2020/BTTTT (4G) được xây dựng trên cơ sở tiêu chuẩn ETSI EN 301 908-13 V11.1.1(2016-07), EN 301 908-1 V6.2.1(2013-04), EN 301 908-2 V6.2.1 (2013-10), EN 301 511 V9.0.2(2003-03), TS 151 010-1 V12.2.0(2014-11)  của Viện Tiêu chuẩn Viễn thông Châu Âu(ETSI).",
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                });
            }

            if (!context.Standards.Any(x => x.Id == "QCVN 65:2013/BTTTT"))
            {
                context.Standards.Add(new Standard()
                {
                    Id = "QCVN 65:2013/BTTTT",
                    Name = "QCVN về thiết bị truy nhập vô tuyến băng tần 5 GHz",
                    StdTypeId = "QCVN",
                    FeeDoc = "Báo giá đo kiểm sản phẩm ngày 08/10/2020",
                    FeePrice = 2435000,
                    LabID = "VILAS103",
                    Abstract = "QCVN 65:2013/BTTTT được xây dựng trên cơ sở tiêu chuẩn ETSI EN 301 893 V1.3.1(2005-08) của Viện Tiêu chuẩn Viễn thông Châu Âu(ETSI).",
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                });
            }
            context.SaveChanges();
        }

        public static void CreateStatus()
        {
            CFContext context = RepositoryBase<Status>.DbContext;

            if (!context.Statuses.Any(x => x.Id == CommonConstants.StatusId_01_Registered))
            {
                context.Statuses.Add(new Status()
                {
                    Id = CommonConstants.StatusId_01_Registered,
                    Name = "Đã đăng ký",
                    Description = "Đã đăng ký->chờ tiếp nhận",
                    LinkedIds = CommonConstants.StatusId_02_Accepted + "," + CommonConstants.StatusId_15_Canceled,
                    Group = CommonConstants.FilterRequestGroup,
                    Actived = true,
                    Blocked = false,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                });
            }

            if (!context.Statuses.Any(x => x.Id == CommonConstants.StatusId_02_Accepted))
            {
                context.Statuses.Add(new Status()
                {
                    Id = CommonConstants.StatusId_02_Accepted,
                    Name = "Đã tiếp nhận",
                    Description = "Đã tiếp nhận->chờ phân công",
                    LinkedIds = CommonConstants.StatusId_03_Assigned + "," + CommonConstants.StatusId_15_Canceled,
                    Group = CommonConstants.FilterRequestGroup,
                    Actived = true,
                    Blocked = false,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                });
            }

            if (!context.Statuses.Any(x => x.Id == CommonConstants.StatusId_03_Assigned))
            {
                context.Statuses.Add(new Status()
                {
                    Id = CommonConstants.StatusId_03_Assigned,
                    Name = "Đã phân công",
                    Description = "Đã phân công->chờ đo kiểm",
                    LinkedIds = CommonConstants.StatusId_04_Testing,
                    Group = CommonConstants.FilterRequestGroup,
                    Actived = true,
                    Blocked = false,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                });
            }

            if (!context.Statuses.Any(x => x.Id == CommonConstants.StatusId_04_Testing))
            {
                context.Statuses.Add(new Status()
                {
                    Id = CommonConstants.StatusId_04_Testing,
                    Name = "Đang đo kiểm",
                    Description = "Đang đo kiểm",
                    LinkedIds = CommonConstants.StatusId_08_Tested + "," + CommonConstants.StatusId_05_WaitingSupport,
                    Group = CommonConstants.FilterRequestGroup,
                    Actived = true,
                    Blocked = false,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                });
            }

            if (!context.Statuses.Any(x => x.Id == CommonConstants.StatusId_05_WaitingSupport))
            {
                context.Statuses.Add(new Status()
                {
                    Id = CommonConstants.StatusId_05_WaitingSupport,
                    Name = "Đang chờ KH hỗ trợ",
                    Description = "Đang chờ KH hỗ trợ",
                    LinkedIds = CommonConstants.StatusId_04_Testing + "," + CommonConstants.StatusId_08_Tested,
                    Group = CommonConstants.FilterRequestGroup,
                    Actived = true,
                    Blocked = false,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                });
            }

            if (!context.Statuses.Any(x => x.Id == CommonConstants.StatusId_06_Rechecking))
            {
                context.Statuses.Add(new Status()
                {
                    Id = CommonConstants.StatusId_06_Rechecking,
                    Name = "Đang kiểm tra lại",
                    Description = "Chưa nhất trí->Đang kiểm tra lại",
                    LinkedIds = CommonConstants.StatusId_07_Rechecked,
                    Group = CommonConstants.FilterRequestGroup,
                    Actived = true,
                    Blocked = false,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                });
            }

            if (!context.Statuses.Any(x => x.Id == CommonConstants.StatusId_07_Rechecked))
            {
                context.Statuses.Add(new Status()
                {
                    Id = CommonConstants.StatusId_07_Rechecked,
                    Name = "Đã kiểm tra lại",
                    Description = "Đã kiểm tra lại->chờ thẩm định",
                    LinkedIds = CommonConstants.StatusId_09_Verified,
                    Group = CommonConstants.FilterRequestGroup,
                    Actived = true,
                    Blocked = false,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                });
            }

            if (!context.Statuses.Any(x => x.Id == CommonConstants.StatusId_08_Tested))
            {
                context.Statuses.Add(new Status()
                {
                    Id = CommonConstants.StatusId_08_Tested,
                    Name = "Đã đo kiểm",
                    Description = "Đã đo kiểm->chờ thẩm định",
                    LinkedIds = CommonConstants.StatusId_09_Verified + "," + CommonConstants.StatusId_06_Rechecking,
                    Group = CommonConstants.FilterRequestGroup,
                    Actived = true,
                    Blocked = false,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                });
            }

            if (!context.Statuses.Any(x=> x.Id == CommonConstants.StatusId_09_Verified))
            {
                context.Statuses.Add(new Status()
                {
                    Id = CommonConstants.StatusId_09_Verified,
                    Name = "Đã thẩm định",
                    Description = "Đã thẩm định->chờ phê duyệt",
                    LinkedIds = CommonConstants.StatusId_10_Approved,
                    Group = CommonConstants.FilterRequestGroup,
                    Actived = true,
                    Blocked = false,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                });
            }


            if (!context.Statuses.Any(x => x.Id == CommonConstants.StatusId_10_Approved))
            {
                context.Statuses.Add(new Status()
                {
                    Id = CommonConstants.StatusId_10_Approved,
                    Name = "Đã phê duyệt",
                    Description = "Đã phê duyệt->chờ ban hành",
                    LinkedIds = CommonConstants.StatusId_12_Issued + "," + CommonConstants.StatusId_11_NotIssued + "," + CommonConstants.StatusId_06_Rechecking,
                    Group = CommonConstants.FilterRequestGroup,
                    Actived = true,
                    Blocked = false,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                });
            }

            if (!context.Statuses.Any(x => x.Id == CommonConstants.StatusId_11_NotIssued))
            {
                context.Statuses.Add(new Status()
                {
                    Id = CommonConstants.StatusId_11_NotIssued,
                    Name = "Hoàn trả mẫu",
                    Description = "Đã phê duyệt->Hoàn trả mẫu",
                    LinkedIds = CommonConstants.StatusId_14_Returned,
                    Group = CommonConstants.FilterRequestGroup,
                    Actived = true,
                    Blocked = false,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                });
            }

            if (!context.Statuses.Any(x => x.Id == CommonConstants.StatusId_12_Issued))
            {
                context.Statuses.Add(new Status()
                {
                    Id = CommonConstants.StatusId_12_Issued,
                    Name = "Đã ban hành",
                    Description = "Đã ban hành->chờ nộp phí",
                    LinkedIds = CommonConstants.StatusId_13_Paid,
                    Group = CommonConstants.FilterRequestGroup,
                    Actived = true,
                    Blocked = false,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                });
            }

            if (!context.Statuses.Any(x => x.Id == CommonConstants.StatusId_13_Paid))
            {
                context.Statuses.Add(new Status()
                {
                    Id = CommonConstants.StatusId_13_Paid,
                    Name = "Đã nộp phí",
                    Description = "Đã nộp phí->chờ nhận KQ",
                    LinkedIds = CommonConstants.StatusId_14_Returned,
                    Group = CommonConstants.FilterRequestGroup,
                    Actived = true,
                    Blocked = false,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                });
            }

            if (!context.Statuses.Any(x => x.Id == CommonConstants.StatusId_14_Returned))
            {
                context.Statuses.Add(new Status()
                {
                    Id = CommonConstants.StatusId_14_Returned,
                    Name = "Đã nhận KQ",
                    Description = "Đã nhận KQ->Kết thúc",
                    LinkedIds = "",
                    Group = CommonConstants.FilterRequestGroup,
                    Actived = true,
                    Blocked = false,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                });
            }

            if (!context.Statuses.Any(x => x.Id == CommonConstants.StatusId_15_Canceled))
            {
                context.Statuses.Add(new Status()
                {
                    Id = CommonConstants.StatusId_15_Canceled,
                    Name = "Đã bị huỷ",
                    Description = "Đã bị huỷ->Kết thúc",
                    LinkedIds = "",
                    Group = CommonConstants.FilterRequestGroup,
                    Actived = true,
                    Blocked = false,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                });
            }
            context.SaveChanges();
        }

        public static void CreatCompany()
        {
            CFContext context = RepositoryBase<Company>.DbContext;

            if (!context.Companies.Any(x => x.Id == "TP-LINK_VN"))
            {
                CompanyProvider.AddSuitTransaction(new CompanyVM()
                {
                    Id = "TP-LINK_VN",
                    Name = "Công ty TNHH TP-Link Technologies Việt Nam",
                    Address = "Phòng 12A-15, tòa nhà Vincom, số 45A, đường Lý Tự Trọng, phường Bến Nghé, quận 1, thành phố Hồ Chí Minh.",
                    PhoneNo = "(028) 62615063",
                    FaxNo = "",
                    ContactName = "C. Trang",
                    ContactPhone = "0908231992",
                    TaxCompanyName = "Công ty TNHH TP-Link Technologies Việt Nam",
                    TaxAddress = "Phòng 12A-15, tòa nhà Vincom, số 45A, đường Lý Tự Trọng, phường Bến Nghé, quận 1, thành phố Hồ Chí Minh.",
                    TaxCode = "0310685120",
                    TaxEmail = "tracy.huynh@tp-link.com",
                    Contract = "",
                    UserName = "tplinkvn",
                    Password = "Abc123",
                    ConfirmPassword = "Abc123",
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                });

                CompanyProvider.AddSuitTransaction(new CompanyVM()
                {
                    Id = "DONGQUAN",
                    Name = "Công ty TNHH Công nghệ - Tư Vấn - Thương Mại Đông Quân",
                    Address = "Số 421/11, đường Sư Vạn Hạnh, phường 12, quận 10, thành phố Hồ Chí Minh.",
                    PhoneNo = "028 38680145",
                    FaxNo = "",
                    ContactName = "Ngô Thị Thủy Anh",
                    ContactPhone = "0976222302",
                    TaxCompanyName = "Công ty TNHH Công nghệ - Tư Vấn - Thương Mại Đông Quân",
                    TaxAddress = "Số 421/11, đường Sư Vạn Hạnh, phường 12, quận 10, thành phố Hồ Chí Minh.",
                    TaxCode = "0302338451",
                    TaxEmail = "anhntt@dongquan.vn",
                    Contract = "",
                    UserName = "dongquan",
                    Password = "Abc123",
                    ConfirmPassword = "Abc123",
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                });
            }
        }
    }
}
