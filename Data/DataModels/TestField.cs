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

    public partial class TestField
    {
        public TestField()
        {
            this.TestSystems = new HashSet<TestSystem>();
        }
        [Key]
        public int FieldID { get; set; }
        public string FiledInfo { get; set; }
    
        public virtual ICollection<TestSystem> TestSystems { get; set; }
    }
}
