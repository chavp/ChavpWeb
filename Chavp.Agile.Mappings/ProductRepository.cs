using Chavp.Agile.Entities;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chavp.Agile.Mappings
{
    public class ProductRepository
        : Repository<Product, string>, IProductRepository
    {
        
    }
}
