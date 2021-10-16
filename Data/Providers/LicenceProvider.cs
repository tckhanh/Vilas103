using BTS.Data.Infrastructure;
using BTS.Data.Repository;
using BTS.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Providers
{
    public interface ILicenceService
    {
        Licence Add(Licence newLicence);

        void Update(Licence newLicence);

        Licence Delete(string Id);

        IEnumerable<Licence> getAll();

        IEnumerable<Licence> getAll(string keyword);

        Licence getByID(string Id);

        Licence getByID(int Id);

        bool IsUsed(string Id);
        void Save();
    }

    public class LicenceService : ILicenceService
    {
        private ILicenceRepository _licenceRepository;
        private IUnitOfWork _unitOfWork;

        public LicenceService(ILicenceRepository labRepository, IUnitOfWork unitOfWork)
        {
            _licenceRepository = labRepository;
            _unitOfWork = unitOfWork;
        }

        public Licence Add(Licence newLicence)
        {
            return _licenceRepository.Add(newLicence);
        }

        public Licence Delete(string Id)
        {
            return _licenceRepository.Delete(Id);
        }

        public IEnumerable<Licence> getAll()
        {
            return _licenceRepository.GetAll();
        }

        public IEnumerable<Licence> getAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _licenceRepository.GetMulti(x => x.key.ToString().Contains(keyword));
            else
                return _licenceRepository.GetAll();
        }

        public Licence getByID(string Id)
        {
            return _licenceRepository.GetSingleById(Id);
        }

        public Licence getByID(int Id)
        {
            return _licenceRepository.GetSingleById(Id);
        }

        public bool IsUsed(string Id)
        {
            return _licenceRepository.IsUsed(Id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Licence newLicence)
        {
            _licenceRepository.Update(newLicence);
        }
    }
}