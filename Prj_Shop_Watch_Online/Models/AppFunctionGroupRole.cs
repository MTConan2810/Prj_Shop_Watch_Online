namespace Prj_Shop_Watch_Online.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AppFunctionGroupRole")]
    public partial class AppFunctionGroupRole
    {
        public int Id { get; set; }

        [DisplayName("Mã ứng dụng")]
        public string AppCode { get; set; }

        [DisplayName("Mã chức năng")]
        public string FunctionCode { get; set; }

        [DisplayName("Mã quyền")]
        public string GroupCode { get; set; }

        [DisplayName("Quyền xem")]
        public bool ViewRole { get; set; }

        [DisplayName("Quyền cập nhật")]
        public bool UpdateRole { get; set; }
    }
}
