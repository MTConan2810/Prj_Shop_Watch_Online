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
    public class BrandsController : Controller
    {
        private SWODBContext db = new SWODBContext();

        // GET: Admin/Brands
        public ActionResult Index(string keyword, int? page, int? pagesize )
        {           
            List<Brands> brands =  db.Brands.Select(s => s).ToList();
            List<int> pagecout = new List<int> { 5, 20, 25, 50 };
            ViewBag.pagesize = new SelectList(pagecout);
            ViewBag.pagesizenow = pagesize;
            if (!string.IsNullOrEmpty(keyword))
            {
                
                brands = brands.Where(obj =>
                                       obj.TenTH.ToUpper().Contains(keyword.ToUpper())
                                       ).Select(s => s).ToList();

            }
            int pageSize = (pagesize ?? 10);
            int pageNumber = (page ?? 1);
            return View(brands.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Brands/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brands brands = await db.Brands.FindAsync(id);
            if (brands == null)
            {
                return HttpNotFound();
            }
            return View(brands);
        }

        // GET: Admin/Brands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Brands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,TenTH,AnhTH,Active,MoTa")] Brands brands)
        {
            if (ModelState.IsValid)
            {
                brands.AnhTH = "";
                var f = Request.Files["ImageFile"];
                if (f != null && f.ContentLength > 0)
                {
                    string FileName = Path.GetFileName(f.FileName);
                    string UpLoadFile = Server.MapPath("~/wwwroot/ImageBrands/") + FileName;
                    f.SaveAs(UpLoadFile);
                    brands.AnhTH = FileName;
                }
                db.Brands.Add(brands);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(brands);
        }

        // GET: Admin/Brands/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brands brands = await db.Brands.FindAsync(id);
            if (brands == null)
            {
                return HttpNotFound();
            }
            return View(brands);
        }

        // POST: Admin/Brands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,TenTH,AnhTH,Active,MoTa")] Brands brands)
        {
            if (ModelState.IsValid)
            {
                var f = Request.Files["ImageFile"];
                if (f != null && f.ContentLength > 0)
                {
                    string FileName = Path.GetFileName(f.FileName);
                    string UpLoadFile = Server.MapPath("~/wwwroot/ImageShop/") + FileName;
                    f.SaveAs(UpLoadFile);
                    brands.AnhTH = FileName;
                }
                db.Entry(brands).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(brands);
        }

        // GET: Admin/Brands/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Brands brands = await db.Brands.FindAsync(id);
        //    if (brands == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(brands);
        //}

        // POST: Admin/Brands/Delete/5        
        public async Task<ActionResult> Delete(int id)
        {
            Brands brands = await db.Brands.FindAsync(id);
            db.Brands.Remove(brands);
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
