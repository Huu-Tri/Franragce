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
    public class collection_childController : Controller
    {
        private FragranceDbContext db = new FragranceDbContext();

        // GET: Admin/collection_child
        public ActionResult Index()
        {
            var collection_child = db.collection_child.Include(c => c.collection);
            return View(collection_child.ToList());
        }

        // GET: Admin/collection_child/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            collection_child collection_child = db.collection_child.Find(id);
            if (collection_child == null)
            {
                return HttpNotFound();
            }
            return View(collection_child);
        }

        // GET: Admin/collection_child/Create
        public ActionResult Create()
        {
            ViewBag.id_c_collect = new SelectList(db.collections, "id_collect", "name_collect");
            return View();
        }

        // POST: Admin/collection_child/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_c_child,name_c_child,desc_c_child,id_c_collect,created_at")] collection_child collection_child)
        {
            if (ModelState.IsValid)
            {
                db.collection_child.Add(collection_child);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_c_collect = new SelectList(db.collections, "id_collect", "name_collect", collection_child.id_c_collect);
            return View(collection_child);
        }

        // GET: Admin/collection_child/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            collection_child collection_child = db.collection_child.Find(id);
            if (collection_child == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_c_collect = new SelectList(db.collections, "id_collect", "name_collect", collection_child.id_c_collect);
            return View(collection_child);
        }

        // POST: Admin/collection_child/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_c_child,name_c_child,desc_c_child,id_c_collect,created_at")] collection_child collection_child)
        {
            if (ModelState.IsValid)
            {
                db.Entry(collection_child).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_c_collect = new SelectList(db.collections, "id_collect", "name_collect", collection_child.id_c_collect);
            return View(collection_child);
        }

        // GET: Admin/collection_child/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            collection_child collection_child = db.collection_child.Find(id);
            if (collection_child == null)
            {
                return HttpNotFound();
            }
            return View(collection_child);
        }

        // POST: Admin/collection_child/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            collection_child collection_child = db.collection_child.Find(id);
            db.collection_child.Remove(collection_child);
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
