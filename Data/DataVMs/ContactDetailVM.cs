using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BTS.Web.Models
{
    public class ContactDetailVM
    {
        public int Id { set; get; }

        [Required(ErrorMessage = "Tên không được trống")]
        public string Name { set; get; }

        [MaxLength(50, ErrorMessage = "Số điện thoại không vượt quá 50 ký tự")]
        public string Phone { set; get; }

        [MaxLength(250, ErrorMessage = "Email không vượt quá 50 ký tự")]
        public string Email { set; get; }

        [MaxLength(250, ErrorMessage = "Website không vượt quá 50 ký tự")]
        public string Website { set; get; }

        [MaxLength(250, ErrorMessage = "Địa chỉ không vượt quá 50 ký tự")]
        public string Address { set; get; }

        public string Other { set; get; }

        [Display(Name = "Vĩ độ")]
        [DisplayFormat(DataFormatString = "{0:n5}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Yêu cầu nhập Vĩ độ")]
        [Range(0, double.MaxValue, ErrorMessage = "Yêu cầu nhập Vĩ độ là số >= 0")]
        public double? Lat { set; get; }

        [Display(Name = "Kinh độ")]
        [DisplayFormat(DataFormatString = "{0:n5}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Yêu cầu nhập Kinh độ")]
        [Range(0, double.MaxValue, ErrorMessage = "Yêu cầu nhập Kinh độ là số >= 0")]
        public double? Lng { set; get; }

        public bool Status { set; get; }
    }
}