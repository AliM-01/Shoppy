using SM.Application.Contracts.ProductCategory.DTOs;

namespace SM.Application.Contracts.ProductCategory.Commands;

public class CreateProductCategoryCommand : IRequest<Response<string>>
{
    public CreateProductCategoryCommand(CreateProductCategoryDto productCategory)
    {
        ProductCategory = productCategory;
    }

    public CreateProductCategoryDto ProductCategory { get; set; }
}