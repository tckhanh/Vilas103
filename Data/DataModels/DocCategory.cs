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

    public partial class DocCategory: Auditable
    {
        public DocCategory()
        {
            this.DocIns = new HashSet<DocIn>();
            this.DocOuts = new HashSet<DocOut>();
        }
        [Key]
        public int DocCatID { get; set; }
        public string DocCatCode { get; set; }
        public string DocCatName { get; set; }
    
        public virtual ICollection<DocIn> DocIns { get; set; }
        public virtual ICollection<DocOut> DocOuts { get; set; }
    }
}
