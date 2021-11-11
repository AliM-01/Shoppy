namespace SM.Application.Contracts.Product.Queries;
public class UpdateProductIsInStockCommand : IRequest<Response<string>>
{
    public UpdateProductIsInStockCommand(long productId)
    {
        ProductId = productId;
    }

    public long ProductId { get; set; }
}
