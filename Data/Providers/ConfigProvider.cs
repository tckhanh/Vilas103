using Data.DataModels;
using Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Providers
{
    public class ConfigProvider
    {
        public SystemConfig Add(SystemConfig newConfig)
        {
            return RepositoryBase<SystemConfig>.Add(newConfig);
        }

        public SystemConfig Delete(int Id)
        {
            return RepositoryBase<SystemConfig>.Delete(Id);
        }

        public IEnumerable<SystemConfig> getAll()
        {
            return RepositoryBase<SystemConfig>.GetAll();
        }

        public IEnumerable<SystemConfig> getAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return RepositoryBase<SystemConfig>.GetMulti(x => x.Id.ToString().Contains(keyword) || x.Code.Contains(keyword));
            else
                return RepositoryBase<SystemConfig>.GetAll();
        }

        public SystemConfig getByID(string Id)
        {
            return RepositoryBase<SystemConfig>.GetSingleById(Id);
        }

        public SystemConfig getByID(int Id)
        {
            return RepositoryBase<SystemConfig>.GetSingleById(Id);
        }

        public SystemConfig getByCode(string code)
        {
            return RepositoryBase<SystemConfig>.GetSingleByCondition(x => x.Code == code);
        }

        public bool IsUsed(int Id)
        {
            return false;
        }

        public void Update(SystemConfig newConfig)
        {
            RepositoryBase<SystemConfig>.Update(newConfig);
        }
    }
}