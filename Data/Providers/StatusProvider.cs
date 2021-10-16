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
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Web;

namespace Data.Providers
{
    public static class StatusProvider
    {
        public static IEnumerable<StatusVM> GetAll()
        {
            IEnumerable<Status> model = RepositoryBase<Status>.GetAll();
            //return Mapping.Mapper.Map<IEnumerable<ReqStatusVM>>(model);
            return Mapper.Map<IEnumerable<StatusVM>>(model);
        }

        public static IEnumerable<StatusVM> GetAll(bool Actived, bool Blocked)
        {
            IEnumerable<Status> model = RepositoryBase<Status>.GetMulti(x => x.Actived == Actived && x.Blocked == Blocked);
            //return Mapping.Mapper.Map<IEnumerable<CompanyVM>>(model);
            return Mapper.Map<IEnumerable<StatusVM>>(model);
        }

        public static IEnumerable<StatusVM> GetData(string Name)
        {
            List<Status> model = RepositoryBase<Status>.GetMulti(x => x.Name.Contains(Name)).ToList<Status>();
            //return Mapping.Mapper.Map<IEnumerable<ReqStatusVM>>(model);
            return Mapper.Map<IEnumerable<StatusVM>>(model);
        }

        public static bool IsUsed(string Id)
        {
            var query1 = from item in RepositoryBase<Status>.DbContext.Standards
                         where item.StdTypeId == Id
                         select item.Id;
            if (query1.Count() > 0) return true;
            
            return false;
        }

        public static void Update(StatusVM item)
        {
            Status dbItem = GetSingleById(item.Id);
            dbItem.UpdateStatus(item);
            RepositoryBase<Status>.Update(dbItem);
            RepositoryBase<Status>.SaveChanges();
        }

        public static void Delete(StatusVM item)
        {
            Delete(item.Id);
        }

        public static void Delete(string Id)
        {
            RepositoryBase<Status>.DeleteMulti(x => x.Id == Id);
            RepositoryBase<Status>.SaveChanges();
        }

        public static void SetActived(string Id, bool value)
        {
            Status model = RepositoryBase<Status>.GetSingleById(Id);
            model.Actived = value;

            model.UpdatedDate = DateTime.Now;
            model.UpdatedBy = HttpContext.Current.User.Identity.Name;
            RepositoryBase<Status>.SaveChanges();
        }

        public static void SetBlocked(string Id, bool value, string StaffID)
        {
            Status model = GetSingleById(Id);
            model.Blocked = value;

            model.UpdatedDate = DateTime.Now;
            model.UpdatedBy = StaffID;
            RepositoryBase<Status>.SaveChanges();
        }

        public static void SetActivedBlocked(string Id, bool ActivedValue, bool BlockedValue, string StaffID)
        {
            Status model = GetSingleById(Id);
            model.Actived = ActivedValue;
            model.Blocked = BlockedValue;

            model.UpdatedDate = DateTime.Now;
            model.UpdatedBy = StaffID;
            RepositoryBase<Status>.SaveChanges();
        }

        public static void DeleteMulti(List<string> IDs)
        {
            foreach (var item in IDs)
            {
                Delete(item);
            }
        }
        
        public static Status Add(StatusVM item)
        {
            Status dbItem = new Status();
            dbItem.UpdateStatus(item);

            dbItem.Id= item.Id;
            dbItem.Actived = item.Actived;

            dbItem.CreatedDate = DateTime.Now;
            dbItem.CreatedBy = HttpContext.Current.User.Identity.Name;

            Status model = RepositoryBase<Status>.Add(dbItem);
            RepositoryBase<Status>.SaveChanges();
            return dbItem;
        }
        public static Status GetSingleById(string key)
        {
            return RepositoryBase<Status>.GetSingleById(key);
        }

        public static int Count(Expression<Func<Status, bool>> where)
        {
            return RepositoryBase<Status>.Count(where);
        }


        public static List<MenuInfo> GetMenuList(string GroupId, string GroupName)
        {
            List<MenuInfo> menuItems = new List<MenuInfo>();
            var i = 0;
            foreach (var item in GetAll(true, false).Where(x=> x.Group == GroupId))
            {
                menuItems.Add(new MenuInfo() {
                    MenuId = i++,
                    MenuName = item.Id,
                    MenuText = item.Name,
                    MenuUrl = item.Description,
                    MenuGroup = GroupName
                });
            }
            return menuItems;
        }
    }
}
