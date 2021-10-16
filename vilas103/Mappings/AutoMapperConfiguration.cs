using AutoMapper;
using Data.DataVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vilas103.Mappings
{
    public class AutoMapperConfiguration
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<Applicant, ApplicantViewModel>();
                //cfg.CreateMap<Operator, OperatorViewModel>();
                //lots more maps...?
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Data.DataModels.Company, CompanyVM>();
                //cfg.CreateMap<InCaseOf, InCaseOfViewModel>();
                //cfg.CreateMap<InCaseOf, InCaseOfTabVM>();
                //cfg.CreateMap<Lab, LabViewModel>();
                //cfg.CreateMap<Lab, LabTabVM>();
                //cfg.CreateMap<Operator, OperatorViewModel>();
                //cfg.CreateMap<Operator, OperatorTabVM>();
                //cfg.CreateMap<Applicant, ApplicantViewModel>();
                //cfg.CreateMap<Applicant, ApplicantTabVM>();
                //cfg.CreateMap<City, CityViewModel>();
                //cfg.CreateMap<City, CityTabVM>();
                //cfg.CreateMap<District, DistrictVM>();
                //cfg.CreateMap<Ward, WardVM>();
                //cfg.CreateMap<AreaTab, AreaTabVM>();
                //cfg.CreateMap<Equipment, EquipmentVM>();
                //cfg.CreateMap<EquipmentTab, EquipmentTabVM>();

                //cfg.CreateMap<Model.Models.Profile, ProfileViewModel>();
                //cfg.CreateMap<Bts, BtsViewModel>();
                //cfg.CreateMap<Certificate, CertificateViewModel>();
                //cfg.CreateMap<Certificate, PrintCertificateViewModel>();                
                //cfg.CreateMap<NoCertificate, NoCertificateViewModel>();
                //cfg.CreateMap<NoRequiredBts, NoRequiredBtsVM>();
                //cfg.CreateMap<SubBtsInCert, SubBtsInCertViewModel>();
                //cfg.CreateMap<SubBtsInNoRequiredBts, SubBtsInNoRequiredBtsVM>();
                //cfg.CreateMap<Footer, FooterViewModel>();
                //cfg.CreateMap<Slide, SlideViewModel>();
                //cfg.CreateMap<WebPage, PageViewModel>();
                //cfg.CreateMap<ContactDetail, ContactDetailViewModel>();

                //cfg.CreateMap<ApplicationGroup, ApplicationGroupViewModel>();
                //cfg.CreateMap<ApplicationRole, ApplicationRoleViewModel>();
                //cfg.CreateMap<ApplicationUser, ApplicationUserViewModel>();
                //cfg.CreateMap<ReportTT18Cert, ReportTT18CertViewModel>();
                //cfg.CreateMap<ReportTT18NoCert, ReportTT18NoCertViewModel>();
                //cfg.CreateMap<Licence, LicenceViewModel>();

                //cfg.CreateMap<Error, ErrorVM>();
                //cfg.CreateMap<Audit, AuditVM>();
                //cfg.CreateMap<SystemConfig, ConfigVM>();
                /* etc */
            });
        }
    }
}