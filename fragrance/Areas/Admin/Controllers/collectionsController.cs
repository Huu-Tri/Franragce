using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using fragrance.Models;

namespace fragrance.Areas.Admin.Controllers
{
    public class collectionsController : BaseController
    {
        private FragranceDbContext db = new FragranceDbContext();

        // GET: Admin/collections
        public ActionResult Index()
        {
            return View(db.collections.ToList());
        }

        // GET: Admin/collections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            collection collection = db.collections.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        // GET: Admin/collections/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/collections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_collect,name_collect,desc_collect,image_collect,created_at")] collection collection, HttpPostedFileBase image_collect)
        {
            if (ModelState.IsValid)
            {
                if (image_collect != null && image_collect.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(image_collect.FileName);
                    var path = Path.Combine(Server.MapPath("~/Assets/Images/Collection"), fileName);

                    if (!System.IO.File.Exists(path))
                    {
                        image_collect.SaveAs(path);
                    }

                    image_collect.SaveAs(path);
                    collection.image_collect = fileName;
                }
                db.collections.Add(collection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(collection);
        }

        // GET: Admin/collections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            collection collection = db.collections.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        // POST: Admin/collections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_collect,name_collect,desc_collect,image_collect,created_at")] collection collection, HttpPostedFileBase image_collect)
        {
            if (ModelState.IsValid)
            {
                if (image_collect != null && image_collect.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(image_collect.FileName);
                    var path = Path.Combine(Server.MapPath("~/Assets/Images/Collection"), fileName);

                    if (!System.IO.File.Exists(path))
                    {
                        image_collect.SaveAs(path);
                    }

                    image_collect.SaveAs(path);
                    collection.image_collect = fileName;
                }
                db.Entry(collection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(collection);
        }

        // GET: Admin/collections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            collection collection = db.collections.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        // POST: Admin/collections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            collection collection = db.collections.Find(id);
            db.collections.Remove(collection);
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
