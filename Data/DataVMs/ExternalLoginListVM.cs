using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BTS.Web.Models
{
    public class ExternalLoginListVM
    {
        public string ReturnUrl { get; set; }
    }
    public class ExternalLoginConfirmationVM
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}