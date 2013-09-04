using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chavp.Agile.Mappings
{
    using Chavp.Agile.Entities;

    public class FeatureRepository
        : Repository<Feature, string>, IFeatureRepository
    {
    }
}
