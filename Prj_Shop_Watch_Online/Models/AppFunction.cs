namespace Prj_Shop_Watch_Online.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AppFunction")]
    public partial class AppFunction
    {
        public int Id { get; set; }

        [DisplayName("Mã ứng dụng")]
        public string AppCode { get; set; }

        [DisplayName("Mã chức năng")]
        public string FunctionCode { get; set; }

        [DisplayName("Tên chức năng")]
        public string FunctionName { get; set; }

        [DisplayName("Trực tiếp")]
        public bool IsDirect { get; set; }

        [DisplayName("Url")]
        public string Url { get; set; }
        
        [DisplayName("Có vai trò cập nhật")]
        public bool HasUpdateRole { get; set; }

        [DisplayName("Ghi chú")]
        public string Note { get; set; }
    }
}
