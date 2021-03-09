using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prj_Shop_Watch_Online.Models
{
    public class OrderDAO
    {
        private SWODBContext db = null;

        public OrderDAO()
        {
            db = new SWODBContext();
        }

        public long Them(Orders orders)
        {
            db.Orders.Add(orders);
            db.SaveChanges();
            return orders.Id;
        }
    }
}