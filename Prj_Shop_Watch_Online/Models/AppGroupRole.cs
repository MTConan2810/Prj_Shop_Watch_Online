namespace Prj_Shop_Watch_Online.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AppGroupRole")]
    public partial class AppGroupRole
    {
        public int Id { get; set; }

        [DisplayName("Mã ứng dụng")]
        public string AppCode { get; set; }

        [DisplayName("Mã quyền")]
        public string GroupCode { get; set; }
        [DisplayName("Tên quyền")]
        public string GroupName { get; set; }
        [DisplayName("Ghi chú")]
        public string Note { get; set; }
    }
}
