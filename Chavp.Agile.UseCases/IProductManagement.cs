using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chavp.Agile.UseCases
{
    using Chavp.Agile.UseCases.Data;

    public interface IProductManagement
    {
        ProductResult GetProducts(int start, int limit);

        bool Add(ProductDto p);

        bool Save(ProductDto p);

        bool Remove(string codeName);
    }
}
