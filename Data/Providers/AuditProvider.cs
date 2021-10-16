using AutoMapper;
using BTS.Web.Infrastructure.Extensions;
using BTS.Web.Models;
using Data.DataModels;
using Data.DataVMs;
using Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Providers
{
    public static class AuditProvider
    {
        public static IEnumerable<AuditVM> GetAll()
        {
            List<Audit> model = RepositoryBase<Audit>.GetAll().ToList<Audit>();
            //return Mapping.Mapper.Map<IEnumerable<AuditVM>>(model);
            return Mapper.Map<IEnumerable<AuditVM>>(model);
        }

        public static bool IsUsed(int Id)
        {
            //var query1 = from item in RepositoryBase<Audit>.DbContext.Requests
            //             where item.AuditID == Id
            //             select item.RequestID;
            //if (query1.Count() > 0) return true;

            return false;
        }

        public static void Update(AuditVM item)
        {
            Audit dbItem = RepositoryBase<Audit>.GetSingleById(item.Id);
            dbItem.UpdateAudit(item);
            RepositoryBase<Audit>.Update(dbItem);
            RepositoryBase<Company>.SaveChanges();
        }

        public static void Delete(int ID)
        {
            RepositoryBase<Audit>.DeleteMulti(x => x.Id == ID);
            RepositoryBase<Company>.SaveChanges();
        }

        public static void DeleteMulti(List<int> IDs)
        {
            RepositoryBase<Audit>.DeleteMulti(x => IDs.Contains(x.Id));
            RepositoryBase<Audit>.SaveChanges();
        }
        public static Audit Add(AuditVM item)
        {
            Audit dbItem = new Audit();
            dbItem.UpdateAudit(item);
            Audit model = RepositoryBase<Audit>.Add(dbItem);
            RepositoryBase<Company>.SaveChanges();
            return dbItem;
        }
        public static AuditVM GetSingleById(int key)
        {
            Audit model = RepositoryBase<Audit>.GetSingleById(key);
            return Mapper.Map<AuditVM>(model);
        }
    }
}
