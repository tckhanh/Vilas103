using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataModels
{
    internal interface IMetable
    {
        string MetaKeyword { set; get; }
        string MetaDescription { set; get; }
        [DefaultValue("false")]
        bool Status { set; get; }
    }
}