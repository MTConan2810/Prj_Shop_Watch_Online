namespace Prj_Shop_Watch_Online.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Promotions
    {
        public int Id { get; set; }

        [DisplayName("Từ ngày")]
        public DateTime FromDate { get; set; }

        [DisplayName("Đến ngày")]
        public DateTime ToDate { get; set; }

        [DisplayName("Áp dụng cho tất cả")]
        public bool ApplyForAll { get; set; }

        [DisplayName("Giảm theo %")]
        public int? DiscountPercent { get; set; }

        [DisplayName("Giảm theo tiền")]
        public decimal? DiscountAmount { get; set; }

        public int? ProductId { get; set; }

        public int? BrandId { get; set; }

        [DisplayName("Trạng thái")]
        public bool Status { get; set; }

        [DisplayName("Tên khuyến mãi")]
        public string Name { get; set; }
    }
}
