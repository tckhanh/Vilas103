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
    public static class StandardProvider
    {
        public static IEnumerable<StandardVM> GetAll()
        {
            IEnumerable<Standard> model = RepositoryBase<Standard>.GetAll();
            //return Mapping.Mapper.Map<IEnumerable<StandardVM>>(model);
            return Mapper.Map<IEnumerable<StandardVM>>(model);
        }

        public static IEnumerable<StandardVM> GetAll(bool Actived, bool Blocked)
        {
            IEnumerable<Standard> model = RepositoryBase<Standard>.GetMulti(x => x.Actived == Actived && x.Blocked == Blocked);
            //return Mapping.Mapper.Map<IEnumerable<CompanyVM>>(model);
            return Mapper.Map<IEnumerable<StandardVM>>(model);
        }

        public static IEnumerable<StandardVM> GetData(string Name)
        {
            List<Standard> model = RepositoryBase<Standard>.GetMulti(x => x.Name.Contains(Name)).ToList<Standard>();
            //return Mapping.Mapper.Map<IEnumerable<StandardVM>>(model);
            return Mapper.Map<IEnumerable<StandardVM>>(model);
        }

        public static long SumPrice(string StdCodes)
        {
            var query1 = from item in RepositoryBase<Standard>.DbContext.Standards
                         where StdCodes.Contains(item.Id)
                         select item.FeePrice;
            if (query1.Count() > 0)
                return query1.Sum();
            else
                return 0;
        }
        public static bool IsUsed(string Id)
        {
            var query1 = from item in RepositoryBase<Standard>.DbContext.Standards
                         where item.Id == Id
                         select item.Id;
            if (query1.Count() > 0) return true;
            
            return false;
        }

        public static void Update(StandardVM item)
        {
            Standard dbItem = GetSingleById(item.Id);
            dbItem.UpdateStandard(item);
            RepositoryBase<Standard>.Update(dbItem);
            RepositoryBase<Standard>.SaveChanges();
        }

        public static void Delete(StandardVM item)
        {
            Delete(item.Id);
        }

        public static void Delete(string Id)
        {
            RepositoryBase<Standard>.DeleteMulti(x => x.Id == Id);
            RepositoryBase<Standard>.SaveChanges();
        }

        public static void SetActived(string Id, bool value)
        {
            Standard model = RepositoryBase<Standard>.GetSingleById(Id);
            model.Actived = value;

            model.UpdatedDate = DateTime.Now;
            model.UpdatedBy = HttpContext.Current.User.Identity.Name;
            RepositoryBase<Standard>.SaveChanges();
        }

        public static void SetBlocked(string Id, bool value, string StaffID)
        {
            Standard model = GetSingleById(Id);
            model.Blocked = value;

            model.UpdatedDate = DateTime.Now;
            model.UpdatedBy = StaffID;
            RepositoryBase<Standard>.SaveChanges();
        }

        public static void SetActivedBlocked(string Id, bool ActivedValue, bool BlockedValue, string StaffID)
        {
            Standard model = GetSingleById(Id);
            model.Actived = ActivedValue;
            model.Blocked = BlockedValue;

            model.UpdatedDate = DateTime.Now;
            model.UpdatedBy = StaffID;
            RepositoryBase<Standard>.SaveChanges();
        }

        public static void DeleteMulti(List<string> IDs)
        {
            foreach (var item in IDs)
            {
                Delete(item);
            }
        }
        
        public static Standard Add(StandardVM item)
        {
            Standard dbItem = new Standard();
            dbItem.UpdateStandard(item);

            dbItem.Id= item.Id;
            dbItem.Actived = item.Actived;

            dbItem.CreatedDate = DateTime.Now;
            dbItem.CreatedBy = HttpContext.Current.User.Identity.Name;

            Standard model = RepositoryBase<Standard>.Add(dbItem);
            RepositoryBase<Standard>.SaveChanges();
            return dbItem;
        }
        public static Standard GetSingleById(string key)
        {
            return RepositoryBase<Standard>.GetSingleById(key);
        }

        public static int Count(Expression<Func<Standard, bool>> where)
        {
            return RepositoryBase<Standard>.Count(where);
        }
    }
}
