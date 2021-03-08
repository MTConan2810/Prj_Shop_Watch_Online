namespace Prj_Shop_Watch_Online.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comments
    {
        public int Id { get; set; }

        [DisplayName("Tiêu đề")]
        public string Tieude { get; set; }

        [DisplayName("Nội dung")]
        public string NoiDung { get; set; }

        [DisplayName("Thời gian bình luận")]
        public DateTime? ThoiGian { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }

        public virtual Products Products { get; set; }

        public virtual Users Users { get; set; }
    }
}
