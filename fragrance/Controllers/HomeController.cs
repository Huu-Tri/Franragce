using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace FileUploadExample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                file.SaveAs(path);
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
