
using Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BTS.Web.Models
{
    public class LicenceVM
    {
        [MaxLength(36)]
        public string Id { get; set; }

        [Display(Name = "số bản quyền")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Yêu cầu nhập Số bản quyền")]
        public string key { get; set; }

        [Display(Name = "Mã máy chủ")]
        public string machineCode { get; set; }

        [DefaultValue("true")]
        [Display(Name = "Kích hoạt")]
        public bool enable { get; set; } = true;

        [DefaultValue("true")]
        [Display(Name = "Hiệu lực")]
        public bool isValid { get; set; } = true;

        [Display(Name = "Ngày tạo")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Ngày hết hạn")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExpireDate { get; set; }

        [DefaultValue("true")]
        [Display(Name = "Hết hạn")]
        public bool isExpired { get; set; } = true;

        [Display(Name = "Số ngày bản quyền")]
        public int TimeSet { get; set; }

        [Display(Name = "Số ngày còn lại")]
        public int DaysLeft { get; set; }

        public LicenceVM()
        {
            Id = new Guid().ToString();
            machineCode = checkLicence.machineCode;
        }
    }
}