using SM.Application.Contracts.ProductCategory.DTOs;

namespace SM.Application.Contracts.ProductCategory.Commands;

public class EditProductCategoryCommand : IRequest<Response<string>>
{
    public EditProductCategoryCommand(EditProductCategoryDto productCategory)
    {
        ProductCategory = productCategory;
    }

    public EditProductCategoryDto ProductCategory { get; set; }
}