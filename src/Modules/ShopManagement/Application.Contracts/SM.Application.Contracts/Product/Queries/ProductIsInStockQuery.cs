namespace SM.Application.Contracts.Product.Queries;
public class ProductIsInStockQuery : IRequest<Response<string>>
{
    public ProductIsInStockQuery(long productId)
    {
        ProductId = productId;
    }

    public long ProductId { get; set; }
}
