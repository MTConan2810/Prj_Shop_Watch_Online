namespace Prj_Shop_Watch_Online.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Shops
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Địa chỉ không được để trống")]
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Chi nhánh không được để trống")]
        [DisplayName("Chi nhánh")]
        public string ChiNhanh { get; set; }

        [Required(ErrorMessage = "Điện thoại không được để trống")]
        [DisplayName("Điện thoại"),DataType(DataType.PhoneNumber)]
        public string DienThoai { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [DisplayName("Email"),DataType(DataType.EmailAddress)]
        public string Mail { get; set; }

        [DisplayName("Ảnh Showroom")]
        public string ImageShop { get; set; }
    }
}
