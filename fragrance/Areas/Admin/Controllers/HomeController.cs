using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace fragrance.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            // Kiểm tra xem người dùng đã đăng nhập hay chưa
            if (Session["Admin_Email"] == null)
            {
                // Nếu chưa đăng nhập, chuyển hướng người dùng đến trang đăng nhập
                return RedirectToAction("Login", "Login");
            }

            // Nếu đã đăng nhập, trả về trang chủ
            return View();
        }
    }
}