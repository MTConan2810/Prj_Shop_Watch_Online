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
    public class UsersController : Controller
    {
        private SWODBContext db = new SWODBContext();

        // GET: Admin/Users
        public ActionResult Index(string keyword, string groupcode, int? page, int? pagesize)
        {
            List<Users> users = db.Users.Select(s => s).ToList();
            List<int> pagecout = new List<int> { 5, 20, 25, 50 };
            ViewBag.pagesize = new SelectList(pagecout);
            ViewBag.pagesizenow = pagesize;
            if (!string.IsNullOrEmpty(keyword))
            {

                users = users.Where(obj =>
                                       obj.Username.ToUpper().Contains(keyword.ToUpper())
                                       ).Select(s => s).ToList();

            }
            switch(groupcode)
            {
                case "NV":
                    var check = from u in db.Users
                                from ug in db.UserGroupRole
                                where ug.GroupCode.Equals("NV") && u.Username.ToUpper().Equals(ug.Username.ToUpper())
                                select u;
                    users = check.ToList();
                    break;
                case "KH":
                    users = db.Users.Where(obj => !obj.Note.Equals("Quản lý") && !obj.Note.Equals("Nhân viên")).Select(s=>s).ToList();
                    break;
            }    
            int pageSize = (pagesize ?? 10);
            int pageNumber = (page ?? 1);
            return View(users.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Users/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = await db.Users.FindAsync(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Username,Password,FullName,Email,Note,Active")] Users users,string comfirmpass)
        {
            if (ModelState.IsValid)
            {
                if(comfirmpass.Equals(users.Password))
                {
                    db.Users.Add(users);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Edit", new { id = users.Id });
                }
                else
                {
                    ViewBag.thongbao = "Xác nhận lại mật khẩu!";
                    return View(users);
                }
            }

            return View(users);
        }

        // GET: Admin/Users/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = await db.Users.FindAsync(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewBag.appcode = new SelectList(db.AppList, "AppCode", "AppName");
            ViewBag.groupcode = new SelectList(db.AppGroupRole, "GroupCode", "GroupName");
            return View(users);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Username,Password,FullName,Email,Note,Active")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(users);
        }
        [HttpPost]
        public async Task<ActionResult> AddUGR(int userId,string username, string appcode, string groupcode)
        {
            UserGroupRole userGroupRole = new UserGroupRole();
            userGroupRole.Username = username;
            userGroupRole.AppCode = appcode;
            userGroupRole.GroupCode = groupcode;
            if(!string.IsNullOrEmpty(appcode)&&!string.IsNullOrEmpty(groupcode))
            {
                db.UserGroupRole.Add(userGroupRole);
                await db.SaveChangesAsync();
            }         
            return RedirectToAction("Edit", new { id = userId });
        }




        //// GET: Admin/Users/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Users users = await db.Users.FindAsync(id);
        //    if (users == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(users);
        //}

        //// POST: Admin/Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            Users users = await db.Users.FindAsync(id);
            db.Users.Remove(users);
            foreach (var obj in db.UserGroupRole)
            {
                if (obj.Username.ToUpper().Equals(users.Username.ToUpper()))
                {
                    db.UserGroupRole.Remove(obj);
                }
            }
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> DeleteUGR(int id, int userId)
        {
            UserGroupRole userGroupRole = await db.UserGroupRole.FindAsync(id);
            db.UserGroupRole.Remove(userGroupRole);
            await db.SaveChangesAsync();
            return RedirectToAction("Edit", new { id = userId });
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
