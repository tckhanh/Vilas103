using Data.Extensions;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Data.DataModels
{
    public abstract class Auditable : IAuditable
    {
        public Auditable()
        {
            Actived = true;
            Blocked = true;
            CreatedDate = DateTime.Now;
        }

        [DefaultValue("true")]
        public bool Actived { get; set; }

        [DefaultValue("true")]
        public bool Blocked { get; set; }

        [DefaultDateTimeValue("Now")]
        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        [StringLength(64)]
        public string UpdatedBy { set; get; }
    }
}