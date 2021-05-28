namespace Prj_Shop_Watch_Online.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
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

        [Required(ErrorMessage = "Mã sản phẩm không được để trống")]
        [DisplayName("Mã Sản phẩm")]
        public string MaSp { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [DisplayName("Tên sản phẩm")]
        public string TenSp { get; set; }

        [Required(ErrorMessage = "Giá không được để trống")]
        [DisplayName("Giá")]
        [DisplayFormat(DataFormatString = "{0:#.###}",ApplyFormatInEditMode = true)]
        public decimal? Gia { get; set; }

        [DisplayName("Giới tính")]
        public string GioiTinh { get; set; }

        [DisplayName("Loại đồng hồ")]
        public string LoaiDH { get; set; }

        [DisplayName("Kiểu đồng hồ")]
        public string KieuDH { get; set; }

        [DisplayName("Độ chịu nước")]
        public double? DoChiuNuoc { get; set; }

        [DisplayName("Chức năng")]
        public string ChucNang { get; set; }

        [DisplayName("Vỏ")]
        public string Vo { get; set; }

        [DisplayName("Loại dây")]
        public string LoaiDay { get; set; }

        [DisplayName("Đường kính")]
        public int? DuongKinh { get; set; }

        [DisplayName("Loại máy")]
        public string LoaiMay { get; set; }

        [DisplayName("Màu mắt")]
        public string MauMat { get; set; }

        [DisplayName("Mắt kính")]
        public string MatKinh { get; set; }

        [DisplayName("Mô tả")]
        public string MoTa { get; set; }

        public int BrandId { get; set; }

        [Required(ErrorMessage = "Số lượng không được để trống")]
        [DisplayName("Số lượng")]
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
