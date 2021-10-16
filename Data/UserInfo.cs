using Data.DataModels;
using Data.Providers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Principal;


namespace Cuc_QLCL.AdapterData
{
    /// <summary>
    /// Lưu thông tin người dùng đang đăng nhập hệ thống
    /// Người dùng admin có full quyền, được kiểm tra tại web.config
    /// </summary>
    /// <Modified>
    /// Name        Date         Comment
    /// giangum     09/04/2009  Thêm mới
    /// </Modified>
    public class UserInfo : IPrincipal, IIdentity
    {
        //public string UserName { get; set; }
        //public string FullName { get; set; }
        //public string Email { get; set; }
        //public string ImagePath { get; set; }
        //public string Roles { get; set; }

        // Đường dẫn đến file Setting
        private string mSettingPath;
        private ApplicationUser _GiamDoc;
        private string mAdminUser = string.Empty;

        private List<string> lstPermissionStartwith = new List<string>();
        private string startWith = string.Empty;

        private List<string> lstPermission = new List<string>();

        //private DmDonVi objDmDonVi;

        private bool _IsNguoiDungCuaVu;

        //private DmTrungTam objDmTrungTam;

        private bool _isThanhTra;

        public bool IsThanhTra
        {
            get { return _isThanhTra; }
            set { _isThanhTra = value; }
        }
        string UserAdmin;

        public string PreviousPage = string.Empty;

        /// <summary>
        /// kiểm tra người dụng có phải là người dùng đăng nhập của vụ
        /// </summary>
        /// <Modified>
        /// Name        Date         Comment
        /// giangum     09/04/2009  Thêm mới
        /// </Modified>
        //public DmTrungTam TrungTam
        //{
        //    get
        //    {
        //        return objDmTrungTam;
        //    }
        //}
        /// <summary>
        /// kiểm tra người dụng có phải là người dùng đăng nhập của vụ
        /// </summary>
        /// <Modified>
        /// Name        Date         Comment
        /// giangum     09/04/2009  Thêm mới
        /// </Modified>

        public bool IsNguoiDungCuaVu
        {
            get
            {
                return _IsNguoiDungCuaVu;
            }
        }
        /// <summary>
        /// kiểm tra người dụng có phải là người dùng là đơn vị
        /// </summary>
        /// <Modified>
        /// Name        Date         Comment
        /// giangum     09/04/2009  Thêm mới
        /// </Modified>
        //public bool isDonVi
        //{
        //    get
        //    {
        //        return (objDmDonVi != null);
        //    }
        //}

        //public DmDonVi DonVi
        //{
        //    get
        //    {
        //        return objDmDonVi;
        //    }
        //}



        public List<string> PermissionStartwith
        {
            get
            {
                return lstPermissionStartwith;
            }
            set { this.lstPermissionStartwith = value; }
        }

        public List<string> Permissions
        {
            get
            {
                return lstPermission;
            }
        }

        /// <summary>
        /// Kiểm tra người dùng có phải là Administrator, config tại file setting hay không
        /// </summary>
        /// <Modified>
        /// Name        Date        Comment
        /// giangum       09/04/2009  Thêm mới
        /// </Modified>
        public bool IsAdministrator
        {
            get
            {
                // Nếu chưa lấy thông tin Username thì trả về false
                return (UserAdmin == UserName);
            }
        }

         public bool IsReceivedBy(int RequestId)
        {
            var item = RequestProvider.GetSingleByCondition(x => x.Id == RequestId && x.ReceivedBy == UserName);
            if (item == null) return false;
            return true;
        }

        public bool IsTestedBy(int RequestId)
        {
            var item = RequestProvider.GetSingleByCondition(x => x.Id == RequestId && x.TestedBy == UserName);
            if (item == null) return false;
            return true;
        }

        public bool IsVerifiedyBy(int RequestId)
        {
            var item = RequestProvider.GetSingleByCondition(x => x.Id == RequestId && x.VerifiedyBy == UserName);
            if (item == null) return false;
            return true;
        }

        

        /// <summary>
        /// Lấy admin user từ file setting
        /// </summary>
        /// <returns>admin user</returns>
        /// <Modified>
        /// Name        Date        Comment
        /// giangum       09/04/2009  Thêm mới
        /// </Modified>
        public string GetAdminUser()
        {
            return System.Configuration.ConfigurationManager.AppSettings["UserAdmin"];
        }

