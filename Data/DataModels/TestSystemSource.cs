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
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class TestSystemSource
    {
        public TestSystemSource()
        {
            this.TestSystems = new HashSet<TestSystem>();
        }

        [Key]
        public int SourceID { get; set; }
        public string SourceInfo { get; set; }

        public virtual ICollection<TestSystem> TestSystems { get; set; }
    }
}
