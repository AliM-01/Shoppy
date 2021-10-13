using System.Collections.Generic;
using System.Threading.Tasks;
using SM.Application.Contracts.ProductCategory.Models;

namespace SM.Application.Contracts.ProductCategory.Interfaces
{
    public interface IProductCategoryApplication
    {
        Task<List<ProductCategoryViewModel>> Filter(FilterProductCategoryModel filter);

        Task<Domain.ProductCategoryAgg.ProductCategory> GetDetails(long id);

        Task Create(CreateProductCategoryModel command);

        Task Edit(EditProductCategoryModel command);
    }
}