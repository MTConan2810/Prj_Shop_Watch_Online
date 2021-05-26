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
using System.IO;

namespace Prj_Shop_Watch_Online.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private SWODBContext db = new SWODBContext();

        // GET: Admin/Products
        public ActionResult Index(string keyword,int? brandId, int? page, int? pagesize)
        {
            List<Products> products = db.Products.Select(s => s).ToList();
            List<int> pagecout = new List<int> { 5, 20, 25, 50 };
            ViewBag.pagesize = new SelectList(pagecout);
            ViewBag.pagesizenow = pagesize;
            if (!string.IsNullOrEmpty(keyword))
            {

                products = products.Where(obj =>
                                       obj.MaSp.ToUpper().Contains(keyword.ToUpper())
                                       ).Select(s => s).ToList();

            }
            if (brandId != null)
            {
                products = products.Where(obj =>
                                       obj.BrandId.Equals(brandId)
                                       ).Select(s => s).ToList();
            }    
            int pageSize = (pagesize ?? 10);
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Products/Details/5
        public async Task<ActionResult> Details(int? id)
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

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "TenTH");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,MaSp,TenSp,Gia,GioiTinh,LoaiDH,KieuDH,DoChiuNuoc,ChucNang,Vo,LoaiDay,DuongKinh,LoaiMay,MauMat,MatKinh,MoTa,BrandId,SoLuong")] Products products)
        {
            var procheck = db.Products.Where(a => a.MaSp.Equals(products.MaSp)).ToList();
            if (procheck.Count == 0)
            {
                if (ModelState.IsValid)
                {
                    db.Products.Add(products);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Edit", new { id = products.Id });
                }
            }
            else
            {
                ViewBag.Error = "Sản phẩm đã tồn tại";
                return View(products);
            }
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "TenTH", products.BrandId);
            return View(products);
        }

        // GET: Admin/Products/Edit/5
        public async Task<ActionResult> Edit(int? id)
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
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "TenTH", products.BrandId);
            return View(products);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,MaSp,TenSp,Gia,GioiTinh,LoaiDH,KieuDH,DoChiuNuoc,ChucNang,Vo,LoaiDay,DuongKinh,LoaiMay,MauMat,MatKinh,MoTa,BrandId,SoLuong")] Products products)
        {
            if (ModelState.IsValid)
            {
                db.Entry(products).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "TenTH", products.BrandId);
            return View(products);
        }
        //ImageProduct
        [HttpPost]
        public async Task<ActionResult> AddImage(int productId, string caption, bool isDefault)
        {
            ProductImage productImage = new ProductImage();
            productImage.ProductId = productId;
            productImage.Caption = caption;
            productImage.IsDefault = isDefault;
            productImage.DateCreated = DateTime.Now;
            productImage.ImagePath = "";
            var f = Request.Files["ImageFile"];
            if (f != null && f.ContentLength > 0)
            {
                string FileName = Path.GetFileName(f.FileName);
                string UpLoadFile = Server.MapPath("~/wwwroot/ImageProducts/") + FileName;
                f.SaveAs(UpLoadFile);
                productImage.ImagePath = FileName;
            }
            db.ProductImage.Add(productImage);
            await db.SaveChangesAsync();
            return RedirectToAction("Edit", new { id = productId });
        }
        public async Task<ActionResult> DeleteImage(int id, int productId)
        {
            ProductImage productImage = await db.ProductImage.FindAsync(id);
            db.ProductImage.Remove(productImage);
            await db.SaveChangesAsync();
            return RedirectToAction("Edit", new { id = productId });
        }





        // GET: Admin/Products/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Products products = await db.Products.FindAsync(id);
        //    if (products == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(products);
        //}

        //// POST: Admin/Products/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            Products products = await db.Products.FindAsync(id);
            db.Products.Remove(products);
            foreach (var obj in db.ProductImage)
            {
                if (obj.ProductId == id)
                {
                    db.ProductImage.Remove(obj);
                }
            }
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
