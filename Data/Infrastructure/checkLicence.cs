
using BTS.Web.Models;
using Data.Common;
using Data.DataModels;
using SKGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;

namespace Data.Infrastructure
{
    public static class checkLicence
    {
        static SerialKeyConfiguration skc = new SerialKeyConfiguration();
        static Generate generate = new Generate(skc);
        static Validate validate = new Validate(skc);
        public static string machineCode = validate.MachineCode.ToString();

        public static string getConfigBtsLicence()
        {
            CFContext DbContext = new CFContext();
            return DbContext.SysConfigs.SingleOrDefault(x => x.Id == CommonConstants.BtsLicenceName)?.ValueString;
        }

        public static IEnumerable<Licence> getLicenses()
        {
            CFContext DbContext = new CFContext();
            return DbContext.Licences.Where(x => x.enable == true);
        }

        public static bool isValid()
        {
            if ((WebConfigurationManager.AppSettings[CommonConstants.BtsLicenceName] == CommonConstants.BtsLicenceValue) || (getConfigBtsLicence() == CommonConstants.BtsLicenceValue))
            {
                return true;
            }
            else
            {
                IEnumerable<Licence> Licences = getLicenses();
                foreach (var Licence in Licences)
                {
                    LicenceVM lastLicenceVM = GetLicenceInfo(Licence);
                    if (lastLicenceVM.isValid)
                        return !lastLicenceVM.isExpired;
                }
                return false;
            }
        }

        public static LicenceVM GetLicenceInfo(Licence licence)
        {
            validate.secretPhase = CommonConstants.SECRET_PHASE;
            validate.Key = licence.key;

            LicenceVM LicenceInfo = new LicenceVM();
            LicenceInfo.Id = licence.Id;
            LicenceInfo.enable = licence.enable;
            LicenceInfo.machineCode = machineCode;
            LicenceInfo.key = licence.key;
            LicenceInfo.isValid = validate.IsValid;
            if (LicenceInfo.isValid)
            {
                LicenceInfo.CreationDate = validate.CreationDate;
                LicenceInfo.ExpireDate = validate.ExpireDate;
                if (LicenceInfo.ExpireDate < DateTime.Now)
                    LicenceInfo.isExpired = true;
                else
                    LicenceInfo.isExpired = false;
                LicenceInfo.TimeSet = validate.SetTime;
                LicenceInfo.DaysLeft = validate.DaysLeft;
            }
            return LicenceInfo;
        }
    }
}