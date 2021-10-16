
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BTS.Web.Models
{
    public class ErrorVM
    {
        [Display(Name = "Mã nhận dạng thông báo lỗi")]
        public int Id { set; get; }

        [Display(Name = "Thông báo lỗi")]
        public string Message { set; get; }

        [Display(Name = "Mô tả lỗi")]
        public string Description { set; get; }

        [Display(Name = "Tên Conttroler lỗi")]
        [StringLength(50, ErrorMessage = "Tên Conttroler lỗi không quá 50 ký tự")]
        public string Controller { get; set; }

        [Display(Name = "Chi tiết lời gọi chương trình")]
        public string StackTrace { get; set; }

        [Display(Name = "Thời điểm xảy ra lỗi")]
        public DateTime CreatedDate { get; set; }
    }
}