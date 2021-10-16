
using Data.DataModels;
using Data.Infrastructure;

namespace Data.Providers
{
    
    public class FeedbackProvider
    {
        
        public Feedback Create(Feedback feedback)
        {
            return RepositoryBase<Feedback>.Add(feedback);
        }
    }
}