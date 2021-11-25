namespace SM.Application.Contracts.Product.Queries;

public class UpdateProductNotInStockCommand : IRequest<Response<string>>
{
    public UpdateProductNotInStockCommand(long productId)
    {
        ProductId = productId;
    }

    public long ProductId { get; set; }
}
