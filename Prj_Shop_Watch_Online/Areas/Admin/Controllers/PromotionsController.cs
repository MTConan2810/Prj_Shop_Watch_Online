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

namespace Prj_Shop_Watch_Online.Areas.Admin.Controllers
{
    public class PromotionsController : Controller
    {
        private SWODBContext db = new SWODBContext();

        // GET: Admin/Promotions
        public async Task<ActionResult> Index()
        {
            return View(await db.Promotions.ToListAsync());
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
            return View(promotions);
        }

        // GET: Admin/Promotions/Delete/5
        public async Task<ActionResult> Delete(int? id)
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

        // POST: Admin/Promotions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
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
