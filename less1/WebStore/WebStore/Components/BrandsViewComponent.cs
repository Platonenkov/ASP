using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Components
{
    public class BrandsViewComponent: ViewComponent
    {
        private readonly IProductData _ProductData;

        public BrandsViewComponent (IProductData productData)
        {
            _ProductData = productData;
        }
        public IViewComponentResult Invoke()
        {
            var brands = GetBrands();
            return View(brands);
        }

        private IEnumerable<BrandViewModel> GetBrands()
        {
            var brands = _ProductData.GetBrands();

            return brands.Select(brand => new BrandViewModel
            {
                Id = brand.Id,
                Name = brand.Name,
                Order = brand.Order
            });
        }
    }
}
