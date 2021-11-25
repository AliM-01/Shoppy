using SM.Application.Contracts.Product.DTOs;

namespace SM.Application.Contracts.Product.Commands;

public class EditProductCommand : IRequest<Response<string>>
{
    public EditProductCommand(EditProductDto product)
    {
        Product = product;
    }

    public EditProductDto Product { get; set; }
}