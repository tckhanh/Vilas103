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

    public partial class AccType
    {
        public AccType()
        {
            this.TestAccessories = new HashSet<TestAccessory>();
        }
        [Key]
        public int AccTypeID { get; set; }
        public string AccTypeName { get; set; }
    
        public virtual ICollection<TestAccessory> TestAccessories { get; set; }
    }
}
