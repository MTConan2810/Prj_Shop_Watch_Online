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
    public class PromotionsController : Controller
    {
        private SWODBContext db = new SWODBContext();

        // GET: Admin/Promotions
        public ActionResult Index(string keyword, int? page, int? pagesize)
        {
            List<Promotions> promotions = db.Promotions.Select(s => s).ToList();
            List<int> pagecout = new List<int> { 5, 20, 25, 50 };
            ViewBag.pagesize = new SelectList(pagecout);
            ViewBag.pagesizenow = pagesize;
            if (!string.IsNullOrEmpty(keyword))
            {

                promotions = promotions.Where(obj =>
                                       obj.Name.ToUpper().Contains(keyword.ToUpper())
                                       ).Select(s => s).ToList();

            }
            int pageSize = (pagesize ?? 10);
            int pageNumber = (page ?? 1);
            return View(promotions.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Promotions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotions promotions = await db.Promotions.FindAsync(id);
            if (promotions == null)
            {
                return HttpNotFound();
            }
            return View(promotions);
        }

        // GET: Admin/Promotions/Create
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "TenTH");
            ViewBag.ProductId = new SelectList(db.Products, "Id", "MaSp");
            return View();
        }

        // POST: Admin/Promotions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FromDate,ToDate,ApplyForAll,DiscountPercent,DiscountAmount,ProductId,BrandId,Status,Name")] Promotions promotions)
        {
            if (ModelState.IsValid)
            {
                db.Promotions.Add(promotions);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "TenTH", promotions.BrandId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "MaSp", promotions.ProductId);
            return View(promotions);
        }

        // GET: Admin/Promotions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotions promotions = await db.Promotions.FindAsync(id);
            if (promotions == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "TenTH", promotions.BrandId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "MaSp", promotions.ProductId);
            return View(promotions);
        }

        // POST: Admin/Promotions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FromDate,ToDate,ApplyForAll,DiscountPercent,DiscountAmount,ProductId,BrandId,Status,Name")] Promotions promotions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(promotions).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "TenTH", promotions.BrandId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "MaSp", promotions.ProductId);
            return View(promotions);
        }

        // GET: Admin/Promotions/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Promotions promotions = await db.Promotions.FindAsync(id);
        //    if (promotions == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(promotions);
        //}

        //// POST: Admin/Promotions/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            Promotions promotions = await db.Promotions.FindAsync(id);
            db.Promotions.Remove(promotions);
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
