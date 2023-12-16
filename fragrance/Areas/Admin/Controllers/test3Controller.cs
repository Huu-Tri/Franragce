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
    public class test3Controller : Controller
    {
        private FragranceDbContext db = new FragranceDbContext();

        // GET: Admin/test3
        public ActionResult Index()
        {
            var product_type = db.product_type.Include(p => p.menu);
            return View(product_type.ToList());
        }

        // GET: Admin/test3/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_type product_type = db.product_type.Find(id);
            if (product_type == null)
            {
                return HttpNotFound();
            }
            return View(product_type);
        }

        // GET: Admin/test3/Create
        public ActionResult Create()
        {
            ViewBag.id_menu = new SelectList(db.menus, "Id", "Name");
            return View();
        }

        // POST: Admin/test3/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_prt,name_prt,image_prt,desc_prt,IsActive,forgender_prt,created_at,id_menu")] product_type product_type)
        {
            if (ModelState.IsValid)
            {
                db.product_type.Add(product_type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_menu = new SelectList(db.menus, "Id", "Name", product_type.id_menu);
            return View(product_type);
        }

        // GET: Admin/test3/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_type product_type = db.product_type.Find(id);
            if (product_type == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_menu = new SelectList(db.menus, "Id", "Name", product_type.id_menu);
            return View(product_type);
        }

        // POST: Admin/test3/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_prt,name_prt,image_prt,desc_prt,IsActive,forgender_prt,created_at,id_menu")] product_type product_type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_menu = new SelectList(db.menus, "Id", "Name", product_type.id_menu);
            return View(product_type);
        }

        // GET: Admin/test3/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_type product_type = db.product_type.Find(id);
            if (product_type == null)
            {
                return HttpNotFound();
            }
            return View(product_type);
        }

        // POST: Admin/test3/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product_type product_type = db.product_type.Find(id);
            db.product_type.Remove(product_type);
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
