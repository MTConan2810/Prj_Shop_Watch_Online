namespace Prj_Shop_Watch_Online.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductImage")]
    public partial class ProductImage
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        [DisplayName("Ảnh")]
        public string ImagePath { get; set; }

        [DisplayName("chú thích")]
        public string Caption { get; set; }

        [DisplayName("Mặc định")]
        public bool IsDefault { get; set; }

        [DisplayName("Ngày tạo"), DataType(DataType.Date)]
        public DateTime? DateCreated { get; set; }

        [DisplayName("Thứ tự")]
        public int? SortOrder { get; set; }

        [DisplayName("dung lượng")]
        public long? FileSize { get; set; }

        public virtual Products Products { get; set; }
    }
}
