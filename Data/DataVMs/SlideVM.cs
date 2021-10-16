
using BTS.Web.Infrastructure.Extensions;
using Data.DataModels;
using Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.Web.Models
{
    public class SlideVM
    {
        public int Id { set; get; }

        [Unique(ErrorMessage = "Tên Slide đã tồn tại rồi !!", TargetModelType = typeof(Slide), TargetPropertyName = "Name")]
        public string Name { set; get; }

        public string Description { set; get; }

        public string Image { set; get; }

        public string Url { set; get; }

        public int? DisplayOrder { set; get; }

        public bool Status { set; get; }

        public string Content { set; get; }
    }
}