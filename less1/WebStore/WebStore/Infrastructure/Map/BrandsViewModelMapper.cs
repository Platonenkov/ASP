﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;

namespace WebStore.Infrastructure.Map
{
    public static class BrandsViewModelMapper
    {
        public static void CopyTo(this BrandViewModel Model, Brand brand)
        {
            brand.Name = Model.Name;
            brand.Order = Model.Order;
        }

        public static Brand Create(this BrandViewModel model)
        {
            var brand = new Brand();
            model.CopyTo(brand);
            return brand;
        }

        public static void CopyTo(this Brand brand, BrandViewModel model, int ProductsCount = 0)
        {
            model.Id = brand.Id;
            model.Name = brand.Name;
            model.Order = brand.Order;
            model.ProductsCount = ProductsCount;
        }

        public static BrandViewModel CreateModel(this Brand brand, int ProductsCount = 0)
        {
            var model = new BrandViewModel();
            brand.CopyTo(model, ProductsCount);
            return model;
        }
    }
}