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

namespace Prj_Shop_Watch_Online.Controllers
{
    public class AccountController : Controller
    {
        private SWODBContext db = new SWODBContext();

        // GET: Account
        public ActionResult Register()
        {           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register([Bind(Include = "Id,Username,Password,FullName,Email,Active")] Users users)
        {
            var tkcheck = db.Users.Where(tk => tk.Email == users.Email || tk.Username == users.Username);
            if (tkcheck.Count() == 0)
            {
                if (ModelState.IsValid)
                {
                    if(users.Active == true)
                    {
                        db.Users.Add(users);
                        await db.SaveChangesAsync();                        
                    }
                    else
                    {
                        ViewBag.thongbao = "Bạn vui lòng đồng ý với chính sách và điều khoản sử dụng";
                        return View(users);
                    }
                }
                return RedirectToAction("Login");   
            }
            else
            {
                ViewBag.error = "Tài khoản đã tồn tại";
                return View(users);
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string Username, string Password)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Where(u => u.Username.ToUpper().Equals(Username.ToUpper()) && u.Password.Equals(Password)).ToList();
                if (user.Count() > 0)
                {
                    //add session
                    Session["HoTenUI"] = user.FirstOrDefault().FullName;
                    Session["EmailUI"] = user.FirstOrDefault().Email;
                    Session["idUser"] = user.FirstOrDefault().Id;
                    if(user.FirstOrDefault().Active == true)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.error = "Tài khoản của bạn đã bị khoá! Vui lòng liên hệ 1800 6785...!";
                    }
                }
                else
                {
                    ViewBag.error = "Tài khoản hoặc mật khẩu sai! Vui lòng đăng nhập lại!!!!";
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
        
       

        // GET: Account/Edit/5
        public async Task<ActionResult> DoiPass(int? id)
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

        // POST: Account/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DoiPass([Bind(Include = "Id,Username,Password,FullName,Email,Note,Active")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(users);
        }    
    }
}
