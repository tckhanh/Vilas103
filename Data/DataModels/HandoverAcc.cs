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

    public partial class HandoverAcc
    {
        [Key]
        public int HandAccID { get; set; }
        public Nullable<int> HandoverID { get; set; }
        public Nullable<int> AccID { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> InActived { get; set; }
    
        public virtual Handover Handover { get; set; }
        public virtual TestAccessory TestAccessory { get; set; }
        public virtual TestSysStatu TestSysStatu { get; set; }
    }
}
