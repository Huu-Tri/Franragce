using fragrance.DTO;
using fragrance.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
                return RedirectToAction("Login", "User");
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
                Session.Timeout = (int)TimeSpan.FromDays(1).TotalMinutes;
				return RedirectToAction("Index", "Fragrance");
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(model);
            }
        }

		[HttpGet]
		public ActionResult UserProfile()
		{
            if (Session["acc_user"] == null)
            {
				return RedirectToAction("Login", "User");
			}
			var curentUser = GetCurrentUser();
            var response = new UserEdit();
            response.Id = curentUser.id_user;
            response.Username = curentUser.name_user;
            response.Email = curentUser.email_user;
            response.Phone = curentUser.phone_user;
            response.user_order = curentUser.user_order;
			return View(response);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult UserProfile(UserEdit model)
		{
			if (model.Password != model.ConfirmPassword)
			{
				ModelState.AddModelError("ConfirmPassword", "The password and confirm password do not match.");
				return View(model);
			}
            var curentUser = GetCurrentUser();
            var user = db.acc_user.Where(x => x.id_user == curentUser.id_user).FirstOrDefault();
            if(model.Password != user.password_user)
            {
				ModelState.AddModelError("", "Oops update failed");
				return View(model);
			}
            user.name_user = model.Username;
            user.email_user = model.Email;
            user.password_user = model.Password;
            db.SaveChanges();
			var response = new UserEdit();
			response.Id = user.id_user;
			response.Username = user.name_user;
			response.Email = user.email_user;
			response.Phone = user.phone_user;
			response.user_order = curentUser.user_order;
			return View(response);
		}
        public ActionResult Logout()
        {
            Session.Abandon(); // Xóa toàn bộ session của người dùng
            return RedirectToAction("Index", "Fragrance"); // Chuyển hướng về trang chủ
        }
        private acc_user GetCurrentUser()
        {
			acc_user curentUser = (acc_user)Session["acc_user"];
            var user = db.acc_user.Where(x => x.id_user == curentUser.id_user).FirstOrDefault();
            return user;
		}
    }
}