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
    
    public partial class OrderSt
    {
        public int OrderID { get; set; }
        public string StaffID { get; set; }
        public Nullable<int> StID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Year { get; set; }
        public string Note { get; set; }
        public Nullable<int> OrderStID { get; set; }
        public Nullable<int> InActived { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
    
        public virtual OrderStatus OrderStatu { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual Stationery Stationery { get; set; }
    }
}