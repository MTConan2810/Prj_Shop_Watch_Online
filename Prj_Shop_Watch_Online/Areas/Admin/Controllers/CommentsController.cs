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
    public class CommentsController : Controller
    {
        private SWODBContext db = new SWODBContext();

        // GET: Admin/Comments
        public ActionResult Index(string keyword, int? page, int? pagesize)
        {
            List<Comments> comments = db.Comments.Select(s => s).ToList();
            List<int> pagecout = new List<int> { 5, 20, 25, 50 };
            ViewBag.pagesize = new SelectList(pagecout);
            ViewBag.pagesizenow = pagesize;
            if (!string.IsNullOrEmpty(keyword))
            {

                comments = comments.Where(obj =>
                                       obj.Users.FullName.ToUpper().Contains(keyword.ToUpper())
                                       ).Select(s => s).ToList();

            }
            int pageSize = (pagesize ?? 10);
            int pageNumber = (page ?? 1);
            return View(comments.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Comments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = await db.Comments.FindAsync(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            return View(comments);
        }

        // GET: Admin/Comments/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "Id", "MaSp");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username");
            return View();
        }

        // POST: Admin/Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Tieude,NoiDung,ThoiGian,UserId,ProductId")] Comments comments)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Add(comments);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "Id", "MaSp", comments.ProductId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", comments.UserId);
            return View(comments);
        }

        // GET: Admin/Comments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = await db.Comments.FindAsync(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "MaSp", comments.ProductId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", comments.UserId);
            return View(comments);
        }

        // POST: Admin/Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Tieude,NoiDung,ThoiGian,UserId,ProductId")] Comments comments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comments).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "MaSp", comments.ProductId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", comments.UserId);
            return View(comments);
        }

        // GET: Admin/Comments/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Comments comments = await db.Comments.FindAsync(id);
        //    if (comments == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(comments);
        //}

        //// POST: Admin/Comments/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            Comments comments = await db.Comments.FindAsync(id);
            db.Comments.Remove(comments);
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
