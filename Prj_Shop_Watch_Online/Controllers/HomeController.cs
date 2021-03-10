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
        //public class HttpParamActionAttribute : ActionNameSelectorAttribute
        //{
        //    public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
        //    {
        //        if (actionName.Equals(methodInfo.Name, StringComparison.InvariantCultureIgnoreCase))
        //            return true;

        //        var request = controllerContext.RequestContext.HttpContext.Request;
        //        return request[methodInfo.Name] != null;
        //    }
        //}
        public ActionResult Index(string sortOder, string thuonghieu, string keyword, string gioitinh , decimal? giafrom, decimal? giato,int? page)
        {
            List<Products> products = db.Products.Select(s => s).ToList();

            //sortoder
            ViewBag.CurrentSort = sortOder;
            ViewBag.SapTheoTen = "Sắp theo tên Z-A";
            ViewBag.SapTheoGia = "Giá tăng dần";
            ViewBag.SapTheoGia_DESC ="Giá giảm dần";
            ViewBag.KhuyenMai = "Khuyến mãi";
            //search
            IEnumerable<string> productsex = (from c in db.Products where (!string.IsNullOrEmpty(c.GioiTinh)) select c.GioiTinh).Distinct();                 
            ViewBag.gioitinh = new SelectList(productsex);

            ViewBag.thuonghieu = new SelectList(db.Brands, "TenTH", "TenTH");

            //hiển thị chon
            ViewBag.SexSearch = gioitinh;
            ViewBag.THSearch = thuonghieu;

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
                    //var result = from obj in db.Products join km in db.Promotions on new { key1 = obj.Id, key2 = obj.BrandId }  equals new { key1 = km.ProductId, key2 = km.BrandId }
                    //             select obj;  
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
        //[HttpPost, HttpParamAction]
        //public ActionResult Reset()
        //{
        //    Index("", "", "", "", null, null, null);
        //    return View();
        //}

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