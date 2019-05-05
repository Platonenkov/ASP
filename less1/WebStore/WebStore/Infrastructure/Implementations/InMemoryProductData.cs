﻿using System.Collections.Generic;
using System.Linq;
using WebStore.Data;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Servcies;

namespace WebStore.Infrastructure.Implementations
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Brand> GetBrands() => TestData.Brands;

        public Product GetProductById(int id) => TestData.Products.FirstOrDefault(product => product.Id == id);

        public IEnumerable<Product> GetProducts(ProductFilter Filter)
        {
            IEnumerable<Product> products = TestData.Products;
            if (Filter is null) return products;
            if (Filter.BrandId != null)
                products = products.Where(product => product.BrandId == Filter.BrandId);
            if (Filter.SectionId != null)
                products = products.Where(product => product.SectionId == Filter.SectionId);
            return products;
        }

        public IEnumerable<Section> GetSections() => TestData.Sections;

    }
}
