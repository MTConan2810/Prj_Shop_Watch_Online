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
    public class AppGroupRolesController : Controller
    {
        private SWODBContext db = new SWODBContext();

        // GET: Admin/AppGroupRoles
        public async Task<ActionResult> Index()
        {
            return View(await db.AppGroupRole.ToListAsync());
        }

        // GET: Admin/AppGroupRoles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppGroupRole appGroupRole = await db.AppGroupRole.FindAsync(id);
            if (appGroupRole == null)
            {
                return HttpNotFound();
            }
            return View(appGroupRole);
        }

        // GET: Admin/AppGroupRoles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AppGroupRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,AppCode,GroupCode,GroupName,Note")] AppGroupRole appGroupRole)
        {
            if (ModelState.IsValid)
            {
                db.AppGroupRole.Add(appGroupRole);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(appGroupRole);
        }

        // GET: Admin/AppGroupRoles/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppGroupRole appGroupRole = await db.AppGroupRole.FindAsync(id);
            if (appGroupRole == null)
            {
                return HttpNotFound();
            }
            return View(appGroupRole);
        }

        // POST: Admin/AppGroupRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,AppCode,GroupCode,GroupName,Note")] AppGroupRole appGroupRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appGroupRole).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(appGroupRole);
        }

        // GET: Admin/AppGroupRoles/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppGroupRole appGroupRole = await db.AppGroupRole.FindAsync(id);
            if (appGroupRole == null)
            {
                return HttpNotFound();
            }
            return View(appGroupRole);
        }

        // POST: Admin/AppGroupRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AppGroupRole appGroupRole = await db.AppGroupRole.FindAsync(id);
            db.AppGroupRole.Remove(appGroupRole);
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
