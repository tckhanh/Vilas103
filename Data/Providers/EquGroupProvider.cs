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
    public static class EquGroupProvider
    {
        public static IEnumerable<EquGroupVM> GetAll()
        {
            IEnumerable<EquGroup> model = RepositoryBase<EquGroup>.GetAll();
            //return Mapping.Mapper.Map<IEnumerable<EquGroupVM>>(model);
            return Mapper.Map<IEnumerable<EquGroupVM>>(model);
        }

        public static IEnumerable<EquGroupVM> GetAll(bool Actived, bool Blocked)
        {
            IEnumerable<EquGroup> model = RepositoryBase<EquGroup>.GetMulti(x => x.Actived == Actived && x.Blocked == Blocked);
            //return Mapping.Mapper.Map<IEnumerable<CompanyVM>>(model);
            return Mapper.Map<IEnumerable<EquGroupVM>>(model);
        }

        public static IEnumerable<EquGroupVM> GetData(string Name)
        {
            List<EquGroup> model = RepositoryBase<EquGroup>.GetMulti(x => x.Name.Contains(Name)).ToList<EquGroup>();
            //return Mapping.Mapper.Map<IEnumerable<EquGroupVM>>(model);
            return Mapper.Map<IEnumerable<EquGroupVM>>(model);
        }

        public static bool IsUsed(string Id)
        {
            var query1 = from item in RepositoryBase<EquGroup>.DbContext.Standards
                         where item.StdTypeId == Id
                         select item.Id;
            if (query1.Count() > 0) return true;
            
            return false;
        }

        public static void Update(EquGroupVM item)
        {
            EquGroup dbItem = GetSingleById(item.Id);
            dbItem.UpdateEquGroup(item);
            RepositoryBase<EquGroup>.Update(dbItem);
            RepositoryBase<EquGroup>.SaveChanges();
        }

        public static void Delete(EquGroupVM item)
        {
            Delete(item.Id);
        }

        public static void Delete(string Id)
        {
            RepositoryBase<EquGroup>.DeleteMulti(x => x.Id == Id);
            RepositoryBase<EquGroup>.SaveChanges();
        }

        public static void SetActived(string Id, bool value)
        {
            EquGroup model = RepositoryBase<EquGroup>.GetSingleById(Id);
            model.Actived = value;

            model.UpdatedDate = DateTime.Now;
            model.UpdatedBy = HttpContext.Current.User.Identity.Name;
            RepositoryBase<EquGroup>.SaveChanges();
        }

        public static void SetBlocked(string Id, bool value, string StaffID)
        {
            EquGroup model = GetSingleById(Id);
            model.Blocked = value;

            model.UpdatedDate = DateTime.Now;
            model.UpdatedBy = StaffID;
            RepositoryBase<EquGroup>.SaveChanges();
        }

        public static void SetActivedBlocked(string Id, bool ActivedValue, bool BlockedValue, string StaffID)
        {
            EquGroup model = GetSingleById(Id);
            model.Actived = ActivedValue;
            model.Blocked = BlockedValue;

            model.UpdatedDate = DateTime.Now;
            model.UpdatedBy = StaffID;
            RepositoryBase<EquGroup>.SaveChanges();
        }

        public static void DeleteMulti(List<string> IDs)
        {
            foreach (var item in IDs)
            {
                Delete(item);
            }
        }
        
        public static EquGroup Add(EquGroupVM item)
        {
            EquGroup dbItem = new EquGroup();
            dbItem.UpdateEquGroup(item);

            dbItem.Id= item.Id;
            dbItem.Actived = item.Actived;

            dbItem.CreatedDate = DateTime.Now;
            dbItem.CreatedBy = HttpContext.Current.User.Identity.Name;

            EquGroup model = RepositoryBase<EquGroup>.Add(dbItem);
            RepositoryBase<EquGroup>.SaveChanges();
            return dbItem;
        }
        public static EquGroup GetSingleById(string key)
        {
            return RepositoryBase<EquGroup>.GetSingleById(key);
        }

        public static int Count(Expression<Func<EquGroup, bool>> where)
        {
            return RepositoryBase<EquGroup>.Count(where);
        }
    }
}
