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
    public class ShopsController : Controller
    {
        private SWODBContext db = new SWODBContext();

        // GET: Admin/Shops
        public async Task<ActionResult> Index()
        {
            return View(await db.Shops.ToListAsync());
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
                db.Entry(shops).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(shops);
        }

        // GET: Admin/Shops/Delete/5
        public async Task<ActionResult> Delete(int? id)
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

        // POST: Admin/Shops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
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
