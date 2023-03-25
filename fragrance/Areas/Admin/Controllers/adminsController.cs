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
    public class adminsController : BaseController
    {
        private FragranceDbContext db = new FragranceDbContext();
        private bool IsAdminEmailExist(string email)
        {
            return db.admins.Any(a => a.email_ad == email);
        }
        // GET: Admin/admins
        public ActionResult Index()
        {
            return View(db.admins.ToList());
        }

        // GET: Admin/admins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            admin admin = db.admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // GET: Admin/admins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_ad,name_ad,email_ad,password_ad,created_at")] admin admin)
        {
            if (ModelState.IsValid)
            {
                if (IsAdminEmailExist(admin.email_ad))
                {
                    ModelState.AddModelError("email_ad", "Email already exists");
                    return View(admin);
                }
                db.admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
        }

        // GET: Admin/admins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            admin admin = db.admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admin/admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_ad,name_ad,email_ad,password_ad,created_at")] admin admin)
        {
            if (ModelState.IsValid)
            {
                if (IsAdminEmailExist(admin.email_ad))
                {
                    ModelState.AddModelError("email_ad", "Email already exists");
                    return View(admin);
                }
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // GET: Admin/admins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            admin admin = db.admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admin/admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            admin admin = db.admins.Find(id);
            if (admin.id_ad == 1)
            {
                ViewBag.Errorad = "Can't delete admin!";
                return View(admin);
            }
            db.admins.Remove(admin);
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
