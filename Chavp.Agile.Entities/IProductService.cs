﻿using Chavp.Agile.Entities.Attributes;
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
            var models = _productRepository.Filter(null, out total, pageIndex, limit).ToList();
            var result = new ProductResult
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

    public interface IProductService
    {
        ProductResult GetProducts(int start, int limit);

        bool Add(ProductDto p);

        bool Save(ProductDto p);

        bool Remove(string codeName);
    }

    public class ProductResult
    {
        public int Total { get; set; }
        public IList<ProductDto> Products { get; set; }
    }

    public class ProductDto
    {
        public string CodeName { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string Slogan { get; set; }
        public string StatusDisplay { get; set; }
    }
}
