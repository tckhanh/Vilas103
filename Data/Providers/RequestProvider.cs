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
    public static class RequestProvider
    {
        public static IEnumerable<RequestVM> GetAll()
        {
            IEnumerable<Request> model = RepositoryBase<Request>.GetAll();
            //return Mapping.Mapper.Map<IEnumerable<RequestVM>>(model);
            return Mapper.Map<IEnumerable<RequestVM>>(model);
        }

        public static IEnumerable<RequestVM> GetAll(bool Actived, bool Blocked)
        {
            IEnumerable<Request> model = RepositoryBase<Request>.GetMulti(x => x.Actived == Actived && x.Blocked == Blocked);
            //return Mapping.Mapper.Map<IEnumerable<CompanyVM>>(model);
            return Mapper.Map<IEnumerable<RequestVM>>(model);
        }

        public static IEnumerable<RequestVM> GetData(string Name)
        {
            List<Request> model = RepositoryBase<Request>.GetMulti(x => x.Model.Contains(Name)).ToList<Request>();
            //return Mapping.Mapper.Map<IEnumerable<RequestVM>>(model);
            return Mapper.Map<IEnumerable<RequestVM>>(model);
        }

        public static IEnumerable<RequestVM> GetDataByStatusIds(string StatusIds)
        {
            List<Request> model = RepositoryBase<Request>.GetMulti(x => StatusIds.Contains(x.StatusId)).ToList<Request>();
            return Mapper.Map<IEnumerable<RequestVM>>(model);
        }
        public static long SumPrice(string StdIds)
        {
            var query1 = from item in RepositoryBase<Standard>.DbContext.Standards
                         where StdIds.Contains(item.Id)
                         select item.FeePrice;
            if (query1.Count() > 0)
                return query1.Sum();
            else
                return 0;
        }
        public static bool IsUsed(int Id)
        {
            var query1 = from item in RepositoryBase<Request>.DbContext.Requests
                         where item.Id == Id
                         select item.Id;
            if (query1.Count() > 0) return true;
            
            return false;
        }

        public static void Update(RequestVM item)
        {
            Request dbItem = GetSingleById(item.Id);
            dbItem.UpdateRequest(item);
            RepositoryBase<Request>.Update(dbItem);
            RepositoryBase<Request>.SaveChanges();
        }

        public static void UpdateRegister(RequestVM item)
        {
            Request dbItem = GetSingleById(item.Id);
            dbItem.UpdateRegisterRequest(item);
            RepositoryBase<Request>.Update(dbItem);
            RepositoryBase<Request>.SaveChanges();
        }

        public static void Delete(RequestVM item)
        {
            Delete(item.Id);
        }

        public static void Delete(int Id)
        {
            RepositoryBase<Request>.DeleteMulti(x => x.Id == Id);
            RepositoryBase<Request>.SaveChanges();
        }

        public static void SetActived(int Id, bool value)
        {
            Request model = RepositoryBase<Request>.GetSingleById(Id);
            model.Actived = value;

            model.UpdatedDate = DateTime.Now;
            model.UpdatedBy = HttpContext.Current.User.Identity.Name;
            RepositoryBase<Request>.SaveChanges();
        }

        public static void SetBlocked(int Id, bool value, string StaffID)
        {
            Request model = GetSingleById(Id);
            model.Blocked = value;

            model.UpdatedDate = DateTime.Now;
            model.UpdatedBy = StaffID;
            RepositoryBase<Request>.SaveChanges();
        }

        public static void SetActivedBlocked(int Id, bool ActivedValue, bool BlockedValue, string StaffID)
        {
            Request model = GetSingleById(Id);
            model.Actived = ActivedValue;
            model.Blocked = BlockedValue;

            model.UpdatedDate = DateTime.Now;
            model.UpdatedBy = StaffID;
            RepositoryBase<Request>.SaveChanges();
        }

        public static void DeleteMulti(List<int> IDs)
        {
            foreach (var item in IDs)
            {
                Delete(item);
            }
        }

        public static Request Add(RequestVM item)
        {
            Request dbItem = new Request();
            dbItem.UpdateRequest(item);

            dbItem.Actived = true;
            dbItem.Blocked = false;

            dbItem.CreatedDate = DateTime.Now;
            dbItem.CreatedBy = HttpContext.Current.User.Identity.Name;

            Request model = RepositoryBase<Request>.Add(dbItem);
            RepositoryBase<Request>.SaveChanges();
            return dbItem;
        }

        public static Request AddRegister(RequestVM item)
        {
            Request dbItem = new Request();
            dbItem.UpdateRequest(item);

            dbItem.StatusId = CommonConstants.StatusId_01_Registered;
            dbItem.Actived = true;
            dbItem.Blocked = false;

            dbItem.CreatedDate = DateTime.Now;
            dbItem.CreatedBy = HttpContext.Current.User.Identity.Name;

            Request model = RepositoryBase<Request>.Add(dbItem);
            RepositoryBase<Request>.SaveChanges();
            return dbItem;
        }
        public static Request GetSingleById(int key)
        {
            return RepositoryBase<Request>.GetSingleById(key);
        }

        public static Request GetSingleByCondition(Expression<Func<Request, bool>> expression, string[] includes = null)
        {
            return RepositoryBase<Request>.GetSingleByCondition(expression, includes);
        }
        

        public static int Count(Expression<Func<Request, bool>> where)
        {
            return RepositoryBase<Request>.Count(where);
        }
    }
}
