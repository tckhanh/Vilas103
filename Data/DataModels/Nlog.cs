using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataModels
{
    [Table("NLogs")]
    public class NLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string MachineName { get; set; }

        public DateTime Logged { get; set; }

        public string Level { get; set; }
        public string Message { get; set; }
        public string Logger { get; set; }
        public string Properties { get; set; }
        public string UserName { get; set; }
        public string Callsite { get; set; }
        public string Thread { get; set; }
        public string Exception { get; set; }
        public string StackTrace { get; set; }
    }
}