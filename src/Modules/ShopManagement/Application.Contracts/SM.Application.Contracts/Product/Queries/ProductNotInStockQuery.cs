namespace SM.Application.Contracts.Product.Queries;

public class ProductNotInStockQuery : IRequest<Response<string>>
{
    public ProductNotInStockQuery(long productId)
    {
        ProductId = productId;
    }

    public long ProductId { get; set; }
}
