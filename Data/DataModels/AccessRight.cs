//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class AccessRight
    {
        [Key]
        public int AcessRightID { get; set; }
        public string StaffID { get; set; }
        public Nullable<int> A1 { get; set; }
        public Nullable<int> A2 { get; set; }
        public Nullable<int> A3 { get; set; }
        public Nullable<int> A4 { get; set; }
        public Nullable<int> A5 { get; set; }
        public Nullable<int> A6 { get; set; }
        public Nullable<int> A7 { get; set; }
        public Nullable<int> A8 { get; set; }
        public Nullable<int> A9 { get; set; }
        public Nullable<int> A10 { get; set; }
        public Nullable<int> A11 { get; set; }
        public Nullable<int> A12 { get; set; }
        public Nullable<int> B1 { get; set; }
        public Nullable<int> B2 { get; set; }
        public Nullable<int> B3 { get; set; }
        public Nullable<int> B4 { get; set; }
        public Nullable<int> B5 { get; set; }
        public Nullable<int> B6 { get; set; }
        public Nullable<int> C1 { get; set; }
        public Nullable<int> C2 { get; set; }
        public Nullable<int> C3 { get; set; }
        public Nullable<int> C4 { get; set; }
        public Nullable<int> C5 { get; set; }
        public Nullable<int> C6 { get; set; }
        public Nullable<int> C7 { get; set; }
        public Nullable<int> C7b { get; set; }
        public Nullable<int> C8 { get; set; }
        public Nullable<int> O1 { get; set; }
        public Nullable<int> O2 { get; set; }
        public Nullable<int> O3 { get; set; }
    
        public virtual Staff Staff { get; set; }
    }
}
