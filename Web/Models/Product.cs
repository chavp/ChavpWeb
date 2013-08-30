using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Product
    {
        public string Brand { get; set; }
        public string Name { get; set; }

        public IList<Feature> Features { get; set; }
    }
}