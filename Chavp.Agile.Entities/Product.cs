﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chavp.Agile.Entities
{
    public class Product
        : Entity
    {
        public Product()
        {
            Features = new List<Feature>();
        }

        public virtual string Brand { get; set; }
        public virtual string Name { get; set; }
        public virtual string Slogan { get; set; }
        public virtual EProductStatus Status { get; set; }

        public virtual IList<Feature> Features { get; set; }

    }
}
