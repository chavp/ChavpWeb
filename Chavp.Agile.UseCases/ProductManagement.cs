using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chavp.Agile.UseCases
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Chavp.Agile.Entities;
    using Chavp.Agile.Entities.Attributes;
    using Chavp.Agile.UseCases.Data;

    /// <summary>
    /// http://blog.8thlight.com/uncle-bob/2012/08/13/the-clean-architecture.html
    /// </summary>
    public class ProductManagement
        : IProductManagement
    {
        static ProductManagement()
        {
            AutoMapperConfiguration.Configure();
        }

        IProductRepository _productRepository;
        public ProductManagement(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [UnitOfWork]
        public ProductResult GetProducts(int pageIndex, int limit)
        {
            int total = _productRepository.Count;
            var models = _productRepository
                .All()
                .OrderByDescending(p => p.Created)
                .Skip(pageIndex * limit)
                .Take(limit)
                .Project<Product>()
                .To<ProductDto>()
                .ToList();

            
            var result = new ProductResult
            {
                Total = total,
                Products = models,
            };

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
                //var newProduct = new Product(p.CodeName)
                //{
                //    Brand = p.Brand,
                //    Name = p.Name,
                //    Slogan = p.Slogan,
                //    Status = EProductStatus.Concept
                //};

                var newProduct = Mapper.Map<ProductDto, Product>(p);
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
