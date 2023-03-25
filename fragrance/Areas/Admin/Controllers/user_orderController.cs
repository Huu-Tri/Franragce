using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using fragrance.Models;

namespace fragrance.Areas.Admin.Controllers
{
    public class user_orderController : Controller
    {
        private FragranceDbContext db = new FragranceDbContext();

        // GET: Admin/user_order
        public ActionResult Index()
        {
            var user_order = db.user_order.Include(u => u.acc_user);
            return View(user_order.ToList());
        }

        // GET: Admin/user_order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_order user_order = db.user_order.Find(id);
            if (user_order == null)
            {
                return HttpNotFound();
            }
            return View(user_order);
        }

        // GET: Admin/user_order/Create
        public ActionResult Create()
        {
            ViewBag.id_order_user = new SelectList(db.acc_user, "id_user", "name_user");
            return View();
        }

        // POST: Admin/user_order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_order,receiver_oder,status_order,address_order,date_order,phone_order,action_order,id_order_user,created_at")] user_order user_order)
        {
            if (ModelState.IsValid)
            {
                db.user_order.Add(user_order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_order_user = new SelectList(db.acc_user, "id_user", "name_user", user_order.id_order_user);
            return View(user_order);
        }

        // GET: Admin/user_order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_order user_order = db.user_order.Find(id);
            if (user_order == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_order_user = new SelectList(db.acc_user, "id_user", "name_user", user_order.id_order_user);
            return View(user_order);
        }

        // POST: Admin/user_order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_order,receiver_oder,status_order,address_order,date_order,phone_order,action_order,id_order_user,created_at")] user_order user_order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user_order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_order_user = new SelectList(db.acc_user, "id_user", "name_user", user_order.id_order_user);
            return View(user_order);
        }

        // GET: Admin/user_order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_order user_order = db.user_order.Find(id);
            if (user_order == null)
            {
                return HttpNotFound();
            }
            return View(user_order);
        }

        // POST: Admin/user_order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            user_order user_order = db.user_order.Find(id);
            db.user_order.Remove(user_order);
            db.SaveChanges();
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
