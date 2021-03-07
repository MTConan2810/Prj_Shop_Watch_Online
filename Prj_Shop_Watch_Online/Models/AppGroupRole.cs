namespace Prj_Shop_Watch_Online.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AppGroupRole")]
    public partial class AppGroupRole
    {
        public int Id { get; set; }

        public string AppCode { get; set; }

        public string GroupCode { get; set; }

        public string GroupName { get; set; }

        public string Note { get; set; }
    }
}
