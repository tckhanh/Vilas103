using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BTS.Web.Models
{
    public class ConfigVM
    {
        [Display(Name = "Mã nhận dạng thông số")]
        public int Id { set; get; }

        [Display(Name = "Mã tên thông số")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Yêu cầu nhập Mã tên thông số")]
        [Column(TypeName = "varchar")]
        [StringLength(50, ErrorMessage = "Mã tên thông số không quá 50 ký tự")]
        public string Code { set; get; }

        [Display(Name = "Mô tả")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Yêu cầu nhập Mô tả thông số")]
        [StringLength(256, ErrorMessage = "Mô tả thông số không quá 256 ký tự")]
        public string Description { set; get; }


        [Display(Name = "Giá trị thông số (Text)")]
        [StringLength(50, ErrorMessage = "Giá trị thông số (Text) không quá 50 ký tự")]
        public string ValueString { set; get; }

        [Display(Name = "Giá trị thông số (Int)")]
        public int? ValueInt { set; get; }
    }
}