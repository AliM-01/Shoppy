using SM.Application.Contracts.Product.DTOs;

namespace SM.Application.Contracts.ProductCategory.Commands;

public class EditProductCommand : IRequest<Response<string>>
{
    public EditProductCommand(EditProductDto product)
    {
        Product = product;
    }

    public EditProductDto Product { get; set; }
}