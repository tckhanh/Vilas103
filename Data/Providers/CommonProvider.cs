using Data.Common;
using Data.DataModels;
using Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Providers
{
   public class CommonProvider
    {
        public Footer GetFooter()
        {
            return RepositoryBase<Footer>.GetSingleByCondition(x => x.Id == CommonConstants.DefaultFooterId);
        }

        public IEnumerable<Slide> GetSlides()
        {
            return RepositoryBase<Slide>.GetMulti(x => x.Status);
        }

        public SysConfig GetSysConfig(string Id)
        {
            return RepositoryBase<SysConfig>.GetSingleByCondition(x => x.Id == Id);
        }
    }
}