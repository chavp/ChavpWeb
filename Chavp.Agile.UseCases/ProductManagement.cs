using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chavp.Agile.UseCases
{
    using Chavp.Agile.Entities;
    using Chavp.Agile.Entities.Attributes;
    using Chavp.Agile.UseCases.Data;

    /// <summary>
    /// http://blog.8thlight.com/uncle-bob/2012/08/13/the-clean-architecture.html
    /// </summary>
    public class ProductManagement
        : IProductManagement
    {
        IProductRepository _productRepository;
        public ProductManagement(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [UnitOfWork]
        public ProductDisplay GetProducts(int pageIndex, int limit)
        {
            int total;
            var models = _productRepository.Filter(null, out total, pageIndex, limit).ToList();
            var result = new ProductDisplay
            {
                Total = total,
                Products = new List<ProductDto>(),
            };
            models.ForEach(m =>
            {
                result.Products.Add(new ProductDto
                {
                    CodeName = m.CodeName,
                    Brand = m.Brand,
                    Name = m.Name,
                    Slogan = m.Slogan,
                    StatusDisplay = m.Status.ToString()
                });
            });
            return result;
        }

        [UnitOfWork]
        public bool Add(ProductDto p)
        {
            var uniqQuery = from x in _productRepository.All()
                            where x.CodeName == p.CodeName
                            select x;
            var m = uniqQuery.FirstOrDefault();
            if (m == null)
            {
                var newProduct = new Product(p.CodeName)
                {
                    Brand = p.Brand,
                    Name = p.Name,
                    Slogan = p.Slogan,
                    Status = EProductStatus.Concept
                };
                _productRepository.Save(newProduct);
                return true;
            }
            return false;
        }

        [UnitOfWork]
        public bool Save(ProductDto p)
        {
            var uniqQuery = from x in _productRepository.All()
                            where x.CodeName == p.CodeName
                            select x;
            var m = uniqQuery.FirstOrDefault();

            if (m != null)
            {
                m.Brand = p.Brand;
                m.Name = p.Name;
                m.Slogan = p.Slogan;
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
}
