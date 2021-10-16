using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Data.DataVMs
{
    public class ApplicationUserGroupVM
    {
        [Display(Name = "Mã người dùng")]
        public string UserId { set; get; }

        [Display(Name = "Mã nhóm")]
        [StringLength(36, ErrorMessage = "Mã nhóm không quá 36 ký tự")]
        public string GroupId { set; get; }

        public virtual ApplicationUserVM ApplicationUser { set; get; }

        public virtual ApplicationGroupVM ApplicationGroup { set; get; }
        public ApplicationUserGroupVM()
        {
            UserId = Guid.NewGuid().ToString();
        }
    }
}