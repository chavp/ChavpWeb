using System;
using System.Collections.Generic;
using System.Linq;
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
            var products = new List<Product>();
            products.Add(new Product
            {
                Brand = "Chavp",
                CodeName = "P-MOCKUP",
                Name = "Hello, World.",
                Slogan = "It's simple.",
                Status = EProductStatus.Concept,
            });
            products.Add(new Product
            {
                Brand = "Chavp",
                CodeName = "P-CAL",
                Name = "ChavpCal.",
                Slogan = "Calculate Baby",
                Status = EProductStatus.Concept,
            });

            return View(products);
        }

    }
}
