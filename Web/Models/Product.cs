using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Product
    {
        public string CodeName { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string Slogan { get; set; }
        public EProductStatus Status { get; set; }
        public string StatusDisplay
        {
            get
            {
                return Status.ToString();
            }
        }

        public IList<Feature> Features { get; set; }
    }
}