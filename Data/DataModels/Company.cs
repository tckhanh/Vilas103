namespace Data.DataModels
{
    using Data.DataVMs;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Company: Auditable
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        public string PhoneNo { get; set; }

        public string FaxNo { get; set; }

        public string ContactName { get; set; }

        public string ContactPhone { get; set; }

        public string TaxCompanyName { get; set; }

        public string TaxAddress { get; set; }

        public string TaxCode { get; set; }

        public string TaxEmail { get; set; }

        public string Contract { get; set; }

        public string UserName { get; set; }

        public DateTime? RegistedDate { get; set; }
        public DateTime? BlockedDate { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public DateTime? FailLoginDate { get; set; }

        public int? FailLoginTimes { get; set; }
    }
}
