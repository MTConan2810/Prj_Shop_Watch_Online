namespace Prj_Shop_Watch_Online.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Products()
        {
            Comments = new HashSet<Comments>();
            OrderDetail = new HashSet<OrderDetail>();
            ProductImage = new HashSet<ProductImage>();
        }

        public int Id { get; set; }

        public string MaSp { get; set; }

        public string TenSp { get; set; }

        public decimal? Gia { get; set; }

        public string GioiTinh { get; set; }

        public string LoaiDH { get; set; }

        public string KieuDH { get; set; }

        public double? DoChiuNuoc { get; set; }

        public string ChucNang { get; set; }

        public string Vo { get; set; }

        public string LoaiDay { get; set; }

        public int? DuongKinh { get; set; }

        public string LoaiMay { get; set; }

        public string MauMat { get; set; }

        public string MatKinh { get; set; }

        public string MoTa { get; set; }

        public int BrandId { get; set; }

        public int? SoLuong { get; set; }

        public virtual Brands Brands { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comments> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductImage> ProductImage { get; set; }
    }
}
