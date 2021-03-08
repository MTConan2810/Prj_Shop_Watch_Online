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
        public ActionResult AddItem(int quantity, int productId)
        {
            var sp = db.Products.Where(s => s.Id == productId).FirstOrDefault();
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<Cart>)cart;
                if (list.Exists(x => x.Products.Id == sp.Id))
                {
                    foreach (var item in list)
                    {
                        item.quantity += quantity;
                    }
                }
                else
                {
                    var item = new Cart();
                    item.Products = sp;
                    item.quantity = quantity;
                    list.Add(item);
                }
                Session[CartSession] = list;
            }
            else
            {
                var item = new Cart();
                item.Products = sp;
                item.quantity = quantity;
                var list = new List<Cart>();
                list.Add(item);
                //gán vào session
                Session[CartSession] = list;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Home");
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

        public ActionResult PT_PayMent()
        {
            List<string> htttoan = new List<string>
            {
                "Tiền mặt","Thẻ"
            };
            ViewBag.hthuc = htttoan;
            return View();
        }

        // POST: Admin/DonHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult PT_PayMent(string hoten, string diachi, string email, string sdt, string ghichu, string hinhthucpay)
        {
            var cart = Session[CartSession];
            var list = (List<Cart>)cart;

            return Redirect("Success");
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}