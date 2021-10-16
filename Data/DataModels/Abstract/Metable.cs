using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataModels
{
    public class Metable : IMetable
    {
        [StringLength(256)]
        public string MetaKeyword { set; get; }

        [StringLength(256)]
        public string MetaDescription { set; get; }

        public bool Status { set; get; }
    }
}