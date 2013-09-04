using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    using Castle.Core;
    using Chavp.Agile.Entities;
    using Chavp.Agile.Entities.Attributes;
    using Chavp.Agile.Mappings;
    using Web.Interceptors;

    [Authorize]
    public class ProductsController : Controller
    {
        IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetProducts(int index, int limit)
        {
            var result = _productService.GetProducts(index, limit);

            return Json(new { success = true, data = result.Products, message = "", total = result.Total }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(Product p)
        {
            Thread.Sleep(1500);

            if (_productService.Add(p))
            {
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
            if (_productService.Save(p))
            {
                return Json(new { success = true, data = p, message = "Save completed." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "Ivalid Product in this system." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Remove(string codeName)
        {
            Thread.Sleep(1500);
            if (_productService.Remove(codeName))
            {
                return Json(new { success = true, data = codeName, message = "Remove completed." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, data = codeName, message = "Ivalid Product in this system." }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
