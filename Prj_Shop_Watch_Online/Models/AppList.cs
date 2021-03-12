namespace Prj_Shop_Watch_Online.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AppList")]
    public partial class AppList
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Mã ứng dụng không được để trống!")]
        [DisplayName("Mã ứng dụng")]
        public string AppCode { get; set; }

        [Required(ErrorMessage = "Tên ứng dụng không được để trống!")]
        [DisplayName("Tên ứng dụng")]
        public string AppName { get; set; }

        [DisplayName("Api domain")]
        public string ApiDomain { get; set; }

        [DisplayName("Web domain")]
        public string WebDomain { get; set; }

        [DisplayName("Url login")]
        public string AutoLoginUrl { get; set; }

        [DisplayName("Ghi chú")]
        public string Note { get; set; }
    }
}
