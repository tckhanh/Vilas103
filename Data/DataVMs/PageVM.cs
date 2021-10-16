
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.Web.Models
{
    public class PageVM
    {
        public int Id { set; get; }
        
        public string Name { set; get; }
        public string Alias { set; get; }
        public string Content { set; get; }
        public bool Status { set; get; }
    }
}