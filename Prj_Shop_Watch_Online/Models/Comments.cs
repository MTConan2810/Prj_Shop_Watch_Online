namespace Prj_Shop_Watch_Online.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comments
    {
        public int Id { get; set; }

        public string Tieude { get; set; }

        public string NoiDung { get; set; }

        public DateTime? ThoiGian { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }

        public virtual Products Products { get; set; }

        public virtual Users Users { get; set; }
    }
}
