namespace Prj_Shop_Watch_Online.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AppGroupRole")]
    public partial class AppGroupRole
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Mã ứng dụng không được để trống!")]
        [DisplayName("Mã ứng dụng")]
        public string AppCode { get; set; }

        [Required(ErrorMessage = "Mã quyền không được để trống!")]
        [DisplayName("Mã quyền")]
        public string GroupCode { get; set; }

        [Required(ErrorMessage = "Tên quyền không được để trống!")]
        [DisplayName("Tên quyền")]
        public string GroupName { get; set; }

        [DisplayName("Ghi chú")]
        public string Note { get; set; }
    }
}
