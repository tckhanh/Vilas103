using Data.DataModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataModels
{
    // Must be expressed in terms of our custom Role and other types:
    public class ApplicationUser
    : IdentityUser<string, ApplicationUserLogin,
    ApplicationUserRole, ApplicationUserClaim>, IAuditable
    {
        [Required]
        public string FullName { get; set; }

        public string Address { get; set; }

        public DateTime? BirthDay { get; set; }

        public string FatherLand { get; set; }

        public string Level { get; set; }

        public string EducationalField { get; set; }

        public DateTime? EntryDate { get; set; }

        public DateTime? OfficialDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string WorkingDuration { get; set; }

        public string JobPositions { get; set; }

        public string ImagePath { get; set; }

        [DefaultValue("false")]
        public bool Locked { get; set; } = false;

        public string CityIDsScope { get; set; }

        public string AreasScope { get; set; }

        public virtual ICollection<ApplicationUserGroup> Groups { get; set; }

        public DateTime? CreatedDate
        { get; set; }

        public string CreatedBy
        { get; set; }

        public DateTime? UpdatedDate
        { get; set; }

        public string UpdatedBy
        { get; set; }

        public ApplicationUser()
        {
            Id = Guid.NewGuid().ToString();
            Locked = false;

            Groups = new HashSet<ApplicationUserGroup>();
            // Add any custom User properties/code here
            //Groups = new List<ApplicationUserGroup>();
        }

        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, string> manager)
        //{
        //    var userIdentity =  manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        //    userIdentity.AddClaim(new Claim("ImagePath", ImagePath));
        //    return userIdentity;
        //}

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, string> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            if (this.ImagePath != null)
                userIdentity.AddClaim(new Claim("ImagePath", this.ImagePath));
            return userIdentity;
        }
    }
}