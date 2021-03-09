using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prj_Shop_Watch_Online.Models
{
    public class OrderDetailsDAO
    {
        private SWODBContext db = null;

        public OrderDetailsDAO()
        {
            db = new SWODBContext();
        }
        public bool Them(OrderDetail orderDetail)
        {
            try
            {
                db.OrderDetail.Add(orderDetail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}