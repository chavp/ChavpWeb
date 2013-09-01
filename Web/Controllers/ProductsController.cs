using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        //
        // GET: /Products/

        public ActionResult Index()
        {
            return View(MvcApplication.Products);
        }

        [HttpPost]
        public JsonResult Add(Product p)
        {
            Thread.Sleep(1500);
            var uniqQuery = from x in MvcApplication.Products
                            where x.CodeName == p.CodeName
                            select x;
            if (uniqQuery.Count() == 0)
            {
                p.Status = EProductStatus.Concept;
                MvcApplication.Products.Add(p);
                return Json(new { success = true, data = p, message = "Adding completed." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "Already have a code name." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Save(Product p)
        {
            Thread.Sleep(1500);
            var uniqQuery = from x in MvcApplication.Products
                            where x.CodeName == p.CodeName
                            select x;
            if (uniqQuery.Count() > 0)
            {
                var oldProduc = uniqQuery.Single();
                oldProduc.Brand = p.Brand;
                oldProduc.Name = p.Name;
                oldProduc.Slogan = p.Slogan;
            }
            return Json(new { success = true, data = p, message = "Save completed." }, JsonRequestBehavior.AllowGet);
        }
    }
}
