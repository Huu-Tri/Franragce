using fragrance.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace fragrance.Controllers
{
    public class FragranceController : Controller
    {
        private FragranceDbContext db = new FragranceDbContext();

        private List<product> GetHotProduct(int count)
        {
            return db.products.OrderByDescending(a => a.order_details.Sum(c => c.amount_order_dt)).Take(count).ToList();
        }
        private List<product> GetProduct()
        {
            return db.products.Where( m => m.status_pr==1).OrderByDescending(m=>m.created_at).ToList();
        }
        private async Task<List<collection>> GetCollections()
        {
            var collect = await db.collection_child.Include(c => c.products).ToListAsync();
            var collections = await db.collections
                              .Include(c => c.collection_child == collect)
                              .ToListAsync();
            return collections;
        }
        public ActionResult Collections()
        {
            var getCollections = GetCollections();
            return View(getCollections);
        }
        // GET: Fragrance

        public ActionResult Index()
        {
            var getproduct = GetProduct();
            return View(getproduct.Take(6).ToList());
        }

		public ActionResult Shop()
		{
            var products = db.product_type.Include(x => x.products).ToList();
			return View(products);
		}

		public ActionResult SliderPartial()
        {
            var listSph = GetHotProduct(10);
            return PartialView(listSph);
        }
        public ActionResult TypeProductFamale()
        {
            var type = from ty 
                       in db.product_type
                       where ty.forgender_prt == "famale"
                       select ty;
            return PartialView(type);
        }
        [ChildActionOnly]
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
            var pr = db.products.Where(x => x.id_pr == id).FirstOrDefault();
            ViewBag.Related = db.products.Where(x => x.id_pro_typeof == pr.id_pro_typeof && x.id_pr != id).ToList();
            return PartialView(pr);
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