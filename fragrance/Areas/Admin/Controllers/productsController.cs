using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using fragrance.Models;

namespace fragrance.Areas.Admin.Controllers
{
    public class productsController : BaseController
    {
        private FragranceDbContext db = new FragranceDbContext();

        // GET: Admin/products
        public ActionResult Index()
        {
            var products = db.products.Include(p => p.collection_child).Include(p => p.product_type);
            return View(products.ToList());
        }

        // GET: Admin/products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/products/Create
        public ActionResult Create()
        {
            var selectNullList = new List<SelectListItem>
            {
                new SelectListItem { Text = "Select", Value = "" }
            };

            var dbCollectionList = new SelectList(db.collection_child, "id_c_child", "name_c_child");
            var dbTypepro = new SelectList(db.product_type, "id_prt", "name_prt");
            ViewBag.id_pro_coll = new SelectList(selectNullList.Union(dbCollectionList), "Value", "Text");
            ViewBag.id_pro_typeof = new SelectList(selectNullList.Union(dbTypepro), "Value", "Text");
            return View();
        }

        // POST: Admin/products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_pr,name_pr,image_pr,volume_pr,price_pr,amount_pr,desc_pr,notes_pr,tips_pr,status_pr,id_pro_typeof,id_pro_coll,created_at")] product product, HttpPostedFileBase image_pr)
        {
            if (ModelState.IsValid)
            {
                if (image_pr != null && image_pr.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(image_pr.FileName);
                    var path = Path.Combine(Server.MapPath("~/Assets/Images/Products"), fileName);

                    if (!System.IO.File.Exists(path))
                    {
                        image_pr.SaveAs(path);
                    }

                    image_pr.SaveAs(path);
                    product.image_pr = fileName;
                }

                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_pro_coll = new SelectList(db.collection_child, "id_c_child", "name_c_child", product.id_pro_coll);
            ViewBag.id_pro_typeof = new SelectList(db.product_type, "id_prt", "name_prt", product.id_pro_typeof);
            return View(product);
        }

        /*public ActionResult Create([Bind(Include = "id_pr,name_pr,image_pr,volume_pr,price_pr,amount_pr,desc_pr,notes_pr,tips_pr,status_pr,id_pro_typeof,id_pro_coll,created_at")] product product,HttpPostedFileBase image_pr)
        {
            if (ModelState.IsValid)
            {
                if (image_pr != null && image_pr.ContentLength > 0)
                {
                    var sFileName = Path.GetFileName(image_pr.FileName);
                    var path = Path.Combine(Server.MapPath("~/Assets/Images/Products"), sFileName);
                    if (!System.IO.File.Exists(path))
                    {
                        image_pr.SaveAs(path);
                    }
                }
                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_pro_coll = new SelectList(db.collection_child, "id_c_child", "name_c_child", product.id_pro_coll);
            ViewBag.id_pro_typeof = new SelectList(db.product_type, "id_prt", "name_prt", product.id_pro_typeof);
            return View(product);
        }*/

        // GET: Admin/products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            var selectNullList = new List<SelectListItem>
            {
                new SelectListItem { Text = "Select", Value = "" }
            };

            var dbCollectionList = new SelectList(db.collection_child, "id_c_child", "name_c_child", product.id_pro_coll);
            var dbTypepro = new SelectList(db.product_type, "id_prt", "name_prt", product.id_pro_typeof);
            ViewBag.id_pro_coll = new SelectList(selectNullList.Union(dbCollectionList), "Value", "Text");
            ViewBag.id_pro_typeof = new SelectList(selectNullList.Union(dbTypepro), "Value", "Text");
            return View(product);
        }

        // POST: Admin/products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_pr,name_pr,image_pr,volume_pr,price_pr,amount_pr,desc_pr,notes_pr,tips_pr,status_pr,id_pro_typeof,id_pro_coll,created_at")] product product, HttpPostedFileBase image_pr)
        {
            if (ModelState.IsValid)
            {
                if (image_pr != null && image_pr.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(image_pr.FileName);
                    var path = Path.Combine(Server.MapPath("~/Assets/Images/Products"), fileName);

                    if (!System.IO.File.Exists(path))
                    {
                        image_pr.SaveAs(path);
                    }

                    image_pr.SaveAs(path);
                    product.image_pr = fileName;
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_pro_coll = new SelectList(db.collection_child, "id_c_child", "name_c_child", product.id_pro_coll);
            ViewBag.id_pro_typeof = new SelectList(db.product_type, "id_prt", "name_prt", product.id_pro_typeof);
            return View(product);
        }

        // GET: Admin/products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
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
