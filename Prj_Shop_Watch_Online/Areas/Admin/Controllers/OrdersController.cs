using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Prj_Shop_Watch_Online.Models;
using PagedList;

namespace Prj_Shop_Watch_Online.Areas.Admin.Controllers
{
    public class OrdersController : Controller
    {
        private SWODBContext db = new SWODBContext();

        // GET: Admin/Orders
        public ActionResult Index(string keyword, int? page, int? pagesize)
        {
            List<Orders> orders = db.Orders.Select(s => s).ToList();
            List<int> pagecout = new List<int> { 5, 20, 25, 50 };
            ViewBag.pagesize = new SelectList(pagecout);
            ViewBag.pagesizenow = pagesize;
            if (!string.IsNullOrEmpty(keyword))
            {

                orders = orders.Where(obj =>
                                       obj.ShipName.ToUpper().Contains(keyword.ToUpper())
                                       ).Select(s => s).ToList();

            }
            int pageSize = (pagesize ?? 10);
            int pageNumber = (page ?? 1);
            return View(orders.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Orders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = await db.Orders.FindAsync(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // GET: Admin/Orders/Create
        public ActionResult Create()
        {
            ViewBag.PaymentId = new SelectList(db.Payment, "Id", "PayName");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username");
            return View();
        }

        // POST: Admin/Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,OrderDate,UserId,ShipName,ShipAddress,ShipEmail,ShipPhoneNumber,Status,PaymentId")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(orders);
                await db.SaveChangesAsync();
                return RedirectToAction("Edit", new { id = orders.Id});
            }

            ViewBag.PaymentId = new SelectList(db.Payment, "Id", "PayName", orders.PaymentId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", orders.UserId);
            return View(orders);
        }

        // GET: Admin/Orders/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = await db.Orders.FindAsync(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            ViewBag.PaymentId = new SelectList(db.Payment, "Id", "PayName", orders.PaymentId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", orders.UserId);
            ViewBag.productId = new SelectList(db.Products, "Id", "MaSp");
            return View(orders);
        }

        // POST: Admin/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,OrderDate,UserId,ShipName,ShipAddress,ShipEmail,ShipPhoneNumber,Status,PaymentId")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orders).State = EntityState.Modified;
                if(orders.Status == true)
                {
                    foreach (var item in db.OrderDetail)
                    {
                        foreach (var pr in db.Products)
                        {
                            if (item.OrderId == orders.Id)
                            {
                                if (pr.Id == item.ProductId)
                                {
                                    pr.SoLuong -= item.Quantity;
                                }    
                            }
                        }
                    }
                }                     
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PaymentId = new SelectList(db.Payment, "Id", "PayName", orders.PaymentId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", orders.UserId);
            return View(orders);
        }
        [HttpPost]
        public async Task<ActionResult> AddOD(int orderId, int productId, int quantity)
        {
            try
            {
                var takePro = db.Products.Where(s => s.Id == productId).ToList();
                var checkkm = from km in db.Promotions where (km.Status == true && km.FromDate <= DateTime.Now && km.ToDate >= DateTime.Now) select km;
                var result = from obj in takePro
                             from km in checkkm.ToList()
                             where obj.Id == km.ProductId || obj.BrandId == km.BrandId || km.ApplyForAll == true
                             select obj;
                var kmapplyforall = db.Promotions.Where(km => km.ApplyForAll == true).ToList();
                Boolean checkQuyen(int code)
                {
                    Boolean check = false;
                    foreach (var abc in result.ToList())
                    {
                        if (abc.Id == code)
                        {
                            check = true;
                        }
                    }
                    return check;
                }
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.OrderId = orderId;
                orderDetail.ProductId = productId;
                orderDetail.Quantity = quantity;
                
                foreach(var item in takePro)
                {
                    if (checkQuyen(productId) == false)
                    {
                        orderDetail.Price = (decimal)item.Gia;
                    }
                    if (kmapplyforall.Count > 0)
                    {
                        foreach (var km in checkkm)
                        {
                            if (km.ApplyForAll == true)
                            {
                                if (km.DiscountPercent != null)
                                {
                                    orderDetail.Price = (decimal)((item.Gia - (decimal)(item.Gia * km.DiscountPercent / 100)) * quantity);
                                }
                                else if (km.DiscountAmount != null)
                                {
                                    orderDetail.Price = (decimal)((item.Gia - (decimal)km.DiscountAmount) * quantity);
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (var km in checkkm)
                        {
                            if (km.ProductId == productId || km.BrandId == item.BrandId)
                            {
                                if (km.DiscountPercent != null)
                                {
                                    orderDetail.Price = (decimal)((item.Gia - (decimal)(item.Gia * km.DiscountPercent / 100)) * quantity);
                                }
                                else if (km.DiscountAmount != null)
                                {
                                    orderDetail.Price = (decimal)((item.Gia - (decimal)km.DiscountAmount) * quantity);
                                }
                            }
                        }
                    }
                }
                db.OrderDetail.Add(orderDetail);
                await db.SaveChangesAsync();
                return RedirectToAction("Edit", new { id = orderId });
            }
            catch (Exception ex)
            {
                ViewBag.error = "Không thành công" + ex.ToString();
                return RedirectToAction("Edit", new { id = orderId });
            }
        }
        [HttpPost]
        public async Task<ActionResult> EditOD(int quantity, int id)
        {
            OrderDetail orderDetail = await db.OrderDetail.FindAsync(id);
            if(quantity > 0)
            {
                orderDetail.Price = (orderDetail.Price / orderDetail.Quantity) * quantity;
                orderDetail.Quantity = quantity;                              
                await db.SaveChangesAsync();
                return RedirectToAction("Edit", new { id = orderDetail.OrderId });
            }
            else
            {
                return RedirectToAction("DeleteOD", new { id = id, orderId = orderDetail.OrderId });
            }    
        }




        // GET: Admin/Orders/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Orders orders = await db.Orders.FindAsync(id);
        //    if (orders == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(orders);
        //}

        //// POST: Admin/Orders/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            Orders orders = await db.Orders.FindAsync(id);
            db.Orders.Remove(orders);
            foreach(var obj in db.OrderDetail)
            {
                if(obj.OrderId == id)
                {
                    db.OrderDetail.Remove(obj);
                }    
            }    
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> DeleteOD(int id, int orderId)
        {
            OrderDetail orderDetail = await db.OrderDetail.FindAsync(id);
            db.OrderDetail.Remove(orderDetail);           
            await db.SaveChangesAsync();
            return RedirectToAction("Edit", new { id = orderId });
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
