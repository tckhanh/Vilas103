using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BTS.Web.Models
{
    public class AuditVM
    {
        [Display(Name = "Mã Audit")]
        public int Id { get; set; }
        [Display(Name = "Mã Session")]
        public string SessionID { get; set; }
        [Display(Name = "Địa chỉ IP")]
        public string IPAddress { get; set; }
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Display(Name = "URL truy cập")]
        public string URLAccessed { get; set; }

        [Display(Name = "Thời điểm truy cập")]
        public DateTime TimeAccessed { get; set; }
        //A new Data property that is going to store JSON string objects that will later be able to
        //be deserialized into objects if necessary to view details about a Request
        [Display(Name = "Chi tiết truy cập")]
        public string Data { get; set; }

        public AuditVM()
        {
        }
    }   
}