using Data.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataModels
{
    [Table("Licences")]
    public class Licence : Auditable
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string key { get; set; }        
        [DefaultValue("true")]
        public bool enable { get; set; } = true;

        public Licence()
        {
            Id = Guid.NewGuid().ToString();
            enable = true;
        }
    }
}
