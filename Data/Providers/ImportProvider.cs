using BTS.Common;
using BTS.Data.Infrastructure;
using BTS.Data.Repository;
using BTS.Model.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Providers
{
    public interface IImportService
    {
        IEnumerable<InCaseOf> GetInCaseOfs();
        IEnumerable<Lab> GetLabs();
        IEnumerable<Operator> GetOperators();
        IEnumerable<Applicant> GetApplicants();
        IEnumerable<City> GetCities();
        IEnumerable<AreaTab> GetAreaTabs();
        IEnumerable<EquipmentTab> GetEquipmentTabs();

        Profile findProfile(string applicantID, string profileNum, DateTime profileDate);

        Profile getProfile(string Id);
        District getDistrict(string Id);
        Equipment getEquipment(int Id);
        Equipment getEquipment(string Name, string Band, string OperatorRootID);
        Ward getWard(string Id);

        Bts getBts(int Id);

        Operator getOperatorByName(string Name);

        Applicant findApplicant(string ApplicantID);

        IEnumerable<Bts> findBtssByProfile(string profileID);

        IEnumerable<Certificate> findCertsByProfile(string profileID);

        Bts findBts(string profileID, string btsCode);

        SubBtsInCert findSubBts(string certificateID, string btsCode, string operatorID);

        Certificate getCertificate(string Id);        

        IEnumerable<Certificate> getCertificatesByProfile(string profileID);

        NoCertificate getNoCertificate(string Id);        

        Certificate findCertificate(string BtsCode, string ProfileID);

        NoCertificate findNoCertificate(string BtsCode, string ProfileID);

        NoRequiredBts findNoRequiredBts(string BtsCode, string OperatorID, string AnnouncedDoc);

        bool Add(InCaseOf item);

        bool Add(Lab item);

        bool Add(City item);
        bool Add(District item);
        bool Add(Ward item);
        bool Add(Equipment item);
        bool Add(Operator item);
        bool Add(Applicant item);

        bool Add(Profile item);

        bool Add(Bts item);

        bool Add(Certificate item);

        bool Add(NoRequiredBts item);

        bool Add(NoCertificate item);

        bool Add(SubBtsInCert item);
        bool Add(SubBtsInNoRequiredBts item);
        void Update(District item);
        void Update(Equipment item);
        void Update(Ward item);
        void Update(InCaseOf newInCaseOf);

        void Update(Profile newProfile);

        void Update(Bts newBts);

        void Update(Certificate newCertificate);

        void Update(NoRequiredBts newCertificate);

        void Update(NoCertificate newNoCertificate);

        void Delete(Bts bts);

        void Delete(Certificate certificate);

        void RemoveBtsInProfile(string ProFileID);

        void RemoveCertsInProfile(string ProFileID);

        void RemoveSubBtsInCert(string CertificateID);

        void RemoveSubBtsInNoRequiredBts(string NoRequiredBtsID);

        void Save();

        Applicant getApplicant(string proFileID);

        IEnumerable<string> getLastOwnCertificateIDs(string btsCode, string operatorID);

        IEnumerable<string> getLastNoOwnCertificateIDs(string btsCode, string operatorID);

        IEnumerable<Certificate> getLastOwnCertificates(string btsCode, string operatorID);

        IEnumerable<Certificate> getLastNoOwnCertificates(string btsCode, string operatorID);

        Certificate getLastOwnCertificate(string btsCode, string operatorID);

        Certificate getLastNoOwnCertificate(string btsCode, string operatorID);

        IEnumerable<Profile> findProfilesBtsInProcess(string btsCode, string operatorID);

        IEnumerable<Profile> findProfilestNoCertificate(string btsCode, string operatorID);

        IEnumerable<NoCertificate> findBtsNoCertificate(string btsCode, string operatorID);
        
    }

    public class ImportProvider : IImportService
    {
        private IInCaseOfRepository _inCaseOfRepository;
        private ILabRepository _labRepository;
        private ICityRepository _cityRepository;
        private IDistrictRepository _districtRepository;
        private IWardRepository _wardRepository;
        private IEquipmentRepository _equipmentRepository;
        private IOperatorRepository _operatorRepository;
        private IApplicantRepository _applicantRepository;
        private IProfileRepository _profileRepository;
        private IBtsRepository _btsRepository;
        private ICertificateRepository _certificateRepository;
        private INoCertificateRepository _noCertificateRepository;
        private ISubBtsInCertRepository _subBTSinCertRepository;
        private ISubBtsInNoRequiredBtsRepository _subBTSinNoRequiredBtsRepository;
        private INoRequiredBtsRepository _noRequiredBtsRepository;
        private IUnitOfWork _unitOfWork;

        public ImportProvider(IInCaseOfRepository inCaseOfRepository, ILabRepository labRepository, ICityRepository cityRepository,
            IDistrictRepository districtRepository, IWardRepository wardRepository, IEquipmentRepository equipmentRepository,
            IOperatorRepository operatorRepository, IApplicantRepository applicantRepository, IProfileRepository profileRepository,
            IBtsRepository btsRepository, ICertificateRepository certificateRepository, INoCertificateRepository noCertificateRepository,
            ISubBtsInCertRepository subBTSinCertRepository, INoRequiredBtsRepository noRequiredBtsRepository,
            ISubBtsInNoRequiredBtsRepository subBTSinNoRequiredBtsRepository, IUnitOfWork unitOfWork)        
        {
            _inCaseOfRepository = inCaseOfRepository;
            _labRepository = labRepository;
            _cityRepository = cityRepository;
            _districtRepository = districtRepository;
            _wardRepository = wardRepository;
            _equipmentRepository = equipmentRepository;
            _operatorRepository = operatorRepository;
            _applicantRepository = applicantRepository;
            _profileRepository = profileRepository;
            _btsRepository = btsRepository;
            _certificateRepository = certificateRepository;
            _noCertificateRepository = noCertificateRepository;
            _subBTSinCertRepository = subBTSinCertRepository;
            _noRequiredBtsRepository = noRequiredBtsRepository;
            _subBTSinNoRequiredBtsRepository = subBTSinNoRequiredBtsRepository;
            _unitOfWork = unitOfWork;
        }

        public bool Add(InCaseOf item)
        {
            if (_inCaseOfRepository.GetSingleById(item.Id) == null && !_inCaseOfRepository.Exists(m => m.Name.Trim().ToUpper() == item.Name.ToUpper()))
            {
                item.CreatedDate = DateTime.Now;
                _inCaseOfRepository.Add(item);
            }
            return true;
        }

        public bool Add(Lab item)
        {
            if (_labRepository.GetSingleById(item.Id) == null && !_labRepository.Exists(m => m.Name.Trim().ToUpper() == item.Name.ToUpper()))
            {
                item.CreatedDate = DateTime.Now;
                _labRepository.Add(item);
            }
            return true;
        }

        public bool Add(City item)
        {
            if (_cityRepository.GetSingleById(item.Id) == null && !_cityRepository.Exists(m => m.Name.Trim().ToUpper() == item.Name.ToUpper()))
            {
                item.CreatedDate = DateTime.Now;
                _cityRepository.Add(item);
            }
            return true;
        }

        public bool Add(District item)
        {
            if (_districtRepository.GetSingleById(item.Id) == null && !_districtRepository.Exists(m => m.CityId.Trim().ToUpper() == item.CityId.ToUpper() && m.Name.Trim().ToUpper() == item.Name.ToUpper()))
            {
                item.CreatedDate = DateTime.Now;
                _districtRepository.Add(item);
            }
            return true;
        }

        public bool Add(Ward item)
        {
            if (_wardRepository.GetSingleById(item.Id) == null && !_wardRepository.Exists(m => m.DistrictId.Trim().ToUpper() == item.DistrictId.ToUpper() && m.Name.Trim().ToUpper() == item.Name.ToUpper()))
            {
                item.CreatedDate = DateTime.Now;
                _wardRepository.Add(item);
            }
            return true;
        }

        public bool Add(Equipment item)
        {
            if (!_equipmentRepository.Exists(m => m.OperatorRootID.Trim().ToUpper() == item.OperatorRootID.ToUpper() && m.Name.Trim().ToUpper() == item.Name.ToUpper() && m.Band.Trim().ToUpper() == item.Band.ToUpper() && m.Tx == item.Tx))
            {
                item.CreatedDate = DateTime.Now;
                _equipmentRepository.Add(item);
            }
            return true;
        }

        public bool Add(Operator item)
        {
            if (_operatorRepository.GetSingleById(item.Id) == null && !_operatorRepository.Exists(m => m.Name.Trim().ToUpper() == item.Name.ToUpper()))
            {
                item.CreatedDate = DateTime.Now;
                _operatorRepository.Add(item);
            }
            return true;
        }

        public bool Add(Applicant item)
        {
            if (_applicantRepository.GetSingleById(item.Id) == null && !_applicantRepository.Exists(m => m.Name.Trim().ToUpper() == item.Name.ToUpper()))
            {
                item.CreatedDate = DateTime.Now;
                _applicantRepository.Add(item);
            }
            return true;
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public bool Add(Profile item)
        {
            if (_profileRepository.GetSingleById(item.Id) == null)
            {
                _profileRepository.Add(item);
            }
            return true;
        }

        public bool Add(Bts item)
        {
            if (_btsRepository.GetSingleById(item.Id) == null)
            {
                _btsRepository.Add(item);
            }
            return true;
        }

        public bool Add(Certificate item)
        {
            if (_certificateRepository.GetSingleById(item.Id) == null)
            {
                _certificateRepository.Add(item);
            }
            return true;
        }

        public bool Add(NoRequiredBts item)
        {
            if (_noRequiredBtsRepository.GetSingleById(item.Id) == null)
            {
                _noRequiredBtsRepository.Add(item);
            }
            return true;
        }


        public bool Add(NoCertificate item)
        {
            if (_noCertificateRepository.GetSingleById(item.Id) == null)
            {
                _noCertificateRepository.Add(item);
            }
            return true;
        }

        public bool Add(SubBtsInCert item)
        {
            if (_subBTSinCertRepository.GetSingleById(item.Id) == null)
            {
                _subBTSinCertRepository.Add(item);
            }
            return true;
        }

        public bool Add(SubBtsInNoRequiredBts item)
        {
            if (_subBTSinNoRequiredBtsRepository.GetSingleById(item.Id) == null)
            {
                _subBTSinNoRequiredBtsRepository.Add(item);
            }
            return true;
        }

        public void Update(Profile item)
        {
            item.UpdatedDate = DateTime.Now;
            _profileRepository.Update(item);
        }

        public void Update(Bts item)
        {
            item.UpdatedDate = DateTime.Now;
            _btsRepository.Update(item);
        }

        public void Update(InCaseOf item)
        {            
            item.UpdatedDate = DateTime.Now;
            _inCaseOfRepository.Update(item);
        }

        public void Update(District item)
        {
            item.UpdatedDate = DateTime.Now;
            _districtRepository.Update(item);
        }

        public void Update(Equipment item)
        {
            item.UpdatedDate = DateTime.Now;
            _equipmentRepository.Update(item);
        }

        public void Update(Ward item)
        {
            item.UpdatedDate = DateTime.Now;
            _wardRepository.Update(item);
        }

        public void Update(Certificate item)
        {
            item.UpdatedDate = DateTime.Now;
            _certificateRepository.Update(item);
        }

        public void Update(NoRequiredBts item)
        {
            item.UpdatedDate = DateTime.Now;
            _noRequiredBtsRepository .Update(item);
        }

        public void Update(NoCertificate item)
        {
            item.UpdatedDate = DateTime.Now;
            _noCertificateRepository.Update(item);
        }


        public Profile findProfile(string applicantID, string profileNum, DateTime profileDate)
        {
            return _profileRepository.findProfile(applicantID, profileNum, profileDate);
        }

        public Profile getProfile(string Id)
        {
            return _profileRepository.GetSingleById(Id);
        }

        public Bts getBts(int Id)
        {
            return _btsRepository.GetSingleById(Id);
        }

        public Bts findBts(string profileID, string btsCode)
        {
            return _btsRepository.GetSingleByCondition(x => x.ProfileID == profileID && x.BtsCode == btsCode);
        }

        public IEnumerable<Bts> findBtssByProfile(string profileID)
        {
            return _btsRepository.GetMulti(x => x.ProfileID == profileID);
        }

        public IEnumerable<Certificate> findCertsByProfile(string profileID)        
        {
            return _certificateRepository.GetMulti(x => x.ProfileID == profileID);
        }

        public Certificate getCertificate(string Id)
        {
            return _certificateRepository.GetSingleById(Id);
        }

        public IEnumerable<Certificate> getCertificatesByProfile(string profileID)
        {
            return _certificateRepository.GetMulti(x => x.ProfileID == profileID);
        }

        public NoCertificate getNoCertificate(string Id)
        {
            return _noCertificateRepository.GetSingleById(Id);
        }

        public Certificate findCertificate(string BtsCode, string ProfileID)
        {
            return _certificateRepository.GetSingleByCondition(x => x.BtsCode == BtsCode && x.ProfileID == ProfileID);
        }

        public NoCertificate findNoCertificate(string BtsCode, string ProfileID)
        {
            return _noCertificateRepository.GetSingleByCondition(x => x.BtsCode == BtsCode && x.ProfileID == ProfileID);
        }

        public NoRequiredBts findNoRequiredBts(string BtsCode, string OperatorID, string AnnouncedDoc)
        {
            return _noRequiredBtsRepository.GetSingleByCondition(x => x.BtsCode == BtsCode && x.OperatorID == OperatorID && x.AnnouncedDoc == AnnouncedDoc);
        }

        public SubBtsInCert findSubBts(string certificateID, string btsCode, string operatorID)
        {
            return _subBTSinCertRepository.GetSingleByCondition(x => x.CertificateID == certificateID && x.BtsCode == btsCode && x.OperatorID == operatorID);
        }

        public void RemoveSubBtsInCert(string CertificateID)
        {
            _subBTSinCertRepository.DeleteMulti(x => x.CertificateID == CertificateID);
        }

        public void RemoveSubBtsInNoRequiredBts(string NoRequiredBtsID)
        {
            _subBTSinNoRequiredBtsRepository.DeleteMulti(x => x.NoRequiredBtsID == NoRequiredBtsID);
        }


        public void RemoveBtsInProfile(string ProFileID)
        {
            _btsRepository.DeleteMulti(x => x.ProfileID == ProFileID);
        }

        public void RemoveCertsInProfile(string ProFileID)
        {
            IEnumerable<Certificate> CertList = getCertificatesByProfile(ProFileID).ToList();
            foreach (var item in CertList)
            {
                RemoveSubBtsInCert(item.Id);
                Delete(item);
            }            
        }

        public Applicant getApplicant(string proFileID)
        {
            Profile profileItem = _profileRepository.GetSingleByCondition(x => x.Id == proFileID);
            return _applicantRepository.GetSingleByCondition(x => x.Id == profileItem.ApplicantID);
        }

        public IEnumerable<string> getLastOwnCertificateIDs(string btsCode, string operatorID)
        {
            return _certificateRepository.getLastOwnCertificates(btsCode, operatorID).Select(x => x.Id);
        }

        public IEnumerable<string> getLastNoOwnCertificateIDs(string btsCode, string operatorID)
        {
            return _certificateRepository.getLastNoOwnCertificates(btsCode, operatorID).Select(x => x.Id);
        }

        public IEnumerable<Certificate> getLastOwnCertificates(string btsCode, string operatorID)
        {
            return _certificateRepository.getLastOwnCertificates(btsCode, operatorID);
        }

        public IEnumerable<Certificate> getLastNoOwnCertificates(string btsCode, string operatorID)
        {
            return _certificateRepository.getLastNoOwnCertificates(btsCode, operatorID);
        }

        public Certificate getLastOwnCertificate(string btsCode, string operatorID)
        {
            return _certificateRepository.getLastOwnCertificates(btsCode, operatorID).FirstOrDefault();
        }

        public Certificate getLastNoOwnCertificate(string btsCode, string operatorID)
        {
            return _certificateRepository.getLastNoOwnCertificates(btsCode, operatorID).FirstOrDefault();
        }

        public void Delete(Bts bts)
        {
            _btsRepository.Delete(bts);
        }

        public void Delete(Certificate certificate)
        {
            _certificateRepository.Delete(certificate);
        }

        public IEnumerable<Profile> findProfilesBtsInProcess(string btsCode, string operatorID)
        {
            return _profileRepository.findProfilesBtsInProcess(btsCode, operatorID);
        }

        public IEnumerable<Profile> findProfilestNoCertificate(string btsCode, string operatorID)
        {
            return _profileRepository.findProfilesBTSNoCertificate(btsCode, operatorID);
        }

        public IEnumerable<NoCertificate> findBtsNoCertificate(string btsCode, string operatorID)
        {
            return _noCertificateRepository.GetMulti(x => x.BtsCode == btsCode && x.OperatorID == operatorID);
        }

        public Operator getOperatorByName(string Name)
        {
            return _operatorRepository.GetSingleByCondition(x => x.Name == Name);            
        }

        public Applicant findApplicant(string ApplicantID)
        {
            return _applicantRepository.GetSingleById(ApplicantID);
        }

        public IEnumerable<City> GetCities()
        {
            return _cityRepository.GetAll().OrderBy(x=> x.Id);
        }

        public IEnumerable<AreaTab> GetAreaTabs()
        {

            return _wardRepository.GetAreaTabs();
        }

        public IEnumerable<EquipmentTab> GetEquipmentTabs()
        {

            return _equipmentRepository.GetEquipmentTabs();
        }

        public IEnumerable<InCaseOf> GetInCaseOfs()
        {
            return _inCaseOfRepository.GetAll().OrderBy(x => x.Id);
        }

        public IEnumerable<Lab> GetLabs()
        {
            return _labRepository.GetAll().OrderBy(x => x.Id);
        }

        public IEnumerable<Operator> GetOperators()
        {
            return _operatorRepository.GetAll().OrderBy(x => x.Id);
        }

        public IEnumerable<Applicant> GetApplicants()
        {
            return _applicantRepository.GetAll().OrderBy(x => x.Id);
        }

        public District getDistrict(string Id)
        {
            return _districtRepository.GetSingleById(Id);
        }

        public Equipment getEquipment(int Id)
        {
            return _equipmentRepository.GetSingleById(Id);
        }

        public Equipment getEquipment(string Name, string Band, string OperatorRootID)
        {
            return _equipmentRepository.GetSingleByCondition(x=> x.Name.Trim().ToUpper() == Name.Trim().ToUpper() && x.Band.Trim().ToUpper() == Band.Trim().ToUpper() && x.OperatorRootID.Trim().ToUpper() == OperatorRootID.Trim().ToUpper()) ;
        }

        public Ward getWard(string Id)
        {
            return _wardRepository.GetSingleById(Id);
        }
    }
}