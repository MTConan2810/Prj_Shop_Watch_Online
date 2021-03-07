namespace Prj_Shop_Watch_Online.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AppFunctionGroupRole")]
    public partial class AppFunctionGroupRole
    {
        public int Id { get; set; }

        public string AppCode { get; set; }

        public string FunctionCode { get; set; }

        public string GroupCode { get; set; }

        public bool ViewRole { get; set; }

        public bool UpdateRole { get; set; }
    }
}
