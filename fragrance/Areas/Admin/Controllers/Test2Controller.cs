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
    public class Test2Controller : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase image_pr)
        {
            if (image_pr != null && image_pr.ContentLength > 0)
            {
                var fileName = Path.GetFileName(image_pr.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                image_pr.SaveAs(path);
                ViewBag.Message = "File uploaded successfully!";
            }
            else
            {
                ViewBag.Message = "File is empty or exceeds the maximum allowed size!";
            }

            return View();
        }

    }
}
