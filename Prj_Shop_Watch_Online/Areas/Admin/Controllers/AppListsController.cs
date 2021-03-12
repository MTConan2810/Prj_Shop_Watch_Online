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
    public class AppListsController : Controller
    {
        private SWODBContext db = new SWODBContext();

        // GET: Admin/AppLists
        public async Task<ActionResult> Index()
        {
            return View(await db.AppList.ToListAsync());
        }

        // GET: Admin/AppLists/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppList appList = await db.AppList.FindAsync(id);
            if (appList == null)
            {
                return HttpNotFound();
            }
            return View(appList);
        }

        // GET: Admin/AppLists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AppLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,AppCode,AppName,ApiDomain,WebDomain,AutoLoginUrl,Note")] AppList appList)
        {
            if (ModelState.IsValid)
            {
                db.AppList.Add(appList);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(appList);
        }

        // GET: Admin/AppLists/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppList appList = await db.AppList.FindAsync(id);
            if (appList == null)
            {
                return HttpNotFound();
            }
            return View(appList);
        }

        // POST: Admin/AppLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,AppCode,AppName,ApiDomain,WebDomain,AutoLoginUrl,Note")] AppList appList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appList).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(appList);
        }

        // GET: Admin/AppLists/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppList appList = await db.AppList.FindAsync(id);
            if (appList == null)
            {
                return HttpNotFound();
            }
            return View(appList);
        }

        // POST: Admin/AppLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AppList appList = await db.AppList.FindAsync(id);
            db.AppList.Remove(appList);
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
