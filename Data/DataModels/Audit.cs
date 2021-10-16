using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataModels
{
    public class Audit
    {
        //A new SessionId that will be used to link an entire users "Session" of Audit Logs
        //together to help identifer patterns involving erratic behavior
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string SessionID { get; set; }
        public string IPAddress { get; set; }
        public string UserName { get; set; }
        public string URLAccessed { get; set; }
        public DateTime TimeAccessed { get; set; }
        //A new Data property that is going to store JSON string objects that will later be able to
        //be deserialized into objects if necessary to view details about a Request
        public string Data { get; set; }

        public Audit()
        {
        }
    }
}
