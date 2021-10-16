using Data.DataModels;

namespace Data.Infrastructure
{
    public class DbFactory : Disposable
    {
        private CFContext dbContext;

        public CFContext Init()
        {
            return dbContext ?? (dbContext = new CFContext());
        }


        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}