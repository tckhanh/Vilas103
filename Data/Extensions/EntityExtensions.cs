using BTS.Web.Models;
using Data.DataModels;
using Data.DataVMs;
using System;

namespace BTS.Web.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static void UpdateCompany(this Company item, CompanyVM itemVM)
        {
            item.Name = itemVM.Name;
            item.Id = itemVM.Id;
            item.Address = itemVM.Address;
            item.PhoneNo = itemVM.PhoneNo;
            item.FaxNo = itemVM.FaxNo;
            item.Contract = itemVM.Contract;
            item.TaxCompanyName = itemVM.TaxCompanyName;
            item.TaxCode = itemVM.TaxCode;
            item.TaxAddress = itemVM.TaxAddress;
            item.ContactName = itemVM.ContactName;
            item.ContactPhone = itemVM.ContactPhone;
            item.TaxEmail = itemVM.TaxEmail;
            item.UserName = itemVM.UserName;
        }

        public static void UpdateLab(this Lab item, LabVM itemVM)
        {
            item.Name = itemVM.Name;
            item.Address = itemVM.Address;
            item.Phone = itemVM.Phone;
            item.Fax = itemVM.Fax;
        }

        public static void UpdateFeedback(this Feedback item, FeedbackVM itemVM)
        {
            item.Name = itemVM.Name;
            item.Email = itemVM.Email;
            item.Message = itemVM.Message;
            item.Status = itemVM.Status;
            item.CreatedDate = DateTime.Now;
        }

        public static void UpdateApplicationGroup(this ApplicationGroup item, ApplicationGroupVM itemVM)
        {
            item.Name = itemVM.Name;
            item.Description = itemVM.Description;
        }

        public static void UpdateApplicationRole(this ApplicationRole item, ApplicationRoleVM itemVM, String action = "add")
        {
            if (action == "add")
                item.Id = Guid.NewGuid().ToString();
            item.Name = itemVM.Name;
            item.Description = itemVM.Description;
        }

        public static void UpdateApplicationUser(this ApplicationUser item, ApplicationUserVM itemVM)
        {
            item.UserName = itemVM.UserName;
            item.FullName = itemVM.FullName;
            item.Email = itemVM.Email;
            item.PhoneNumber = itemVM.PhoneNumber;
            item.Address = itemVM.Address;
            item.BirthDay = itemVM.BirthDay;
            item.FatherLand = itemVM.FatherLand;
            item.Level = itemVM.Level;
            item.EducationalField = itemVM.EducationalField;
            item.EntryDate = itemVM.EntryDate;
            item.EndDate = itemVM.EndDate;
            item.Locked = itemVM.Locked;
            item.JobPositions = itemVM.JobPositions;
            item.ImagePath = itemVM.ImagePath;
            item.CityIDsScope = itemVM.CityIDsScope;
            item.AreasScope = itemVM.AreasScope;
        }

        public static void UpdateUserProfile(this ApplicationUser item, ApplicationUserVM itemVM)
        {
            item.FullName = itemVM.FullName;
            item.Email = itemVM.Email;
            item.PhoneNumber = itemVM.PhoneNumber;
            item.Address = itemVM.Address;
            item.BirthDay = itemVM.BirthDay;
            item.FatherLand = itemVM.FatherLand;
            item.Level = itemVM.Level;
            item.EducationalField = itemVM.EducationalField;
            item.EntryDate = itemVM.EntryDate;
            item.EndDate = itemVM.EndDate;
            item.Locked = itemVM.Locked;
            item.JobPositions = itemVM.JobPositions;
            item.ImagePath = itemVM.ImagePath;
        }

        public static void UpdateGroup(this ApplicationGroup item, ApplicationGroupVM itemVM)
        {
            item.Name = itemVM.Name;
            item.Description = itemVM.Description;
        }


        public static void UpdateLicence(this Licence item, LicenceVM itemVM)
        {
            item.key = itemVM.key;
            item.enable = itemVM.enable;
        }

        public static void UpdateError(this Error item, ErrorVM itemVM)
        {
            item.Message = itemVM.Message;
            item.Description = itemVM.Description;
            item.Controller = itemVM.Controller;
            item.StackTrace = itemVM.StackTrace;
            item.CreatedDate = itemVM.CreatedDate;
        }

        public static void UpdateAudit(this Audit item, AuditVM itemVM)
        {
            item.Data = itemVM.Data;
            item.IPAddress = itemVM.IPAddress;
            item.SessionID = itemVM.SessionID;
            item.TimeAccessed = itemVM.TimeAccessed;
            item.URLAccessed = itemVM.URLAccessed;
            item.UserName = itemVM.UserName;
        }

        public static void UpdateStandardType(this StdType item, StdTypeVM itemVM)
        {
            item.Name = itemVM.Name;
        }

        public static void UpdateStandard(this Standard item, StandardVM itemVM)
        {
            item.Name = itemVM.Name;
            item.StdTypeId = itemVM.StdTypeId;
            item.Abstract = itemVM.Abstract;
            item.FileName = itemVM.FileName;
            item.URL = itemVM.URL;
            item.IssueDate = itemVM.IssueDate;
            item.ValidDate = itemVM.ValidDate;
            item.FeeDoc = itemVM.FeeDoc;
            item.FeePrice = itemVM.FeePrice;
            item.LabID = itemVM.LabID;
        }

        public static void UpdateEquType(this EquType item, EquTypeVM itemVM)
        {
            item.Name = itemVM.Name;
            item.DisplayName = itemVM.DisplayName;
            item.Standards = itemVM.Standards;
            item.Price = itemVM.Price;
            item.Description = itemVM.Description;
        }

        public static void UpdateEquGroup(this EquGroup item, EquGroupVM itemVM)
        {
            item.Name = itemVM.Name;
        }

        public static void UpdateStatus(this Status item, StatusVM itemVM)
        {
            item.Name = itemVM.Name;
            item.LinkedIds = itemVM.LinkedIds;
            item.Group = item.Group;
            item.Description = itemVM.Description;
        }

        public static void UpdateRequest(this Request item, RequestVM itemVM)
        {
            item.RegisteredNo = itemVM.RegisteredNo;
            item.ReceivedNo = itemVM.ReceivedNo;
            item.ContractNo = itemVM.ContractNo;
            item.CompanyId = itemVM.CompanyId;
            item.EquTypeId = itemVM.EquTypeId;
            item.EquGroupId = itemVM.EquGroupId;
            item.EquName = itemVM.EquName;
            item.Manufacturer = itemVM.Manufacturer;
            item.Model = itemVM.Model;
            item.Serial_Imei = itemVM.Serial_Imei;
            item.MadeIn = itemVM.MadeIn;
            item.Standards = itemVM.Standards;
            item.Accessories = itemVM.Accessories;
            item.SealInfo = itemVM.SealInfo;
            item.HoldEquipNo = itemVM.HoldEquipNo;
            item.Note = itemVM.Note;
            item.StatusId = itemVM.StatusId;
            item.AppointmentDate = itemVM.AppointmentDate;

            item.RegistedBy = itemVM.RegistedBy;
            item.ReceivedBy = itemVM.ReceivedBy;
            item.AssignedBy = itemVM.AssignedBy;
            item.TestedBy = itemVM.TestedBy;
            item.VerifiedyBy = itemVM.VerifiedyBy;
            item.ApprovedBy = itemVM.ApprovedBy;
            item.ReturnedBy = itemVM.ReturnedBy;

            item.RegistedDate = itemVM.RegistedDate;
            item.ReceivedDate = itemVM.ReceivedDate;
            item.AssignedDate = itemVM.AssignedDate;
            item.ConfirmedDate = itemVM.ConfirmedDate;
            item.TestedDate = itemVM.TestedDate;
            item.SentToLinkLabDate = item.SentToLinkLabDate;
            item.ReceivedFromLinkLabDate = itemVM.ReceivedFromLinkLabDate;
            item.VerifiedDate = itemVM.VerifiedDate;
            item.ApprovedDate = itemVM.ApprovedDate;
            item.SealedDate = itemVM.SealedDate;
            item.ReturnedDate = itemVM.ReturnedDate;
            item.TestedDate = itemVM.TestedDate;

            item.TestReportId = itemVM.TestReportId;
            item.FeeId = itemVM.FeeId;
            item.Price = itemVM.Price;
        }

        public static void UpdateRegisterRequest(this Request item, RequestVM itemVM)
        {
            item.RegisteredNo = itemVM.RegisteredNo;
            item.ContractNo = itemVM.ContractNo;
            item.CompanyId = itemVM.CompanyId;
            item.EquTypeId = itemVM.EquTypeId;
            item.EquGroupId = itemVM.EquGroupId;
            item.EquName = itemVM.EquName;
            item.Manufacturer = itemVM.Manufacturer;
            item.Model = itemVM.Model;
            item.Serial_Imei = itemVM.Serial_Imei;
            item.MadeIn = itemVM.MadeIn;
            item.Standards = itemVM.Standards;
            item.Accessories = itemVM.Accessories;
            item.SealInfo = itemVM.SealInfo;
            item.HoldEquipNo = itemVM.HoldEquipNo;
            item.Note = itemVM.Note;

            item.RegistedDate = itemVM.RegistedDate;
            item.Price = itemVM.Price;
        }

        public static void UpdateSysConfig(this SysConfig item, SysConfigVM itemVM)
        {
            item.ValueString= itemVM.ValueString;
            item.ValueInt = itemVM.ValueInt;
            item.Description = itemVM.Description;
        }

    }
}