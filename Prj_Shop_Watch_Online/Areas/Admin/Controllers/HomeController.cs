using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prj_Shop_Watch_Online.Models;

namespace Prj_Shop_Watch_Online.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        SWODBContext db = new SWODBContext();
        // GET: Admin/Home
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
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
                    Session["HoTen"] = user.FirstOrDefault().FullName;
                    Session["Email"] = user.FirstOrDefault().Email;
                    Session["UserID"] = user.FirstOrDefault().Id;
                    var checkRole = db.UserGroupRole.Where(ur => ur.Username.ToUpper().Contains(Username.ToUpper())).ToList();
                    if(user.FirstOrDefault().Active == true)
                    {
                        if(checkRole.Count >0)
                        {
                            foreach (var item in checkRole)
                            {
                                if (item.GroupCode.Equals("ADMIN") || item.GroupCode.Equals("NV"))
                                {
                                    Session["Role"] = item.GroupCode;
                                    return RedirectToAction("Index", "Home");
                                }
                            }
                        } 
                        else
                        {
                            ViewBag.error = "Tài khoản của bạn không có quyền truy cập ứng dụng này..!";
                        }
                        
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

    }
}