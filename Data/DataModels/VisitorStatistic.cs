using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataModels
{
    [Table("VisitorStatistics")]
    public class VisitorStatistic
    {
        [Key]
        public Guid Id { set; get; }

        [Required]
        public DateTime VisitedDate { set; get; }

        public string IPAddress { set; get; }

        public VisitorStatistic()
        {
            Id = Guid.NewGuid();
        }
    }
}