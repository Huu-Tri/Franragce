using fragrance.DTO;
using fragrance.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace fragrance.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private FragranceDbContext db = new FragranceDbContext();
        // GET: Admin/Home
        public ActionResult Index()
        {
            ViewBag.OrderNeedAction = db.user_order.Where(x => x.action_order != (int)OrderAction.Success && x.action_order != (int)OrderAction.Cancel && x.action_order != (int)OrderAction.Received).Count();
            /*var aaa = db.user_order.Where(x => x.created_at.Value.Month == DateTime.Now.Month && x.created_at.Value.Year == DateTime.Now.Year && x.action_order == (int)OrderAction.Success).Include(x => x.order_details).SelectMany(x => x.order_details).Include(x => x.product).ToList();
            var pros = db.products.Where(x => aaa.Select(y => y.id_pro).Contains(x.id_pr)).ToList();*/
            ViewBag.Earn = "20000";
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