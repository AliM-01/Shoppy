using System.Collections.Generic;
using System.Threading.Tasks;

namespace SM.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository
    {
        Task<List<ProductCategory>> GetAll();

        Task<ProductCategory> GetById(long id);

        Task Create(ProductCategory productCategory);
    }
}