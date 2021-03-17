using Prj_Shop_Watch_Online.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Reflection;
using System.Threading.Tasks;
using System.Net;

namespace Prj_Shop_Watch_Online.Controllers
{
    public class HomeController : Controller
    {
        private SWODBContext db = new SWODBContext();

        public ActionResult Index(string sortOder, string thuonghieu, string keyword, string gioitinh , string mucgia,string loaidh,int? page)
        {
            List<Products> products = db.Products.ToList();
            //Find
            //sortoder
            List<string> listsort = new List<string> { "Sắp theo tên Z-A", "Giá tăng dần", "Giá giảm dần", "Khuyến mãi" };
            ViewBag.sortOder = new SelectList(listsort);                       
            
            //search
            IEnumerable<string> productsex = (from c in db.Products where (!string.IsNullOrEmpty(c.GioiTinh)) select c.GioiTinh).Distinct();                 
            ViewBag.gioitinh = new SelectList(productsex);
            
            ViewBag.thuonghieu = new SelectList(db.Brands, "TenTH", "TenTH");
            
            List<string> listgia = new List<string> { "Đồng giá 499.000đ", "Dưới 2 triệu", "Từ 2 triệu đến 5 triệu", "Trên 5 triệu", "Trên 100 triệu" };
            ViewBag.mucgia = new SelectList(listgia);
            
            IEnumerable<string> productloaidh = (from c in db.Products where (!string.IsNullOrEmpty(c.LoaiDH)) select c.LoaiDH).Distinct();
            ViewBag.loaidh = new SelectList(productloaidh);        
            //hiển thị chon
            ViewBag.SexSearch = gioitinh;
            ViewBag.THSearch = thuonghieu;
            ViewBag.CurrentSort = sortOder;
            ViewBag.currentGia = mucgia;
            ViewBag.currentLoaiDH = loaidh;


            if (!string.IsNullOrEmpty(keyword))
            {
                page = 1;
                products = products.Where(obj =>
                                       obj.TenSp.ToUpper().Contains(keyword.ToUpper())
                                       ).Select(s=>s).ToList();
                
            }  
            if(!string.IsNullOrEmpty(gioitinh))
            {
                products = db.Products.Where(obj =>                                     
                                       obj.GioiTinh.Equals(gioitinh)                                                                      
                                       ).Select(s => s).ToList();
            }
            if (!string.IsNullOrEmpty(thuonghieu))
            {
                products = products.Where(obj =>
                                       obj.Brands.TenTH.Equals(thuonghieu)
                                       ).Select(s => s).ToList();
            }
            //muc gia
            switch(mucgia)
            {
                case "Đồng giá 499.000đ":
                    products = products.Where(obj => obj.Gia == 499000).Select(s => s).ToList();
                    break;
                case "Dưới 2 triệu":
                    products = products.Where(obj => obj.Gia < 2000000).Select(s => s).ToList();
                    break;
                case "Từ 2 triệu đến 5 triệu":
                    products = products.Where(obj => obj.Gia >= 2000000 && obj.Gia <= 5000000).Select(s => s).ToList();
                    break;
                case "Trên 5 triệu":
                    products = products.Where(obj => obj.Gia > 5000000).Select(s => s).ToList();
                    break;
                case "Trên 100 triệu":
                    products = products.Where(obj => obj.Gia > 100000000).Select(s => s).ToList();
                    break;
            }
            if (!string.IsNullOrEmpty(loaidh))
            {
                products = products.Where(obj =>
                                       obj.LoaiDH.Equals(loaidh)
                                       ).Select(s => s).ToList();
            }            
            //sort
            switch (sortOder)
            {
                case "Sắp theo tên Z-A":
                    products = products.OrderByDescending(s => s.TenSp).ToList();
                    break;

                case "Giá tăng dần":
                    products = products.OrderBy(s => s.Gia).ToList();
                    break;

                case "Giá giảm dần":
                    products = products.OrderByDescending(s => s.Gia).ToList();
                    break;
                case "Khuyến mãi":                    
                    var checkkm = from km in db.Promotions where (km.Status == true && km.FromDate <= DateTime.Now && km.ToDate >= DateTime.Now) select km;
                    var result = from obj in products
                                 from km in checkkm.ToList()
                                 where obj.Id == km.ProductId || obj.BrandId == km.BrandId || km.ApplyForAll == true
                                 orderby obj.TenSp
                                 select obj;
                    products = result.ToList();
                    break;             
                default:
                    products = products.OrderBy(s => s.TenSp).ToList();
                    break;
            }
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }

        // GET: Home/Details/5
        public async Task<ActionResult> ProductDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = await db.Products.FindAsync(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        public PartialViewResult _Comments(int id, int? page)
        {
            List<Comments> comments = db.Comments.Where(s => s.ProductId == id).Select(s => s).ToList();
            
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return PartialView(comments.ToPagedList(pageNumber,pageSize));
        }
        //public ActionResult AddComments()
        //{
        //    return View();
        //}
        [HttpPost]
        public async Task<ActionResult> AddComments(string tieude,string noidung,int productId)
        {
            Comments comments = new Comments();
            if (Session["idUser"] != null)
            { 
                comments.Tieude = tieude;
                comments.NoiDung = noidung;
                comments.ProductId = productId;
                comments.ThoiGian = DateTime.Now;
                comments.UserId = (int)Session["idUser"];
                if (ModelState.IsValid)
                {
                    db.Comments.Add(comments);
                    await db.SaveChangesAsync();
                    return RedirectToAction("ProductDetails","Home",new { id = productId});
                }
            }   
            else
            {
                //ViewBag.error = "Bạn phải đăng nhập để bình luận";
                return RedirectToAction("Login","Account");
            }
            return RedirectToAction("ProductDetails", "Home", new { id = productId });
        }

        public PartialViewResult _Nav()
        {
            var brands = db.Brands.Where(b => b.Active == true).Select(s => s).ToList();
            return PartialView(brands);
        }
        private string CartSession = "CartSession";

        [ChildActionOnly]
        public PartialViewResult _CartView()
        {
            var cart = Session[CartSession];
            var list = new List<Cart>();
            if (cart != null)
            {
                list = (List<Cart>)cart;
            }
            return PartialView(list);
        }
        public ActionResult Shop(string address)
        {
            List<Shops> shops = new List<Shops>();
            ViewBag.shopaddress = address;
            if (address == null)
            {
                shops = db.Shops.ToList();
            }
            else
            {
                shops = db.Shops.Where(s => s.Address.Contains(address)).Select(s => s).ToList();
            }
            return View(shops);
        }
    }
}