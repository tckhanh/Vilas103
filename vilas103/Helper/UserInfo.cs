using Data.Providers;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace vilas103.Helper
{
    public class UserInfoVM
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public string Roles { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserAdmin { get; set; }
        public string mSettingPath { get; set; }
    }
    public class UserInfo : ClaimsIdentity, IPrincipal
    {
        //public string Name { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public string Roles { get; set; }
        //public bool IsAuthenticated { get; set; }
        public string UserAdmin { get; set; }
        public string mSettingPath { get; set; }

        #region IIdentity
        //public string AuthenticationType
        //{
        //    get
        //    {
        //        return "Forms";
        //    }
        //}

        #endregion;

        

        public bool IsInRole(string role)
        {
            return Roles.Split(new char[] { ';' }).Contains(role);
        }

        /// <summary>
        /// Giao diện IIdentity, cho phép xác thức người dùng
        /// </summary>
        public IIdentity Identity
        {
            get
            {
                return (IIdentity)this;
            }
        }

        /// <summary>
        /// Phương thức khởi tạo mặc định, truyền đường dẫn đến file setting để đọc thông tin user admin
        /// </summary>
        public UserInfo(string SettingPath)
        {
            mSettingPath = SettingPath;
        }

        /// <summary>
        /// Phương thức khởi tạo mặc định, truyền đường dẫn đến file setting để đọc thông tin user admin
        /// </summary>
        public UserInfo()
        {
            mSettingPath = string.Empty;
            //UserAdmin = SysConfigProvider.GetSingleById("ADMIN_USER").ValueString;
        }


        public void getLogInUserInfo()
        {
            //return HttpContext.Current.User as ApplicationUser;
            ClaimsIdentity item = (ClaimsIdentity)HttpContext.Current.User.Identity;
            //IsAuthenticated = item.IsAuthenticated;
            if (item.IsAuthenticated)
            {
                UserName = item.Name;
                FullName = item.FindFirst("FullName")?.Value;
                Email = item.FindFirst("Email")?.Value;
                ImagePath = item.FindFirst("ImagePath")?.Value;
                Roles = string.Join(";", item.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList());
            }
        }
        public void _getLogInUserInfo()
        {
            //return HttpContext.Current.User as ApplicationUser;
            ClaimsIdentity item = (ClaimsIdentity)HttpContext.Current.Session["User"];
            if (item != null)
            {
                //IsAuthenticated = true;
                UserName = item.Name;
                FullName = item.FindFirst("FullName")?.Value;
                Email = item.FindFirst("Email")?.Value;
                ImagePath = item.FindFirst("ImagePath")?.Value;
                Roles = string.Join(";", item.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList());
            }
            else
            {
                //IsAuthenticated = false;
            }
        }
    }
}