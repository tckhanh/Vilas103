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

    public partial class Incident
    {
        [Key]
        public int IncidentID { get; set; }
        public Nullable<int> AccID { get; set; }
        public string IncidentName { get; set; }
        public string Detail { get; set; }
        public string Solve { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<int> EffectToSysID { get; set; }
        public Nullable<System.DateTime> SolveDate { get; set; }
        public Nullable<System.DateTime> IncidentDate { get; set; }
        public Nullable<System.DateTime> InitDate { get; set; }
    }
}
