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
    using Chavp.Agile.UseCases;
    using Chavp.Agile.UseCases.Data;
    using Web.Interceptors;

    [Authorize]
    public class ProductsController : Controller
    {
        IProductManagement _productManagement;
        public ProductsController(IProductManagement productManagement)
        {
            _productManagement = productManagement;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetProducts(int index, int limit)
        {
            var result = _productManagement.GetProducts(index, limit);

            return Json(new { success = true, data = result.Products, message = "", total = result.Total }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(ProductDto p)
        {
            //Thread.Sleep(1500);

            if (_productManagement.Add(p))
            {
                return Json(new { success = true, data = p, message = string.Format("Adding {0} completed.", p.CodeName) }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "Already have a code name." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Save(ProductDto p)
        {
           // Thread.Sleep(1500);
            if (_productManagement.Save(p))
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
           // Thread.Sleep(1500);
            if (_productManagement.Remove(codeName))
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
