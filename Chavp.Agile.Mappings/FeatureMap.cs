using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chavp.Agile.Mappings
{
    using Chavp.Agile.Entities;
    using FluentNHibernate.Mapping;

    public class FeatureMap
        : EntityMap<Feature>
    {
        public FeatureMap()
        {
            Table("Features");

            Map(x => x.Name).Unique();
            Map(x => x.Description);

            Map(x => x.Action);
            Map(x => x.Result);
            Map(x => x.Object);

            HasManyToMany<Product>(x => x.Products)
                .Cascade.All()
                .Table("ProductsFeatures");
        }
    }
}
