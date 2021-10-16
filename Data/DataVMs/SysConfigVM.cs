using Data.DataVMs;
using Data.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataModels
{
    public class SysConfigVM: AuditableVM
    {
        [MaxLength(64, ErrorMessage = "Tên thông số không quá 64 ký tự")]
        [Display(Name = "Tên thông số")]
        [Required(ErrorMessage = "Yêu cầu nhập Tên thông số")]
        [Unique(ErrorMessage = "Tên thông số đã tồn tại rồi !!", TargetModelType = typeof(SysConfig), TargetPropertyName = "Id")]
        public string Id { set; get; }

        [MaxLength(128, ErrorMessage = "Đặc tả Tên thông số không quá 128 ký tự")]
        [Display(Name = "Đặc tả")]
        public string Description { set; get; }

        [Display(Name = "Năm")]
        public int Year { set; get; }

        [Display(Name = "Giá trị (Text)")]
        public string ValueString { set; get; }

        [Display(Name = "Giá trị (số)")]
        public int ValueInt { set; get; }
    }
}