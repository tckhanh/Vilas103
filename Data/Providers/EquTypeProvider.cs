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
    public static class EquTypeProvider
    {
        public static IEnumerable<EquTypeVM> GetAll()
        {
            IEnumerable<EquType> model = RepositoryBase<EquType>.GetAll();
            //return Mapping.Mapper.Map<IEnumerable<EquTypeVM>>(model);
            return Mapper.Map<IEnumerable<EquTypeVM>>(model);
        }

        public static IEnumerable<EquTypeVM> GetAll(bool Actived, bool Blocked)
        {
            IEnumerable<EquType> model = RepositoryBase<EquType>.GetMulti(x => x.Actived == Actived && x.Blocked == Blocked);
            //return Mapping.Mapper.Map<IEnumerable<CompanyVM>>(model);
            return Mapper.Map<IEnumerable<EquTypeVM>>(model);
        }

        public static IEnumerable<EquTypeVM> GetData(string Name)
        {
            List<EquType> model = RepositoryBase<EquType>.GetMulti(x => x.Name.Contains(Name)).ToList<EquType>();
            //return Mapping.Mapper.Map<IEnumerable<EquTypeVM>>(model);
            return Mapper.Map<IEnumerable<EquTypeVM>>(model);
        }

        public static bool IsUsed(string Id)
        {
            var query1 = from item in RepositoryBase<EquType>.DbContext.Standards
                         where item.StdTypeId == Id
                         select item.Id;
            if (query1.Count() > 0) return true;
            
            return false;
        }

        public static void Update(EquTypeVM item)
        {
            EquType dbItem = GetSingleById(item.Id);
            dbItem.UpdateEquType(item);
            RepositoryBase<EquType>.Update(dbItem);
            RepositoryBase<EquType>.SaveChanges();
        }

        public static void Delete(EquTypeVM item)
        {
            Delete(item.Id);
        }

        public static void Delete(string Id)
        {
            RepositoryBase<EquType>.DeleteMulti(x => x.Id == Id);
            RepositoryBase<EquType>.SaveChanges();
        }

        public static void SetActived(string Id, bool value)
        {
            EquType model = RepositoryBase<EquType>.GetSingleById(Id);
            model.Actived = value;

            model.UpdatedDate = DateTime.Now;
            model.UpdatedBy = HttpContext.Current.User.Identity.Name;
            RepositoryBase<EquType>.SaveChanges();
        }

        public static void SetBlocked(string Id, bool value, string StaffID)
        {
            EquType model = GetSingleById(Id);
            model.Blocked = value;

            model.UpdatedDate = DateTime.Now;
            model.UpdatedBy = StaffID;
            RepositoryBase<EquType>.SaveChanges();
        }

        public static void SetActivedBlocked(string Id, bool ActivedValue, bool BlockedValue, string StaffID)
        {
            EquType model = GetSingleById(Id);
            model.Actived = ActivedValue;
            model.Blocked = BlockedValue;

            model.UpdatedDate = DateTime.Now;
            model.UpdatedBy = StaffID;
            RepositoryBase<EquType>.SaveChanges();
        }

        public static void DeleteMulti(List<string> IDs)
        {
            foreach (var item in IDs)
            {
                Delete(item);
            }
        }
        
        public static EquType Add(EquTypeVM item)
        {
            EquType dbItem = new EquType();
            dbItem.UpdateEquType(item);

            dbItem.Id= item.Id;
            dbItem.Actived = item.Actived;

            dbItem.CreatedDate = DateTime.Now;
            dbItem.CreatedBy = HttpContext.Current.User.Identity.Name;

            EquType model = RepositoryBase<EquType>.Add(dbItem);
            RepositoryBase<EquType>.SaveChanges();
            return dbItem;
        }
        public static EquType GetSingleById(string key)
        {
            return RepositoryBase<EquType>.GetSingleById(key);
        }

        public static int Count(Expression<Func<EquType, bool>> where)
        {
            return RepositoryBase<EquType>.Count(where);
        }
    }
}
