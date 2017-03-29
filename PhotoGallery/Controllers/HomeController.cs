using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoGallery.Models;

namespace PhotoGallery.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetData()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var ret = context.Photos.ToList().Select(x => new { x.Id, x.Title, x.Summary, Path = x.Uri }).ToList();
                return Json(ret, JsonRequestBehavior.AllowGet);
            }
        }
    }
}