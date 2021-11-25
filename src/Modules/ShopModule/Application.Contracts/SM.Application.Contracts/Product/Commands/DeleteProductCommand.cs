namespace SM.Application.Contracts.Product.Commands;
public class DeleteProductCommand : IRequest<Response<string>>
{
    public DeleteProductCommand(long productId)
    {
        ProductId = productId;
    }

    public long ProductId { get; set; }
}