using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chavp.Agile.UseCases.Data
{
    public class ProductResult
    {
        public int Total { get; set; }
        public IList<ProductDto> Products { get; set; }
    }
}
