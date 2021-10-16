﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data.DataVMs
{
    using Data.DataModels;
    using Data.Extensions;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class EquTypeVM: AuditableVM
    {

        [MaxLength(16, ErrorMessage = "Mã loại sản phẩm không quá 16 ký tự")]
        [Display(Name = "Mã loại SP")]
        [Required(ErrorMessage = "Yêu cầu nhập mã loại sản phẩm")]
        [Unique(ErrorMessage = "Mã loại sản phẩm đã tồn tại rồi !!", TargetModelType = typeof(EquType), TargetPropertyName = "Id")]
        public string Id { get; set; }

        [MaxLength(256, ErrorMessage = "Tên loại sản phẩm không quá 256 ký tự")]
        [Display(Name = "Tên loại SP")]
        [Required(ErrorMessage = "Yêu cầu nhập tên loại sản phẩm")]
        [Unique(ErrorMessage = "Tên loại sản phẩm đã tồn tại rồi !!", TargetModelType = typeof(EquType), TargetPropertyName = "Name")]
        public string Name { get; set; }

        [MaxLength(256, ErrorMessage = "Tên hiển thị loại sản phẩm không quá 256 ký tự")]
        [Display(Name = "Tên hiển thị loại SP")]
        [Required(ErrorMessage = "Yêu cầu nhập tên loại sản phẩm")]
        public string DisplayName { get; set; }

        //[MaxLength(16, ErrorMessage = "Mã loại sản phẩm không quá 16 ký tự")]
        //[Display(Name = "Mã loại SP")]
        //[Required(ErrorMessage = "Yêu cầu nhập mã loại sản phẩm")]
        //public string GroupID { get; set; }

        [Display(Name = "Tiêu chuẩn đo")]
        [Required(ErrorMessage = "Yêu cầu nhập Các Tiêu chuẩn đo")]
        public string Standards { get; set; }

        [Display(Name = "Đơn giá đo")]
        public long Price { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        //public virtual EquGroupVM EquGroup { get; set; }
        //public virtual ICollection<RequestVM> Requests { get; set; }
    }
}
