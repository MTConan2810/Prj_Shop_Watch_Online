namespace Prj_Shop_Watch_Online.Models
{
    using System;
    using System.Collections.Generic;
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

        public DateTime? OrderDate { get; set; }

        public int? UserId { get; set; }

        public string ShipName { get; set; }

        public string ShipAddress { get; set; }

        public string ShipEmail { get; set; }

        public string ShipPhoneNumber { get; set; }

        public bool Status { get; set; }

        public int PaymentId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }

        public virtual Payment Payment { get; set; }

        public virtual Users Users { get; set; }
    }
}
