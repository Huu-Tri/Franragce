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
    public class product_typeController : Controller
    {
        private FragranceDbContext db = new FragranceDbContext();

        // GET: Admin/product_type
        public ActionResult Index()
        {
			var product_type = db.product_type.Include(p => p.menu);
			return View(product_type.ToList());
		}

        // GET: Admin/product_type/Details/5
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

        // GET: Admin/product_type/Create
        public ActionResult Create()
        {
			ViewBag.id_menu = new SelectList(db.menus, "Id", "Name");
			return View();
        }

        // POST: Admin/product_type/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_prt,name_prt,image_prt,desc_prt,forgender_prt,created_at,id_menu")] product_type product_type, HttpPostedFileBase image_prt)
        {
            if (ModelState.IsValid)
            {
                if (image_prt != null && image_prt.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(image_prt.FileName);
                    var path = Path.Combine(Server.MapPath("~/Assets/Images/Product_type"), fileName);

                    if (!System.IO.File.Exists(path))
                    {
                        image_prt.SaveAs(path);
                    }

                    image_prt.SaveAs(path);
                    product_type.image_prt = fileName;
                }
                db.product_type.Add(product_type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
			ViewBag.id_menu = new SelectList(db.menus, "Id", "Name", product_type.id_menu);
			return View(product_type);
        }

        // GET: Admin/product_type/Edit/5
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

        // POST: Admin/product_type/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_prt,name_prt,image_prt,desc_prt,forgender_prt,created_at, id_menu")] product_type product_type, HttpPostedFileBase image_prt)
        {
            if (ModelState.IsValid)
            {
                if (image_prt != null && image_prt.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(image_prt.FileName);
                    var path = Path.Combine(Server.MapPath("~/Assets/Images/Product_type"), fileName);

                    if (!System.IO.File.Exists(path))
                    {
                        image_prt.SaveAs(path);
                    }

                    image_prt.SaveAs(path);
                    product_type.image_prt = fileName;
                }
                db.Entry(product_type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
			ViewBag.id_menu = new SelectList(db.menus, "Id", "Name", product_type.id_menu);
			return View(product_type);
        }

        // GET: Admin/product_type/Delete/5
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

        // POST: Admin/product_type/Delete/5
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
