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
    public static class StdTypeProvider
    {
        public static IEnumerable<StdTypeVM> GetAll()
        {
            IEnumerable<StdType> model = RepositoryBase<StdType>.GetAll();
            //return Mapping.Mapper.Map<IEnumerable<StandardTypeVM>>(model);
            return Mapper.Map<IEnumerable<StdTypeVM>>(model);
        }

        public static IEnumerable<StdTypeVM> GetAll(bool Actived, bool Blocked)
        {
            IEnumerable<StdType> model = RepositoryBase<StdType>.GetMulti(x => x.Actived == Actived && x.Blocked == Blocked);
            //return Mapping.Mapper.Map<IEnumerable<CompanyVM>>(model);
            return Mapper.Map<IEnumerable<StdTypeVM>>(model);
        }

        public static IEnumerable<StdTypeVM> GetData(string TypeName)
        {
            List<StdType> model = RepositoryBase<StdType>.GetMulti(x => x.Name.Contains(TypeName)).ToList<StdType>();
            //return Mapping.Mapper.Map<IEnumerable<StandardTypeVM>>(model);
            return Mapper.Map<IEnumerable<StdTypeVM>>(model);
        }

        public static bool IsUsed(string Id)
        {
            var query1 = from item in RepositoryBase<StdType>.DbContext.Standards
                         where item.StdTypeId == Id
                         select item.Id;
            if (query1.Count() > 0) return true;
            
            return false;
        }

        public static void Update(StdTypeVM item)
        {
            StdType dbItem = GetSingleById(item.Id);
            dbItem.UpdateStandardType(item);
            RepositoryBase<StdType>.Update(dbItem);
            RepositoryBase<StdType>.SaveChanges();
        }

        public static void Delete(StdTypeVM item)
        {
            Delete(item.Id);
        }

        public static void Delete(string Id)
        {
            RepositoryBase<StdType>.DeleteMulti(x => x.Id == Id);
            RepositoryBase<StdType>.SaveChanges();
        }

        public static void SetActived(string Id, bool value)
        {
            StdType model = RepositoryBase<StdType>.GetSingleById(Id);
            model.Actived = value;

            model.UpdatedDate = DateTime.Now;
            model.UpdatedBy = HttpContext.Current.User.Identity.Name;
            RepositoryBase<StdType>.SaveChanges();
        }

        public static void SetBlocked(string Id, bool value, string StaffID)
        {
            StdType model = GetSingleById(Id);
            model.Blocked = value;

            model.UpdatedDate = DateTime.Now;
            model.UpdatedBy = StaffID;
            RepositoryBase<StdType>.SaveChanges();
        }

        public static void SetActivedBlocked(string Id, bool ActivedValue, bool BlockedValue, string StaffID)
        {
            StdType model = GetSingleById(Id);
            model.Actived = ActivedValue;
            model.Blocked = BlockedValue;

            model.UpdatedDate = DateTime.Now;
            model.UpdatedBy = StaffID;
            RepositoryBase<StdType>.SaveChanges();
        }

        public static void DeleteMulti(List<string> IDs)
        {
            foreach (var item in IDs)
            {
                Delete(item);
            }
        }
        
        public static StdType Add(StdTypeVM item)
        {
            StdType dbItem = new StdType();
            dbItem.UpdateStandardType(item);

            dbItem.Id= item.Id;
            dbItem.Actived = item.Actived;

            dbItem.CreatedDate = DateTime.Now;
            dbItem.CreatedBy = HttpContext.Current.User.Identity.Name;

            StdType model = RepositoryBase<StdType>.Add(dbItem);
            RepositoryBase<StdType>.SaveChanges();
            return dbItem;
        }
        public static StdType GetSingleById(string key)
        {
            return RepositoryBase<StdType>.GetSingleById(key);
        }

        public static int Count(Expression<Func<StdType, bool>> where)
        {
            return RepositoryBase<StdType>.Count(where);
        }
    }
}
