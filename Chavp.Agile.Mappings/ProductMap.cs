using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chavp.Agile.Mappings
{
    using Chavp.Agile.Entities;
    using FluentNHibernate.Mapping;

    public class ProductMap
        : EntityMap<Product>
    {
        public ProductMap()
        {
            Table("Products");

            Map(x => x.Name).UniqueKey("namespace");
            Map(x => x.Brand).UniqueKey("namespace");
            Map(x => x.Slogan);
            Map(x => x.Status);

            HasManyToMany<Feature>(x => x.Features)
                .Cascade.All()
                .Table("ProductsFeatures");
        }
    }
}
