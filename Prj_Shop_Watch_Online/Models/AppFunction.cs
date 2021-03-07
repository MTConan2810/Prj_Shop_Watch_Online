namespace Prj_Shop_Watch_Online.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AppFunction")]
    public partial class AppFunction
    {
        public int Id { get; set; }

        public string AppCode { get; set; }

        public string FunctionCode { get; set; }

        public string FunctionName { get; set; }

        public bool IsDirect { get; set; }

        public string Url { get; set; }

        public bool HasUpdateRole { get; set; }

        public string Note { get; set; }
    }
}
