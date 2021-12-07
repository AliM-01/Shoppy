namespace DM.Application.Contracts.CustomerDiscount.Queries;

public class CheckProductHasCustomerDiscountQuery : IRequest<Response<object>>
{
    public CheckProductHasCustomerDiscountQuery(long productId)
    {
        ProductId = productId;
    }

    public long ProductId { get; set; }
}