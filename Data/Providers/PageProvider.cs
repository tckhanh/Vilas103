using BTS.Data.Infrastructure;
using BTS.Data.Repositories;
using BTS.Model.Models;

namespace Data.Providers
{
    public interface IPageService
    {
        WebPage GetByAlias(string alias);
    }

    public class PageProvider : IPageService
    {
        private IPageRepository _pageRepository;
        private IUnitOfWork _unitOfWork;

        public PageProvider(IPageRepository pageRepository, IUnitOfWork unitOfWork)
        {
            this._pageRepository = pageRepository;
            this._unitOfWork = unitOfWork;
        }

        public WebPage GetByAlias(string alias)
        {
            return _pageRepository.GetSingleByCondition(x => x.Alias == alias);
        }
    }
}