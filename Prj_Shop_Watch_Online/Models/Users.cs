namespace Prj_Shop_Watch_Online.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            Comments = new HashSet<Comments>();
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Tài khoản không được để trống")]
        [DisplayName("Tên tài khoản")]
        public string Username { get; set; }

        [DisplayName("Mật khẩu"), DataType(DataType.Password)]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Tối thiểu 3 kí tự,Tối đa 20 kí tự")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [DisplayName("Họ và tên")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [DisplayName("Email"),DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Ghi chú")]
        public string Note { get; set; }

        [DisplayName("Trạng thái")]
        public bool Active { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comments> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
