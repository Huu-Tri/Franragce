using fragrance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace fragrance.Controllers
{
    public class UserController : Controller
    {
        
        private FragranceDbContext db = new FragranceDbContext();


        [HttpGet]

        // GET: User
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "id_user,name_user,email_user,phone_user,password_user,created_at")] acc_user acc_user)
        {
            if (ModelState.IsValid)
            {
                db.acc_user.Add(acc_user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(acc_user);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin model)
        {
            var user = db.acc_user.Where(a => a.email_user.Equals(model.Email) && a.password_user.Equals(model.Password)).FirstOrDefault();
            if (user != null)
            {
                Session["acc_user"] = user;
                Session["User_Email"] = user.email_user;
                Session["User_Name"] = user.name_user;
                return RedirectToAction("Index", "Fragrance");
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(model);
            }

        }
        public ActionResult Logout()
        {
            Session.Abandon(); // Xóa toàn bộ session của người dùng
            return RedirectToAction("Index", "Fragrance"); // Chuyển hướng về trang chủ
        }
    }
}