using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataModels
{
    public class SysConfig: Auditable
    {
        [Key]
        public string Id { set; get; }

        public string Description { set; get; }

        public int Year { set; get; }

        public string ValueString { set; get; }

        public int ValueInt { set; get; }
    }
}