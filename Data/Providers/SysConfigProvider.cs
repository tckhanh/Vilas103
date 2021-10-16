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
    public static class SysConfigProvider
    {
        private static Object RegisteredNoLOCK = new Object();
        private static Object ReceivedNoLOCK = new Object();

        public static IEnumerable<SysConfigVM> GetAll()
        {
            IEnumerable<SysConfig> model = RepositoryBase<SysConfig>.GetAll();
            //return Mapping.Mapper.Map<IEnumerable<StandardTypeVM>>(model);
            return Mapper.Map<IEnumerable<SysConfigVM>>(model);
        }

        public static IEnumerable<SysConfigVM> GetAll(bool Actived, bool Blocked)
        {
            IEnumerable<SysConfig> model = RepositoryBase<SysConfig>.GetMulti(x => x.Actived == Actived && x.Blocked == Blocked);
            //return Mapping.Mapper.Map<IEnumerable<CompanyVM>>(model);
            return Mapper.Map<IEnumerable<SysConfigVM>>(model);
        }

        public static IEnumerable<SysConfigVM> GetData(string Name)
        {
            List<SysConfig> model = RepositoryBase<SysConfig>.GetMulti(x => x.Description.Contains(Name)).ToList<SysConfig>();
            //return Mapping.Mapper.Map<IEnumerable<StandardTypeVM>>(model);
            return Mapper.Map<IEnumerable<SysConfigVM>>(model);
        }

        public static bool IsUsed(string Id)
        {
            var query1 = from item in RepositoryBase<SysConfig>.DbContext.SysConfigs
                         where item.Id == Id
                         select item.Id;
            if (query1.Count() > 0) return true;
            
            return false;
        }

        public static void Update(SysConfigVM item)
        {
            SysConfig dbItem = GetSingleById(item.Id);
            dbItem.UpdateSysConfig(item);
            RepositoryBase<SysConfig>.Update(dbItem);
            RepositoryBase<SysConfig>.SaveChanges();
        }

        public static void Delete(SysConfigVM item)
        {
            Delete(item.Id);
        }

        public static void Delete(string Id)
        {
            RepositoryBase<SysConfig>.DeleteMulti(x => x.Id == Id);
            RepositoryBase<SysConfig>.SaveChanges();
        }

        public static void SetActived(string Id, bool value)
        {
            SysConfig model = RepositoryBase<SysConfig>.GetSingleById(Id);
            model.Actived = value;

            model.UpdatedDate = DateTime.Now;
            model.UpdatedBy = HttpContext.Current.User.Identity.Name;
            RepositoryBase<SysConfig>.SaveChanges();
        }

        public static void SetBlocked(string Id, bool value, string StaffID)
        {
            SysConfig model = GetSingleById(Id);
            model.Blocked = value;

            model.UpdatedDate = DateTime.Now;
            model.UpdatedBy = StaffID;
            RepositoryBase<SysConfig>.SaveChanges();
        }

        public static void SetActivedBlocked(string Id, bool ActivedValue, bool BlockedValue, string StaffID)
        {
            SysConfig model = GetSingleById(Id);
            model.Actived = ActivedValue;
            model.Blocked = BlockedValue;

            model.UpdatedDate = DateTime.Now;
            model.UpdatedBy = StaffID;
            RepositoryBase<SysConfig>.SaveChanges();
        }

        public static void DeleteMulti(List<string> IDs)
        {
            foreach (var item in IDs)
            {
                Delete(item);
            }
        }
        
        public static SysConfig Add(SysConfigVM item)
        {
            SysConfig dbItem = new SysConfig();
            dbItem.UpdateSysConfig(item);

            dbItem.Id= item.Id;
            dbItem.Actived = item.Actived;

            dbItem.CreatedDate = DateTime.Now;
            dbItem.CreatedBy = HttpContext.Current.User.Identity.Name;

            SysConfig model = RepositoryBase<SysConfig>.Add(dbItem);
            RepositoryBase<SysConfig>.SaveChanges();
            return dbItem;
        }
        public static SysConfig GetSingleById(string key)
        {
            return RepositoryBase<SysConfig>.GetSingleById(key);
        }
        public static SysConfig GetRegisteredNo(int Year)
        {
            return GetItem(CommonConstants.RegistedNo, Year, 1, "1");
        }
        public static SysConfig GetReceivedNo(int Year)
        {
            return GetItem(CommonConstants.ReceivedNo, Year, 1, "1");
        }

        public static SysConfig GrantRegisteredNo(int Year)
        {
            lock (RegisteredNoLOCK)
            {
                var item = GetRegisteredNo(Year);

                item.ValueInt++;
                RepositoryBase<SysConfig>.Update(item);
                RepositoryBase<SysConfig>.SaveChanges();

                item.ValueInt--;
                return item;
            }
        }

        public static SysConfig GrantReceivedNo(int Year)
        {
            lock (ReceivedNoLOCK)
            {
                var item = GetReceivedNo(Year);

                item.ValueInt++;
                RepositoryBase<SysConfig>.Update(item);
                RepositoryBase<SysConfig>.SaveChanges();

                item.ValueInt--;
                return item;
            }
        }

       
        public static SysConfig GetItem(string Id, int Year, int ValueInt = 1, string ValueString = "")
        {
            var Item = RepositoryBase<SysConfig>.GetSingleByCondition(x => x.Id == Id && x.Year == Year);
            if (Item == null)
            {
                Item = new SysConfig()
                {
                    Id = CommonConstants.RegistedNo,
                    Year = Year,
                    Description = CommonConstants.ReceivedNo_Description,
                    ValueInt = ValueInt,
                    ValueString = ValueString,
                    Actived = true,
                    Blocked = false,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                };
                SysConfig model = RepositoryBase<SysConfig>.Add(Item);
                RepositoryBase<SysConfig>.SaveChanges();
            }
            return Item;
        }

        public static SysConfig GetSingleByCondition(Expression<Func<SysConfig, bool>> expression, string[] includes = null)
        {
            return RepositoryBase<SysConfig>.GetSingleByCondition(expression, includes);
        }

        public static int Count(Expression<Func<SysConfig, bool>> where)
        {
            return RepositoryBase<SysConfig>.Count(where);
        }
    }
}
