//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data.DataVMs
{
    using System;
    using System.Collections.Generic;
    
    public partial class Authorize
    {
        public int AuthorizeID { get; set; }
        public string StaffID { get; set; }
        public int RoleID { get; set; }
        public System.DateTime BeginDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<bool> InActived { get; set; }
    
        public virtual Role Role { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
