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
    using System;
    using System.Collections.Generic;
    
    public partial class EffectToSy
    {
        public EffectToSy()
        {
            this.Incidents = new HashSet<Incident>();
        }
    
        public int EffectID { get; set; }
        public string EffectName { get; set; }
    
        public virtual ICollection<Incident> Incidents { get; set; }
    }
}
