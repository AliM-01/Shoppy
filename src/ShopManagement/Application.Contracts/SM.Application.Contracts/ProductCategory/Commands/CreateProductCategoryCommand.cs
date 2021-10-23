using _0_Framework.Application.Wrappers;
using MediatR;
using SM.Application.Contracts.ProductCategory.Models;

namespace SM.Application.Contracts.ProductCategory.Commands
{
    public class CreateProductCategoryCommand : IRequest<Response<string>>
    {
        public CreateProductCategoryCommand(CreateProductCategoryDto productCategory)
        {
            ProductCategory = productCategory;
        }

        public CreateProductCategoryDto ProductCategory { get; set; }
    }
}