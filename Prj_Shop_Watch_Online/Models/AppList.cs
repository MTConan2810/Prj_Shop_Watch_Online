namespace Prj_Shop_Watch_Online.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AppList")]
    public partial class AppList
    {
        public int Id { get; set; }

        public string AppCode { get; set; }

        public string AppName { get; set; }

        public string ApiDomain { get; set; }

        public string WebDomain { get; set; }

        public string AutoLoginUrl { get; set; }

        public string Note { get; set; }
    }
}
