using Data.DataModels;
using Data.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace Data.DataVMs
{
    public class CompanyVM : AuditableVM
    {
        [StringLength(50, ErrorMessage = "tên Công ty viết tắt không quá 50 ký tự")]
        [Required(ErrorMessage = "Yêu cầu nhập tên Công ty viết tắt")]
        [Display(Name = "Tên viết tắt")]
        [Unique(ErrorMessage = "Tên viết tắt đã tồn tại rồi !!", TargetModelType = typeof(Company), TargetPropertyName = "FastName")]
        public string Id { get; set; }

        [StringLength(256)]
        [Display(Name = "Tên Công ty")]
        [Required(ErrorMessage = "Yêu cầu nhập tên Công ty đầy đủ")]
        [Unique(ErrorMessage = "Tên Công ty đã tồn tại rồi !!", TargetModelType = typeof(Company), TargetPropertyName = "Name")]
        public string Name { get; set; }

        [StringLength(256)]
        [Required(ErrorMessage = "Yêu cầu nhập Địa chỉ Công ty")]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [StringLength(50)]
        [Display(Name = "Số điện thoại")]
        public string PhoneNo { get; set; }

        [StringLength(50)]
        [Display(Name = "Số Fax")]
        public string FaxNo { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Yêu cầu nhập tên người liên hệ")]
        [Display(Name = "Người liên hệ")]
        public string ContactName { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Yêu cầu nhập số điện thoại người liên hệ")]
        [Display(Name = "SĐT liên hệ")]
        public string ContactPhone { get; set; }

        [StringLength(256)]
        [Required(ErrorMessage = "Yêu cầu nhập tên Công ty trên hoá đơn")]
        [Display(Name = "Tên Cty trên Hoá đơn")]
        public string TaxCompanyName { get; set; }

        [StringLength(256)]
        [Required(ErrorMessage = "Yêu cầu nhập địa chỉ Công ty trên hoá đơn")]
        [Display(Name = "Địa chỉ trên Hoá đơn")]
        public string TaxAddress { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Yêu cầu nhập Mã số thuế Công ty")]
        [Display(Name = "Mã số thuế")]
        public string TaxCode { get; set; }

        [StringLength(50)]
        [Display(Name = "Email nhận Hoá đơn")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string TaxEmail { get; set; }

        [StringLength(50)]
        [Display(Name = "Hợp đồng (nếu có)")]
        public string Contract { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Yêu cầu nhập tên Đăng nhập")]
        [Display(Name = "Tài khoản")]
        [Unique(ErrorMessage = "Tài khoản đã tồn tại rồi !!", TargetModelType = typeof(Company), TargetPropertyName = "UserName")]
        public string UserName { get; set; }

        [StringLength(50)]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [StringLength(50)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không trùng với Mật khẩu")]
        [Display(Name = "Xác nhận mật khẩu")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Ngày đăng ký")]
        [DataType(DataType.Date)]
        public DateTime? RegistedDate { get; set; }

        [Display(Name = "Ngày bị khoá")]
        [DataType(DataType.Date)]
        public DateTime? BlockedDate { get; set; }

        [Display(Name = "Ngày đăng nhập")]
        [DataType(DataType.Date)]
        public DateTime? LastLoginDate { get; set; }

        [Display(Name = "Ngày Login bị lỗi")]
        [DataType(DataType.Date)]
        public DateTime? FailLoginDate { get; set; }

        [Display(Name = "Số lần Login bị lỗi")]
        public int FailLoginTimes { get; set; }

        public CompanyVM()
        {
            Password = "DefaultPassword";
            ConfirmPassword = "DefaultPassword";
        }
    }
}
