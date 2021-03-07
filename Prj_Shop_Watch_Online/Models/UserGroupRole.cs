namespace Prj_Shop_Watch_Online.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserGroupRole")]
    public partial class UserGroupRole
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string AppCode { get; set; }

        public string GroupCode { get; set; }
    }
}
