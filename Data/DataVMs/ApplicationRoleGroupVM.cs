using BTS.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Data.DataVMs
{
    public class ApplicationRoleGroupViewModel
    {
        [Display(Name = "Mã nhóm")]
        [StringLength(36, ErrorMessage = "Mã nhóm không quá 36 ký tự")]
        public string GroupId { set; get; }

        [Display(Name = "Mã quyền")]
        public string RoleId { set; get; }

        public virtual ApplicationRoleVM ApplicationRole { set; get; }

        public virtual ApplicationGroupVM ApplicationGroup { set; get; }
        public ApplicationRoleGroupViewModel()
        {
            GroupId = Guid.NewGuid().ToString();
        }
    }
}