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
    
    public partial class DocumentVM:AuditableVM
    {
        public DocumentVM()
        {
            this.DocIssues = new HashSet<DocIssueVM>();
        }
    
        public int DocID { get; set; }
        public string DocCode { get; set; }
        public Nullable<int> DocTypeID { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        
    
        public virtual ICollection<DocIssueVM> DocIssues { get; set; }
        public virtual DocTypeVM DocTypes { get; set; }
    }
}