﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data.DataVMs
{
    using Data.DataModels;
    using System;
    using System.Collections.Generic;
    
    public partial class Opinion
    {
        public int ID { get; set; }
        public int RequestID { get; set; }
        public string Detail { get; set; }
        public string RolerID { get; set; }
        public string CreateStaffID { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string Notes { get; set; }
    
        public virtual Request Request { get; set; }
        public virtual Roler Roler { get; set; }
    }
}
