namespace Prj_Shop_Watch_Online.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Promotions
    {
        public int Id { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public bool ApplyForAll { get; set; }

        public int? DiscountPercent { get; set; }

        public decimal? DiscountAmount { get; set; }

        public int? ProductId { get; set; }

        public int? BrandId { get; set; }

        public bool Status { get; set; }

        public string Name { get; set; }
    }
}
