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
            return View();
        }

        [HttpGet]
        public ActionResult GetProducts(int start, int limit)
        {
            var total = MvcApplication.Products.Count;
            var result = MvcApplication.Products.Skip(start).Take(limit).ToList();

            return Json(new { success = true, data = result, message = "", total = total }, JsonRequestBehavior.AllowGet);
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
                return Json(new { success = true, data = p, message = string.Format("Adding {0} completed.", p.CodeName) }, JsonRequestBehavior.AllowGet);
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

        [HttpPost]
        public JsonResult Remove(string codeName)
        {
            Thread.Sleep(1500);
            var uniqQuery = from x in MvcApplication.Products
                            where x.CodeName == codeName
                            select x;
            if (uniqQuery.Count() > 0)
            {
                var oldProduc = uniqQuery.Single();
                MvcApplication.Products.Remove(oldProduc);
            }

            return Json(new { success = true, data = codeName, message = "Remove completed." }, JsonRequestBehavior.AllowGet);
        }
    }
}
