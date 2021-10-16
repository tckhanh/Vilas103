using AutoMapper;
using BTS.Web.Infrastructure.Extensions;
using Data.Common;
using Data.DataModels;
using Data.DataVMs;
using Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Data.Providers
{
    public static class CompanyProvider
    {
        public static IEnumerable<CompanyVM> GetAll()
        {
            IEnumerable<Company> model = RepositoryBase<Company>.GetAll();
            //return Mapping.Mapper.Map<IEnumerable<CompanyVM>>(model);
            return Mapper.Map<IEnumerable<CompanyVM>>(model);
        }

        public static IEnumerable<CompanyVM> GetAll(bool Actived, bool Blocked)
        {
            IEnumerable<Company> model = RepositoryBase<Company>.GetMulti(x=> x.Actived == Actived && x.Blocked == Blocked);
            //return Mapping.Mapper.Map<IEnumerable<CompanyVM>>(model);
            return Mapper.Map<IEnumerable<CompanyVM>>(model);
        }


        public static IEnumerable<CompanyVM> GetData(string UserName)
        {
            List<Company> model = RepositoryBase<Company>.GetMulti(x => x.UserName == UserName).ToList<Company>();
            //return Mapping.Mapper.Map<IEnumerable<CompanyVM>>(model);
            return Mapper.Map<IEnumerable<CompanyVM>>(model);
        }

        public static bool IsUsed(string Id)
        {
            //var query1 = from item in RepositoryBase<Company>.DbContext.Requests
            //             where item.CompanyID == Id
            //             select item.RequestID;
            //if (query1.Count() > 0) return true;

            return false;
        }

        public static void Update(CompanyVM item)
        {
            Company dbItem = RepositoryBase<Company>.GetSingleById(item.Id);
            dbItem.UpdateCompany(item);
            RepositoryBase<Company>.Update(dbItem);
            RepositoryBase<Company>.SaveChanges();
        }

        public static void Delete(CompanyVM item)
        {
            Delete(item.Id);
        }

        public static void DeleteSuitTransaction(CompanyVM item)
        {
            using (DbContextTransaction transaction = RepositoryBase<Company>.DbContext.Database.BeginTransaction())
            {
                try
                {
                    // kiem tra User cua Company có được cấp quyền chưa
                    Company companyItem = GetSingleById(item.Id);

                    ApplicationUser userItem = ApplicationUserProvider.GetSingleByName(companyItem.UserName);
                    if (userItem != null)
                        ApplicationUserProvider.DeleteSuit(userItem.Id, true);

                    // Delete Compay
                    Delete(item.Id);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Trace.WriteLine("Error occurred:" + ex.Message);
                    throw;
                }
            }
        }

        public static void Delete(string Id)
        {
            RepositoryBase<Company>.DeleteMulti(x => x.Id == Id);
            RepositoryBase<Company>.SaveChanges();
        }
        public static void SetActived(string Id, bool value, string StaffID)
        {
            Company model = RepositoryBase<Company>.GetSingleById(Id);
            model.Actived = value;

            model.UpdatedDate = DateTime.Now;
            model.UpdatedBy = StaffID;
            RepositoryBase<Company>.SaveChanges();
        }

        public static void SetBlocked(string Id, bool value, string StaffID)
        {
            Company model = RepositoryBase<Company>.GetSingleById(Id);
            model.Blocked = value;
            if (value) model.BlockedDate = DateTime.Now;
            model.UpdatedDate = DateTime.Now;
            model.UpdatedBy = StaffID;
            RepositoryBase<Company>.SaveChanges();
        }

        public static void SetActivedBlocked(string Id, bool ActivedValue, bool BlockedValue, string StaffID)
        {
            Company model = RepositoryBase<Company>.GetSingleById(Id);
            model.Actived = ActivedValue;
            model.Blocked = BlockedValue;

            model.UpdatedDate = DateTime.Now;
            model.UpdatedBy = StaffID;
            RepositoryBase<Company>.SaveChanges();
        }

        public static void DeleteMulti(List<string> IDs)
        {
            foreach (var item in IDs)
            {
                Delete(item);
            }
        }
        public static Company AddSuitTransaction(CompanyVM item)
        {
            Company result = new Company();
            using (DbContextTransaction transaction = RepositoryBase<Company>.DbContext.Database.BeginTransaction())
            {
                try
                {
                    ApplicationUser dbItemUser = new ApplicationUser()
                    {
                        UserName = item.UserName,
                        FullName = item.ContactName,
                        Email = item.TaxEmail,
                        ImagePath = CommonConstants.SuperAdmin_ImagePath,
                        Locked = false,
                    };

                    dbItemUser = ApplicationUserProvider.Add(dbItemUser, item.Password);

                    ApplicationGroup defaultGroup = ApplicationGroupProvider.GetByName(CommonConstants.PUBLIC_GROUP);
                    if (defaultGroup != null)
                    {
                        ApplicationGroupProvider.AddUserToGroupSuit(defaultGroup, dbItemUser.Id);
                    }
                    result = Add(item);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Trace.WriteLine("Error occurred:" + ex.Message);
                    throw;
                }
            }
            return result;
        }

        public static Company Add(CompanyVM item)
        {
            Company dbItem = new Company();
            dbItem.UpdateCompany(item);
            dbItem.Actived = false;
            dbItem.Blocked = false;
            dbItem.CreatedDate = DateTime.Now;
            dbItem.CreatedBy = HttpContext.Current.User.Identity.Name;
            Company model = RepositoryBase<Company>.Add(dbItem);
            RepositoryBase<Company>.SaveChanges();
            return dbItem;
        }
        public static Company GetSingleById(string key)
        {
            return RepositoryBase<Company>.GetSingleById(key);
        }

        public static int Count(Expression<Func<Company, bool>> where)
        {
            return RepositoryBase<Company>.Count(where);
        }
    }
}
