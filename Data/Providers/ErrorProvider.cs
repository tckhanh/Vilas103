using Data.DataModels;
using Data.Infrastructure;
using System.Collections.Generic;

namespace Data.Providers
{
    public class ErrorProvider
    {

        public Error Add(Error newError)
        {
            return RepositoryBase<Error>.Add(newError);
        }

        public Error Delete(int Id)
        {
            return RepositoryBase<Error>.Delete(Id);
        }

        public IEnumerable<Error> getAll()
        {
            return RepositoryBase<Error>.GetAll();
        }

        public IEnumerable<Error> getAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return RepositoryBase<Error>.GetMulti(x => x.Id.ToString().Contains(keyword) || x.Message.Contains(keyword));
            else
                return RepositoryBase<Error>.GetAll();
        }

        public Error getByID(string Id)
        {
            return RepositoryBase<Error>.GetSingleById(Id);
        }

        public Error getByID(int Id)
        {
            return RepositoryBase<Error>.GetSingleById(Id);
        }

        public void Update(Error newError)
        {
            RepositoryBase<Error>.Update(newError);
        }

        public Error Create(Error error)
        {
            return RepositoryBase<Error>.Add(error);
        }
    }
}