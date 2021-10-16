using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BTS.Web.Models
{

    public class LoginVM
    {
        [Required(ErrorMessage = "Bạn cần nhập tài khoản")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "ReturnUrl")]
        public string ReturnUrl { get; set; }


        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}