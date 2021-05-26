using Prj_Shop_Watch_Online.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prj_Shop_Watch_Online.Controllers
{
    public class CartController : Controller
    {
        SWODBContext db = new SWODBContext();
        private static string CartSession = "CartSession";
        // GET: Cart
        public ActionResult Index()
        {          
            var cart = Session[CartSession];
            var list = new List<Cart>();
            if (cart != null)
            {
                list = (List<Cart>)cart;
            }
            return View(list);
        }
        public ActionResult AddItem(int productId)
        {
            var product = db.Products
                                   .Where(p => p.Id == productId)
                                    .FirstOrDefault();
            if (product == null)
            {
                return HttpNotFound("Không có sản phẩm");
            }    
            //add cart 
            var cart = Session[CartSession];
            if(cart!= null)
            {
                var list = (List<Cart>)cart;
                var cartItem = list.Find(p => p.Products.Id == productId);
                if (cartItem !=null)
                {
                    cartItem.quantity++;
                }
                else
                {
                    var item = new Cart();
                    item.Products = product;
                    item.quantity = 1;
                    list.Add(item);                  
                }
                Session[CartSession] = list;
                return RedirectToAction("Index", "Home");
                
            }
            else
            {
                var item = new Cart();
                item.Products =product;
                item.quantity = 1;
                var list = new List<Cart>();
                list.Add(item);
                //gán vào session
                Session[CartSession] = list;
                return RedirectToAction("Index");
            }
            
        }

        /*-----Update----*/
     
        [HttpPost]
        public ActionResult UpdateCart(int productId, int quantity)
        {
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = Session[CartSession];
            var list = (List<Cart>)cart;
            var cartItem = list.Find(p => p.Products.Id == productId);
            if (cartItem != null)
            {
                if (quantity > 0)
                {
                    cartItem.quantity = quantity;
                    Session[CartSession] = list;
                }
                else
                {
                    ViewBag.error = "--Lỗi-- số lượng phải lớn hơn 0 và là số";
                }
            }
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return RedirectToAction("Index");
        }

        public ActionResult RemoveCart(int productId)
        {
            var cart = Session[CartSession];
            var list = (List<Cart>)cart;
            var cartItem = list.Find(p => p.Products.Id == productId);
            if (cartItem != null)
            {
                list.Remove(cartItem);
            }
            Session[CartSession] = list;
            return RedirectToAction("Index");
        }

        // POST: Admin/DonHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Check_Out(string shipname, string shipaddress, string shipemail,string shipphonenumber, int PaymentId)
        {
            var cart = Session[CartSession];
            var list = (List<Cart>)cart;
            //create Order
            Orders orders = new Orders();
            orders.OrderDate = DateTime.Now;
            orders.PaymentId = PaymentId;
            orders.ShipName = shipname;
            orders.ShipAddress = shipaddress;
            orders.ShipEmail = shipemail;
            orders.ShipPhoneNumber = shipphonenumber;
            if(Session["idUser"]!=null)
            {
                orders.UserId = (int)Session["idUser"];
            }               
            orders.Status = false;
            var orderId = new OrderDAO().Them(orders);
            //create orderdetails
            try
            {
                var checkkm = from km in db.Promotions where (km.Status == true && km.FromDate <= DateTime.Now && km.ToDate >= DateTime.Now) select km;
                var result = from obj in list
                             from km in checkkm.ToList()
                             where obj.Products.Id == km.ProductId || obj.Products.BrandId == km.BrandId || km.ApplyForAll == true
                             select obj;
                var kmapplyforall = db.Promotions.Where(km => km.ApplyForAll == true).ToList();
                Boolean checkQuyen(int code)
                {
                    Boolean check = false;
                    foreach (var abc in result.ToList())
                    {
                        if (abc.Products.Id == code)
                        {
                            check = true;
                        }
                    }
                    return check;
                }
                var orderDetailsADD = new OrderDetailsDAO();
                foreach (var item in list)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderId = (int)orderId;
                    orderDetail.ProductId = item.Products.Id;
                    orderDetail.Quantity = item.quantity;
                    if(checkQuyen(item.Products.Id)==false)
                    {
                        orderDetail.Price = (decimal)item.Products.Gia;
                    }
                    if(kmapplyforall.Count > 0)
                    {
                        foreach (var km in checkkm)
                        {
                            if (km.ApplyForAll == true)
                            {
                                if (km.DiscountPercent != null)
                                {
                                    orderDetail.Price = (decimal)((item.Products.Gia - (decimal)(item.Products.Gia * km.DiscountPercent / 100)) * item.quantity);
                                }
                                else if (km.DiscountAmount != null)
                                {
                                    orderDetail.Price = (decimal)((item.Products.Gia - (decimal)km.DiscountAmount) * item.quantity);
                                }
                            }                           
                        }
                    }
                    else
                    {
                        foreach (var km in checkkm)
                        {
                            if (km.ProductId == item.Products.Id || km.BrandId == item.Products.BrandId)
                            {
                                if (km.DiscountPercent != null)
                                {
                                    orderDetail.Price = (decimal)((item.Products.Gia - (decimal)(item.Products.Gia * km.DiscountPercent / 100)) * item.quantity);
                                }
                                else if (km.DiscountAmount != null)
                                {
                                    orderDetail.Price = (decimal)((item.Products.Gia - (decimal)km.DiscountAmount) * item.quantity);
                                }
                            }
                        }
                    }
                    
                    orderDetailsADD.Them(orderDetail);
                }
            }
            catch(Exception ex)
            {
                ViewBag.error = "Không thành công" + ex.ToString();
            }

            return RedirectToAction("Sucess");
        }
        public ActionResult Sucess()
        {
            return View();
        }
    }
}