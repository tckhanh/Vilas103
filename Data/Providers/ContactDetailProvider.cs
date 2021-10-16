using Data.DataModels;
using Data.Infrastructure;

namespace Data.Providers
{
    public static class ContactDetailProvider
    {
         public static ContactDetail GetDefaultContact()
        {
            return RepositoryBase<ContactDetail>.GetSingleByCondition(x => x.Status);
        }
    }
}