        private string mLocalIP = "Not found";

        public string LocalIP
        {
            get { return mLocalIP; }
            set { mLocalIP = value; }
        }

        private string mOrganizationCode = "";

        public string OrganizationCode
        {
            get { return mOrganizationCode; }
            set { mOrganizationCode = value; }
        }

        public string MaTrungTam
        {
            get { return mOrganizationCode; }
            set { mOrganizationCode = value; }
        }

        /// <summary>
        /// Thông tin giám đốc
        /// </summary>
        /// <Modified>
        /// Name        Date        Comment
        /// giangum       12/12/2008  Thêm mới
        /// </Modified>
        public ApplicationUser GiamDoc
        {
            get { return _GiamDoc; }
            set { _GiamDoc = value; }
        }


        /// <summary>
        /// Lấy tên giám đốc
        /// </summary>
        /// <Modified>
        /// Name        Date        Comment
        /// giangum       12/12/2008  Thêm mới
        /// </Modified>
        public string TenGiamDoc
        {
            get
            {
                return _GiamDoc != null ? _GiamDoc.FullName : string.Empty;
            }

        }

        private string mUserID;
        private string mUserName;
        private string mPreferLanguage;
        private bool _isAuthenticated;
        private string mFullName;

        public string UserID
        {
            get { return mUserID; }
            set { mUserID = value; }
        }

        public string UserName
        {
            get { return mUserName; }
            set { mUserName = value; }
        }

        public string PreferLanguage
        {
            get { return mPreferLanguage; }
            set { mPreferLanguage = value; }
        }

        #region IIdentity
        public string AuthenticationType
        {
            get
            {
                return "Forms";
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return _isAuthenticated;
            }
        }
        /// <summary>
        /// Lấy tên đăng nhập của người dùng
        /// </summary>
        public string Name
        {
            get
            {
                return mUserID;
            }
        }
        /// <summary>
        /// Lấy tên đầy đủ của user
        /// </summary>
        public string FullName
        {
            get
            {
                return mFullName;
            }
        }

        #endregion;

        /// <summary>
        /// Kiểm tra người dùng có vai trò 'Role' hay không
        /// </summary>
        /// <param name="Role"></param>
        /// <returns>true nếu đúng, false nếu sai</returns>
        /// <Modified>
        /// Name        Date        Comment
        /// giangum       12/12/2008  Thêm mới
        /// </Modified>
        public bool IsInRole(string Role)
        {
            foreach (string strPermisstion in lstPermission)
            {
                if (strPermisstion.StartsWith(Role))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="StartWith"></param>
        /// <returns></returns>
        public List<string> GetPermissionList(string StartWith)
        {
            return PermissionStartwith = ProviderFactory.SysPermissionProvider.GetPermissionIDByUserId(this.UserID, StartWith);
        }

        /// <summary>
        /// Kiểm tra người dùng có permission không hay không
        /// </summary>
        /// <param name="Role"></param>
        /// <returns>true nếu đúng, false nếu sai</returns>
        /// <Modified>
        /// Name        Date        Comment
        /// giangum       12/12/2008  Thêm mới
        /// </Modified>
        public bool IsPermission(string strPermission)
        {
            return (lstPermission.IndexOf(strPermission) >= 0);
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
        /// <Modified>
        /// Name        Date        Comment
        /// giangum       12/12/2008  Thêm mới
        /// </Modified>
        public UserInfo(string SettingPath)
        {
            mSettingPath = SettingPath;
        }

        /// <summary>
        /// Phương thức khởi tạo mặc định, truyền đường dẫn đến file setting để đọc thông tin user admin
        /// </summary>
        /// <Modified>
        /// Name        Date        Comment
        /// giangum       12/12/2008  Thêm mới
        /// </Modified>
        public UserInfo()
        {
            mSettingPath = string.Empty;
            UserAdmin = SysConfigProvider.GetSingleById("ADMIN_USER").ValueString;
        }


        public bool Authenticate(string UserName, string Password)
        {
            return true;
        }

        /// <summary>
        /// Phương thức xác nhận người dùng đăng nhập hệ thống internet
        /// </summary>
        /// <Modified>
        /// Name        Date        Comment
        /// giangum       12/12/
        public bool Authenticate(string UserName, string Password, string MaDonVi, bool NguoiCuaCuc)
        {
            //kiểm tra xem người dùng có phải là người dùng của vụ hay không            
            return true;
        }

    }
}
