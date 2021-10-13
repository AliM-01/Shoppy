using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SM.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository
    {
        Task<List<ProductCategory>> GetAllAsync();

        Task<ProductCategory> GetByIdAsync(long id);

        Task CreateAsync(ProductCategory productCategory);

        Task<bool> ExistsAsync(Expression<Func<ProductCategory, bool>> expression);

        Task SaveChangesAsync();
    }
}