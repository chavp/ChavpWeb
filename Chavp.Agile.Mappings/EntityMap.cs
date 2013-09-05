using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chavp.Agile.Mappings
{
    using FluentNHibernate.Data;
    using FluentNHibernate.Mapping;

    public abstract class EntityMap<T> 
        : ClassMap<T> where T : Chavp.Agile.Entities.Entity
    {
        public EntityMap()
        {
            Id(x => x.Id);
            OptimisticLock.Version();
            Version(x => x.Version)
                .CustomType("Timestamp");

            Map(x => x.Created).Not.Nullable();
        }
    }
}
