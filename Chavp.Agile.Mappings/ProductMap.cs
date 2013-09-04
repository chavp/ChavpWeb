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
        : ClassMap<Product>
    {
        public ProductMap()
        {
            Table("Products");

            Id(x => x.CodeName);
            OptimisticLock.Version();
            Version(x => x.Version)
                .CustomType("Timestamp");

            Map(x => x.Brand);
            Map(x => x.Name);
            Map(x => x.Slogan);
            Map(x => x.Status);

            HasManyToMany<Feature>(x => x.Features)
                .Cascade.All()
                .Table("ProductsFeatures");
        }
    }
}
