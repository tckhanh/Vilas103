using Data.DataModels;
using Data.Infrastructure;
using System.Collections.Generic;

namespace Data.Providers
{
    public interface IAuditService
    {
        Audit Add(Audit newAudit);

        void Update(Audit newAudit);

        Audit Delete(int Id);

        IEnumerable<Audit> getAll();

        IEnumerable<Audit> getAll(string keyword);

        Audit getByID(string Id);

        Audit getByID(int Id);

        Audit Create(Audit error);

        void Save();
    }

    public class AuditService : IAuditService
    {

        public Audit Add(Audit newAudit)
        {
            return RepositoryBase<Audit>.Add(newAudit);
        }

        public Audit Delete(int Id)
        {
            return RepositoryBase<Audit>.Delete(Id);
        }

        public IEnumerable<Audit> getAll()
        {
            return RepositoryBase<Audit>.GetAll();
        }

        public IEnumerable<Audit> getAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return RepositoryBase<Audit>.GetMulti(x => x.Id.ToString().Contains(keyword) || x.UserName.Contains(keyword));
            else
                return RepositoryBase<Audit>.GetAll();
        }

        public Audit getByID(string Id)
        {
            return RepositoryBase<Audit>.GetSingleById(Id);
        }

        public Audit getByID(int Id)
        {
            return RepositoryBase<Audit>.GetSingleById(Id);
        }

        public void Update(Audit newAudit)
        {
            RepositoryBase<Audit>.Update(newAudit);
        }

        public Audit Create(Audit error)
        {
            return RepositoryBase<Audit>.Add(error);
        }
    }
}