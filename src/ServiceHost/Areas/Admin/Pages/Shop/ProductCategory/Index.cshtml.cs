using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SM.Application.Contracts.ProductCategory.Interfaces;
using SM.Application.Contracts.ProductCategory.Models;

namespace ServiceHost.Areas.Admin.Pages.Shop.ProductCategory
{
    public class IndexModel : PageModel
    {
        public List<ProductCategoryViewModel> ProductCategories;

        private readonly IProductCategoryApplication _productCategoryApplication;

        public IndexModel(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }

        public async Task OnGet(FilterProductCategoryModel filter)
        {
            var filteredResult = await _productCategoryApplication.Filter(filter);

            var t = new List<ProductCategoryViewModel>();

            for (int i = 0; i < 25; i++)
            {
                t.Add(new ProductCategoryViewModel
                {
                    Title = i.ToString(),
                    CreationDate = "re",
                    Id = i,
                    ImagePath = "fd",
                    ProductsCount = 0
                });
            }

            ProductCategories = t;
        }
    }
}
