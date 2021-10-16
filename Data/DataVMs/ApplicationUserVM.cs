using Data.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Data.DataVMs
{
    public class ApplicationUserVM
    {
        [Display(Name = "Mã người dùng")]
        public string Id { set; get; }

        [Display(Name = "Họ tên người dùng")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Yêu cầu nhập Họ tên người dùng")]
        [StringLength(255, ErrorMessage = "Họ tên người dùng không quá 255 ký tự")]
        public string FullName { set; get; }

        [Display(Name = "Tài khoản đăng nhập")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Yêu cầu nhập Tài khoản đăng nhập")]
        public string UserName { set; get; }

        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        [MinLength(6,ErrorMessage ="Mật khẩu phải có ít nhất 6 ký tự")]
        public string Password { set; get; }

        [Display(Name = "Xác nhận Mật khẩu")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Xác nhận mật khẩu không trùng với Mật khẩu")]
        public string ConfirmPassword { set; get; }

        [Display(Name = "Hộp thư Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Yêu cầu nhập Hộp thư Email")]
        public string Email { set; get; }

        [Display(Name = "Số điện thoại")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Yêu cầu nhập Số điện thoại")]
        public string PhoneNumber { set; get; }

        [Display(Name = "Địa chỉ")]
        [StringLength(255, ErrorMessage = "Địa chỉ không quá 255 ký tự")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDay { set; get; }

        [Display(Name = "Quê quán")]
        [StringLength(50)]
        public string FatherLand { get; set; }

        [Display(Name = "Trình độ chuyên môn")]
        [StringLength(50)]
        public string Level { get; set; }

        [Display(Name = "Chuyên ngành đào tạo")]
        [StringLength(150)]
        public string EducationalField { get; set; }

        [Display(Name = "Ngày vào cơ quan")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EntryDate { get; set; }

        [Display(Name = "Ngày vào rời cơ quan")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Các vị trí công việc đã đảm nhiệm")]
        [StringLength(255)]
        [DataType(DataType.MultilineText)]
        public string JobPositions { get; set; }

        [MaxLength(555)]
        [Display(Name = "Tập tin ảnh")]
        public string ImagePath { get; set; }

        [Display(Name = "Bị khóa")]
        public bool Locked { get; set; } = true;

        public HttpPostedFileBase ImageUpload { get; set; }

        [StringLength(255)]
        public string CityIDsScope { get; set; }

        [StringLength(255)]
        public string AreasScope { get; set; }

        public virtual IEnumerable<ApplicationGroupVM> Groups { get; set; }

        [Display(Name = "Nhóm người dùng")]
        public ICollection<SelectListItem> GroupList { get; set; }

        [Display(Name = "Danh sách các Quyền được cấp")]
        public ICollection<SelectListItem> RoleList { get; set; }

        [Display(Name = "Danh sách các Tỉnh/Thành phố")]
        public ICollection<SelectListItem> CityList { get; set; }

        [Display(Name = "Danh sách các Khu vực")]
        public ICollection<SelectListItem> AreaList { get; set; }

        public ApplicationUserVM()
        {
            Id = Guid.NewGuid().ToString();
            ImagePath = "~/AppFiles/Images/default.png";
            GroupList = new List<SelectListItem>();
            RoleList = new List<SelectListItem>();
            CityList = new List<SelectListItem>();
            AreaList = new List<SelectListItem>();
        }
    }
}