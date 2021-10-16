using Data.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataVMs
{
    public class AuditableVM
    {
        public AuditableVM()
        {
            Actived = true;
            Blocked = true;
            CreatedDate = DateTime.Now;
        }

        [DefaultValue("true")]
        [Display(Name = "Kích hoạt")]
        public bool Actived { get; set; }

        [DefaultValue("true")]
        [Display(Name = "Bị khoá")]
        public bool Blocked { get; set; }

        [DefaultDateTimeValue("Now")]
        [Display(Name = "Ngày tạo")]
        [DataType(DataType.Date)]
        public DateTime? CreatedDate { set; get; }

        [StringLength(64)]
        [Display(Name = "Người tạo")]
        public string CreatedBy { set; get; }

        [Display(Name = "Ngày sửa đổi")]
        [DataType(DataType.Date)]
        public DateTime? UpdatedDate { set; get; }

        [StringLength(64)]
        [Display(Name = "Người sửa đổi")]
        public string UpdatedBy { set; get; }

    }
}
