﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Map
{
    public static class BrandsViewModelMapper
    {
        public static void CopyTo(this Brand brand, BrandViewModel model, int ProductsCount = 0 )
        {
            model.Id = brand.Id;
            model.Name = brand.Name;
            model.Order = brand.Order;
            model.ProductsCount = ProductsCount;
        }

        public static BrandViewModel CreateViewModel(this Brand brand, int ProductsCount = 0)
        {
            var model = new BrandViewModel();
            brand.CopyTo(model, ProductsCount);
            return model;
        }

        public static void CopyTo(this BrandViewModel model, Brand brand)
        {
            brand.Name = model.Name;
            brand.Order = model.Order;
        }

        public static Brand Create(this BrandViewModel model)
        {
            var brand = new Brand();
            model.CopyTo(brand);
            return brand;
        }

    }
}
