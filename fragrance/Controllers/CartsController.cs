using fragrance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace fragrance.Controllers
{
    public class CartsController : Controller
    {
        // GET: Carts
        FragranceDbContext db = new FragranceDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public List<Cart> GetCarts()
        {
            List<Cart> CartList = Session["Cart"] as List<Cart>;
            if (CartList == null)
            {
                CartList = new List<Cart>();
                Session["Cart"] = CartList;
            }
            return CartList;
        }
        public ActionResult AddCart(int ms, string url)
        {

            List<Cart> CartList = GetCarts();

            Cart sp = CartList.Find(n => n.iPro == ms);
            if (sp == null)
            {
                sp = new Cart(ms);
                CartList.Add(sp);
            }
            else
            {
                sp.iQuantity++;
            }
            return Redirect(url);
        }

        private double SumCart()
        {
            int iSum = 0;
            List<Cart> CartList = Session["Cart"] as List<Cart>;
            if (CartList != null)
            {
                iSum = CartList.Sum(n => n.iQuantity);
            }
            return iSum;
        }

        private double SumCartMoney()
        {
            double dSumMoney = 0;
            List<Cart> CartList = Session["Cart"] as List<Cart>;
            if (CartList != null)
            {
                dSumMoney = CartList.Sum(n => n.dSumMoney);
            }
            return dSumMoney;
        }

        public ActionResult Cart()
        {
            List<Cart> CartList = GetCarts();
            if (CartList.Count == 0)
            {
                return RedirectToAction("Index", "Fragrance");
            }
            ViewBag.SumCart = SumCart();
            ViewBag.SumCartMoney = SumCartMoney();
            return View(CartList);
        }
        public ActionResult CartPartial()
        {
            ViewBag.SumCart = SumCart();
            ViewBag.SumCartMoney = SumCartMoney();
            return PartialView();
        }

        public ActionResult DeleteProFromCart(int iPro)
        {
            List<Cart> CartList = GetCarts();

            Cart sp = CartList.SingleOrDefault(n => n.iPro == iPro);

            if (sp != null)
            {
                CartList.RemoveAll(n => n.iPro == iPro);
                if (CartList.Count == 0)
                {
                    return RedirectToAction("Index", "Fragrance");
                }
            }
            return RedirectToAction("Cart");
        }

        public ActionResult UpdateCart(int iPro, FormCollection f)
        {
            List<Cart> CartList = GetCarts();
            Cart sp = CartList.SingleOrDefault(n => n.iPro == iPro);

            if (sp != null)
            {
                sp.iQuantity = int.Parse(f["quantity"].ToString());
            }
            return RedirectToAction("Cart");
        }

        public ActionResult DeleteCart()
        {
            List<Cart> CartList = GetCarts();
            CartList.Clear();
            return RedirectToAction("Index", "Fragrance");
        }
        [HttpGet]
        public ActionResult Order()
        {

            if (Session["User_Name"] == null || Session["User_Name"].ToString() == "")
            {
                return Redirect("~/User/Login?id=2");
            }

            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Fragrance");
            }
            // lấy hàng từ session
            List<Cart> CartList = GetCarts();
            ViewBag.SumCart = SumCart();
            ViewBag.SumCartMoney = SumCartMoney();
            return View(CartList);
        }
        [HttpPost]
        public ActionResult Order(FormCollection f)
        {

            user_order od = new user_order();
            acc_user us = (acc_user)Session["acc_user"];
            List<Cart> CartList = GetCarts();
            od.id_order_user = us.id_user;
            od.status_order = "Confirm";
            od.receiver_oder = f["receiver_oder"];
            od.address_order = f["address_order"];
            od.phone_order = f["phone_order"];
            od.action_order = 1;
            db.user_order.Add(od);
            db.SaveChanges();
            foreach (var item in CartList)
            {
                order_details order_details = new order_details();
                order_details.id_order_dt = od.id_order;
                order_details.id_pro = item.iPro;
                order_details.amount_order_dt = item.iQuantity;
                order_details.order_details_total = (decimal)item.dSumMoney;
                db.order_details.Add(order_details);

            }
            db.SaveChanges();
            Session["Cart"] = null;
            return RedirectToAction("OrderConfirm", "Carts");
        }
        public ActionResult OrderConfirm()
        {
            return View();
        }
    }
}