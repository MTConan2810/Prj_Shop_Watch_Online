using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prj_Shop_Watch_Online.Models
{
    public class Cart
    {
        public int quantity { get; set; }
        public virtual Products Products { get; set; }
    }
}