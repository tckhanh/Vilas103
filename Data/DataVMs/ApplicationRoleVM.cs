using Data.DataModels;
using Data.DataVMs;
using Data.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTS.Web.Models
{
    public class ApplicationRoleVM
    {
        [Display(Name = "Mã Quyền")]
        public string Id { set; get; }

        [Display(Name = "Tên Quyền")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Yêu cầu nhập Tên Quyền")]
        [Unique(ErrorMessage = "Tên Quyền đã tồn tại rồi !!", TargetModelType = typeof(ApplicationRole), TargetPropertyName = "Name")]
        public string Name { set; get; }

        [Display(Name = "Mô tả quyền")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Yêu cầu nhập Mô tả quyền")]
        [MaxLength(250, ErrorMessage = "Mô tả quyền không quá 250 ký tự")]
        public string Description { set; get; }

        [Display(Name = "Danh sách các nhóm được cấp quyền")]
        public ICollection<SelectListItem> GroupList { get; set; }

        [Display(Name = "Danh sách các người dùng được cấp quyền")]
        public ICollection<SelectListItem> UserList { get; set; }

        public ApplicationRoleVM()
        {
            Id = Guid.NewGuid().ToString();
            GroupList = new List<SelectListItem>();
            UserList = new List<SelectListItem>();
        }
    }
}