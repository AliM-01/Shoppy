using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application.Wrappers;
using SM.Application.Contracts.ProductCategory.Models;

namespace SM.Application.Contracts.ProductCategory.Interfaces
{
    public interface IProductCategoryApplication
    {
        Task<OperationResult<List<ProductCategoryViewModel>>> GetAll();

        Task<OperationResult<Domain.ProductCategory.ProductCategory>> GetDetails(long id);

        Task<OperationResult> Create(CreateProductCategoryModel command);

        Task<OperationResult> Edit(EditProductCategoryModel command);
    }
}