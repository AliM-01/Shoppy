using SM.Application.Contracts.Product.DTOs;

namespace SM.Application.Contracts.Product.Commands;

public class CreateProductCommand : IRequest<Response<string>>
{
    public CreateProductCommand(CreateProductDto product)
    {
        Product = product;
    }

    public CreateProductDto Product { get; set; }
}