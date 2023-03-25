using fragrance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace fragrance.Controllers
{
    public class FragranceController : Controller
    {
        private FragranceDbContext db = new FragranceDbContext();

        private List<product> GetProduct()
        {
            return db.products.Where( m => m.status_pr==1).OrderByDescending(m=>m.created_at).ToList();
        }
        // GET: Fragrance
        public ActionResult Index()
        {
            var getproduct = GetProduct();
            return View(getproduct.ToList());
        }

        public ActionResult TypeProductFamale()
        {
            var type = from ty 
                       in db.product_type
                       where ty.forgender_prt == "famale"
                       select ty;
            return PartialView(type);
        }

        public ActionResult TypeProductMale()
        {
            var type = from ty
                       in db.product_type
                       where ty.forgender_prt == "male"
                       select ty;
            return PartialView(type);
        }
        public ActionResult TypeProduct(int id)
        {
            var pr = from s in db.products
                     where s.id_pro_typeof == id
                     select s;
            return View(pr.ToList());
        }
        public ActionResult Product(int id)
        {
            var pr = from s in db.products where s.id_pr == id select s;
            return PartialView(pr.Single());
        }
        public ActionResult Search(string strSearch)
        {

            ViewBag.Search = strSearch;
            if (!string.IsNullOrEmpty(strSearch))
            {
                
                var re = from s in db.products where s.name_pr.Contains(strSearch) select s;

                return View(re.ToList());
            }
            return View();
        }

    }
}