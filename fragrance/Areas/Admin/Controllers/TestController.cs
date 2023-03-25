using fragrance.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace fragrance.Areas.Admin.Controllers
{
    public class TestController : Controller
    {
        // GET: Admin/Test
        public ActionResult Index(HttpPostedFileBase fFileUpload)
        {
            if (fFileUpload != null && fFileUpload.ContentLength > 0)
            {
                var image_pr = Path.GetFileName(fFileUpload.FileName);
                var path = Path.Combine(Server.MapPath("~/Assets/Images/Products"), image_pr);
                if (!System.IO.File.Exists(path))
                {
                    fFileUpload.SaveAs(path);
                }
            }
            return View();
        }
        public ActionResult Test()
        {
            return View();
        }
    }
}