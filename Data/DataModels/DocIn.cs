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

    public partial class DocIn: Auditable
    {
        [Key]
        public int DocID { get; set; }
        public Nullable<int> DocCatID { get; set; }
        public Nullable<int> SeriNo { get; set; }
        public string DocNo { get; set; }
        public Nullable<System.DateTime> DocDate { get; set; }
        public Nullable<int> OfficeID { get; set; }
        public string About { get; set; }
        public Nullable<System.DateTime> InDate { get; set; }
        public string FileName { get; set; }
        public string URL { get; set; }
    
        public virtual DocCategory DocCategories { get; set; }
        public virtual Office Offices { get; set; }
    }
}