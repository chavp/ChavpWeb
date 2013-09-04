using Chavp.Agile.Entities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chavp.Agile.Entities
{
    public class ProductService
        : IProductService
    {
        IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [UnitOfWork]
        public ProductResult GetProducts(int pageIndex, int limit)
        {
            int total;
            var result = _productRepository.Filter(null, out total, pageIndex, limit).ToList();
            return new ProductResult
            {
                Total = total,
                Products = result,
            };
        }

        [UnitOfWork]
        public bool Add(Product p)
        {
            var uniqQuery = from x in _productRepository.All()
                            where x.CodeName == p.CodeName
                            select x;
            if (uniqQuery.Count() == 0)
            {
                p.Status = EProductStatus.Concept;
                _productRepository.Save(p);
                return true;
            }
            return false;
        }

        [UnitOfWork]
        public bool Save(Product p)
        {
            var uniqQuery = from x in _productRepository.All()
                            where x.CodeName == p.CodeName
                            select x;
            if (uniqQuery.Count() > 0)
            {
                var oldProduc = uniqQuery.Single();
                oldProduc.Brand = p.Brand;
                oldProduc.Name = p.Name;
                oldProduc.Slogan = p.Slogan;
                return true;
            }
            return false;
        }

        [UnitOfWork]
        public bool Remove(string codeName)
        {
            var uniqQuery = from x in _productRepository.All()
                            where x.CodeName == codeName
                            select x;
            if (uniqQuery.Count() > 0)
            {
                var oldProduc = uniqQuery.Single();
                _productRepository.Delete(oldProduc);
                return true;
            }
            return false;
        }
    }

    public interface IProductService
    {
        ProductResult GetProducts(int start, int limit);

        bool Add(Product p);

        bool Save(Product p);

        bool Remove(string codeName);
    }

    public class ProductResult
    {
        public int Total { get; set; }
        public IList<Product> Products { get; set; }
    }
}
