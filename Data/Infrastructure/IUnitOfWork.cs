namespace Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}