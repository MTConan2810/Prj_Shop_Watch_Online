namespace Prj_Shop_Watch_Online.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Orders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orders()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }

        [DisplayName("Ngày đặt hành")]
        [DataType(DataType.Date)]
        public DateTime? OrderDate { get; set; }

        public int? UserId { get; set; }

        [DisplayName("Tên người nhận")]
        public string ShipName { get; set; }

        [DisplayName("Địa chỉ nhận")]
        public string ShipAddress { get; set; }

        [DisplayName("Email người nhận")]
        public string ShipEmail { get; set; }

        [DisplayName("SĐT người nhận"),DataType(DataType.PhoneNumber)]
        public string ShipPhoneNumber { get; set; }

        [DisplayName("Xác nhận hoá đơn")]
        public bool Status { get; set; }

        public int PaymentId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }

        public virtual Payment Payment { get; set; }

        public virtual Users Users { get; set; }
    }
}
