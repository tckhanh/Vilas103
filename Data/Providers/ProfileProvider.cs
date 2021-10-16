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
    public interface IProfileService
    {
        Profile Add(Profile newProfile);

        void Update(Profile newProfile);

        Profile Delete(string Id);

        IEnumerable<Profile> getAll();

        IEnumerable<Profile> getAll(string keyword);

        IEnumerable<Applicant> getAllApplicant();

        Profile getByID(string Id, string[] includes = null);

        bool IsUsed(string Id);

        void Save();
    }

    public class ProfileProvider : IProfileService
    {
        private IProfileRepository _profileRepository;
        private IApplicantRepository _applicantRepository;
        private IUnitOfWork _unitOfWork;

        public ProfileProvider(IProfileRepository profileRepository, IApplicantRepository applicantRepository, IUnitOfWork unitOfWork)
        {
            _profileRepository = profileRepository;
            _applicantRepository = applicantRepository;
            _unitOfWork = unitOfWork;
        }

        public Profile Add(Profile newProfile)
        {
            return _profileRepository.Add(newProfile);
        }

        public Profile Delete(string Id)
        {
            return _profileRepository.Delete(Id);
        }

        public IEnumerable<Profile> getAll()
        {
            return _profileRepository.GetAll(includes: new string[] { "Applicant" });
        }

        public IEnumerable<Profile> getAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _profileRepository.GetMulti(x => x.ProfileNum.Contains(keyword) || x.ApplicantID.Contains(keyword));
            else
                return _profileRepository.GetAll();
        }

        public Profile getByID(string Id, string[] includes = null)
        {
            return _profileRepository.GetSingleByCondition(x => x.Id == Id, includes);
        }

        public bool IsUsed(string Id)
        {
            return _profileRepository.IsUsed(Id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Profile newProfile)
        {
            _profileRepository.Update(newProfile);
        }

        public IEnumerable<Applicant> getAllApplicant()
        {
            return _applicantRepository.GetAll();
        }
    }
}