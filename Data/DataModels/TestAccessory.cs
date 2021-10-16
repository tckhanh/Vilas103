﻿//------------------------------------------------------------------------------
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

    public partial class TestAccessory
    {
        public TestAccessory()
        {
            this.HandoverAccs = new HashSet<HandoverAcc>();
        }
        [Key]
        public int AccID { get; set; }
        public Nullable<int> TestSysID { get; set; }
        public string AccNo { get; set; }
        public string AccName { get; set; }
        public string Model { get; set; }
        public string SN { get; set; }
        public string Manufacturer { get; set; }
        public Nullable<int> CountryID { get; set; }
        public Nullable<int> AccTypeID { get; set; }
        public Nullable<int> MadeYear { get; set; }
        public Nullable<System.DateTime> ReceiveDate { get; set; }
        public string History { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<int> Hold { get; set; }
        public Nullable<int> Body { get; set; }
        public Nullable<int> MainAcc { get; set; }
        public Nullable<int> Valid { get; set; }
    
        public virtual AccHoldStatu AccHoldStatu { get; set; }
        public virtual AccType AccType { get; set; }
        public virtual ICollection<HandoverAcc> HandoverAccs { get; set; }
        public virtual TestSystem TestSystem { get; set; }
        public virtual TestSysStatu TestSysStatu { get; set; }
    }
}
