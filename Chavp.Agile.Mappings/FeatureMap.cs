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
        : ClassMap<Feature>
    {
        public FeatureMap()
        {
            Table("Features");

            Id(x => x.CodeName);
            Version(x => x.Version)
                .CustomType("Timestamp");

            Map(x => x.Created).Not.Nullable();

            Map(x => x.Name);
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
