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
    public class acc_userController : Controller
    {
        private FragranceDbContext db = new FragranceDbContext();

        // GET: Admin/acc_user
        public ActionResult Index()
        {
            return View(db.acc_user.ToList());
        }

        // GET: Admin/acc_user/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            acc_user acc_user = db.acc_user.Find(id);
            if (acc_user == null)
            {
                return HttpNotFound();
            }
            return View(acc_user);
        }

        // GET: Admin/acc_user/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/acc_user/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_user,name_user,email_user,phone_user,password_user,created_at")] acc_user acc_user)
        {
            if (ModelState.IsValid)
            {
                db.acc_user.Add(acc_user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(acc_user);
        }

        // GET: Admin/acc_user/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            acc_user acc_user = db.acc_user.Find(id);
            if (acc_user == null)
            {
                return HttpNotFound();
            }
            return View(acc_user);
        }

        // POST: Admin/acc_user/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_user,name_user,email_user,phone_user,password_user,created_at")] acc_user acc_user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(acc_user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(acc_user);
        }

        // GET: Admin/acc_user/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            acc_user acc_user = db.acc_user.Find(id);
            if (acc_user == null)
            {
                return HttpNotFound();
            }
            return View(acc_user);
        }

        // POST: Admin/acc_user/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            acc_user acc_user = db.acc_user.Find(id);
            db.acc_user.Remove(acc_user);
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
