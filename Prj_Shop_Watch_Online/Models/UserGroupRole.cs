namespace Prj_Shop_Watch_Online.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserGroupRole")]
    public partial class UserGroupRole
    {
        public int Id { get; set; }

        [DisplayName("Tên tài khoản")]
        [Required(ErrorMessage = "Tên tài khoản không được để trống!")]
        public string Username { get; set; }

        [DisplayName("Mã ứng dụng")]
        public string AppCode { get; set; }

        [DisplayName("Mã quyền")]
        public string GroupCode { get; set; }
    }
}
