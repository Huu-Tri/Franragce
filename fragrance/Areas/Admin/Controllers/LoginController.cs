using fragrance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace fragrance.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private FragranceDbContext db = new FragranceDbContext();
        private AdminLogin adlogin = new AdminLogin();
        // GET: Admin/AdminLogin
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AdminLogin model)
        {
            var admin = db.admins.Where(a => a.email_ad.Equals(model.Email) && a.password_ad.Equals(model.Password)).FirstOrDefault();
            if (admin != null)
            {
                Session["Admin_Email"] = admin.email_ad;
                Session["Admin_Name"] = admin.name_ad;
                return RedirectToAction("Index", "Home");
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
            return RedirectToAction("Login", "Login"); // Chuyển hướng về trang chủ
        }
    }
}