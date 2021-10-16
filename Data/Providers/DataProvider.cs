using Data.DataModels;
using Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Data.Providers
{
    public static class DataProvider
    {
        const string VilasDataContextKey = "VilasDataContextKey";
        public static CFContext MyDbContext
        {
            get
            {
                if (HttpContext.Current.Items[VilasDataContextKey] == null)
                    HttpContext.Current.Items[VilasDataContextKey] = new CFContext();
                return (CFContext)HttpContext.Current.Items[VilasDataContextKey];
            }
        }

        #region Company
        public static IEnumerable<Company> GetCompanies()
        {
            var query = from item in MyDbContext.Companies
                        select item;
            MyDbContext.SaveChanges();
            return query.ToList<Company>();
            
        }

        public static int updateCompany(Company entity)
        {
            MyDbContext.Companies.Attach(entity);
            MyDbContext.Entry(entity).State = EntityState.Modified;
            return MyDbContext.SaveChanges();
        }

        #endregion

        #region Others
        #endregion
        //public static bool CompanyIsUsed(int Id)
        //{
        //    var query1 = from item in MyDbContext.Requests
        //                 where item.CompanyID == Id
        //                 select item.RequestID;
        //    if (query1.Count() > 0) return true;

        //    return false;
        //}
    }
}
