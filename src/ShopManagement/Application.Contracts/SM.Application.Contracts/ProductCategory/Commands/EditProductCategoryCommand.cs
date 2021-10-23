using _0_Framework.Application.Wrappers;
using MediatR;
using SM.Application.Contracts.ProductCategory.Models;

namespace SM.Application.Contracts.ProductCategory.Commands
{
    public class EditProductCategoryCommand : IRequest<Response<string>>
    {
        public EditProductCategoryCommand(EditProductCategoryDto productCategory)
        {
            ProductCategory = productCategory;
        }

        public EditProductCategoryDto ProductCategory { get; set; }
    }
}