namespace Prj_Shop_Watch_Online.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Brands
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Brands()
        {
            Products = new HashSet<Products>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage ="Tên thương hiệu không được để trống!")]
        [DisplayName("Thương hiệu")]
        public string TenTH { get; set; }

        [DisplayName("Ảnh")]
        public string AnhTH { get; set; }

        [DisplayName("Trạng thái")]
        public bool Active { get; set; }

        [DisplayName("Mô tả")]
        public string MoTa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Products> Products { get; set; }
    }
}
