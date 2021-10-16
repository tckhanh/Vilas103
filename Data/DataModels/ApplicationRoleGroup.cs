using Data.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataModels
{
    [Table("ApplicationRoleGroups")]
    public class ApplicationRoleGroup : Auditable
    {
        [Key]
        [Column(Order = 1)]
        public string GroupId { set; get; }

        [Column(Order = 2)]
        [Key]
        public string RoleId { set; get; }

        [ForeignKey("RoleId")]
        public virtual ApplicationRole ApplicationRole { set; get; }

        [ForeignKey("GroupId")]
        public virtual ApplicationGroup ApplicationGroup { set; get; }
    }
}