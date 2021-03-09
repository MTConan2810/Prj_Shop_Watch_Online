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

        [DisplayName("Địa chỉ")]
        public string Address { get; set; }
        
        [DisplayName("Chi nhánh")]
        public string ChiNhanh { get; set; }
        
        [DisplayName("Điện thoại")]
        public string DienThoai { get; set; }

        [DisplayName("Email")]
        public string Mail { get; set; }

        [DisplayName("Ảnh Showroom")]
        public string ImageShop { get; set; }
    }
}
