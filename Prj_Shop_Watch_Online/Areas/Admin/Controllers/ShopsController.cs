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
using System.IO;
using PagedList;

namespace Prj_Shop_Watch_Online.Areas.Admin.Controllers
{
    public class ShopsController : Controller
    {
        private SWODBContext db = new SWODBContext();

        // GET: Admin/Shops
        public ActionResult Index(string keyword, int? page, int? pagesize)
        {
            List<Shops> shops = db.Shops.Select(s=>s).ToList();
            List<int> pagecout = new List<int> { 5,20,25,50 };
            ViewBag.pagesize = new SelectList(pagecout);
            ViewBag.pagesizenow = pagesize;
            if (!string.IsNullOrEmpty(keyword))
            {

                shops = shops.Where(obj =>
                                    obj.Address.ToUpper().Contains(keyword.ToUpper())
                                    ).Select(s => s).ToList();

            }
            int pageSize = (pagesize ?? 10);
            int pageNumber = (page ?? 1);
            return View(shops.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Shops/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shops shops = await db.Shops.FindAsync(id);
            if (shops == null)
            {
                return HttpNotFound();
            }
            return View(shops);
        }

        // GET: Admin/Shops/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Shops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Address,ChiNhanh,DienThoai,Mail,ImageShop")] Shops shops)
        {
            if (ModelState.IsValid)
            {
                shops.ImageShop = "";
                var f = Request.Files["ImageFile"];
                if (f != null && f.ContentLength > 0)
                {
                    string FileName = Path.GetFileName(f.FileName);
                    string UpLoadFile = Server.MapPath("~/wwwroot/ImageShop/") + FileName;
                    f.SaveAs(UpLoadFile);
                    shops.ImageShop = FileName;
                }
                db.Shops.Add(shops);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(shops);
        }

        // GET: Admin/Shops/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shops shops = await db.Shops.FindAsync(id);
            if (shops == null)
            {
                return HttpNotFound();
            }
            return View(shops);
        }

        // POST: Admin/Shops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Address,ChiNhanh,DienThoai,Mail,ImageShop")] Shops shops)
        {
            if (ModelState.IsValid)
            {
                var f = Request.Files["ImageFile"];
                if (f != null && f.ContentLength > 0)
                {
                    string FileName = Path.GetFileName(f.FileName);
                    string UpLoadFile = Server.MapPath("~/wwwroot/ImageShop/") + FileName;
                    f.SaveAs(UpLoadFile);
                    shops.ImageShop = FileName;
                }
                db.Entry(shops).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(shops);
        }

        // GET: Admin/Shops/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Shops shops = await db.Shops.FindAsync(id);
        //    if (shops == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(shops);
        //}

        // POST: Admin/Shops/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Shops shops = await db.Shops.FindAsync(id);
            db.Shops.Remove(shops);
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
