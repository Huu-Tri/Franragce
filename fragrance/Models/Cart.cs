using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fragrance.Models
{
    public class Cart
    {
        private FragranceDbContext db = new FragranceDbContext();
        public int iPro { get; set; }
        public string sNamepr { get; set; }
        public string sImagepr { get; set; }
        public double dPricepr { get; set; }
        public int iQuantity { get; set; }
        public string Message { get; set; }
        public double dSumMoney
        {
            get { return iQuantity * dPricepr; }
        }
        public Cart(int ms)
        {
            iPro = ms;
            product s = db.products.Single(n => n.id_pr == iPro);
            sNamepr = s.name_pr;
            sImagepr = s.image_pr;
            dPricepr = double.Parse(s.price_pr.ToString());
            iQuantity = 1;
        }
    }
}