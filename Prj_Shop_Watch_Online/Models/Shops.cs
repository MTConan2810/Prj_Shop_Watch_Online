namespace Prj_Shop_Watch_Online.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Shops
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public string ChiNhanh { get; set; }

        public string DienThoai { get; set; }

        public string Mail { get; set; }

        public string ImageShop { get; set; }
    }
}
