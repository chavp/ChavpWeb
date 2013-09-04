using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chavp.Agile.UseCases.Data
{
    public class FeatureResult
    {
        public int Total { get; set; }
        public IList<FeatureDto> Features { get; set; }
    }
}
