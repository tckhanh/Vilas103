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

    public partial class HandoverStatus
    {
        public HandoverStatus()
        {
            this.Handovers = new HashSet<Handover>();
        }
        [Key]
        public int HandStatusID { get; set; }
        public string HandStatusName { get; set; }
    
        public virtual ICollection<Handover> Handovers { get; set; }
    }
}
