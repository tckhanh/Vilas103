using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data.DataModels
{
    public class MenuInfo
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuText { get; set; }
        public string MenuUrl { get; set; }
        public string MenuGroup { get; set; }
    }

}