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
    
    public partial class IncidentStatus
    {
        public IncidentStatus()
        {
            this.Incidents = new HashSet<Incident>();
        }
    
        public int IncidentStatusID { get; set; }
        public string IncidentStatusName { get; set; }
    
        public virtual ICollection<Incident> Incidents { get; set; }
    }
}
