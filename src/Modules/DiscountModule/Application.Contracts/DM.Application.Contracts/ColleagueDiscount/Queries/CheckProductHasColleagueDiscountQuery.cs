namespace DM.Application.Contracts.ColleagueDiscount.Queries;

public class CheckProductHasColleagueDiscountQuery : IRequest<Response<object>>
{
    public CheckProductHasColleagueDiscountQuery(long productId)
    {
        ProductId = productId;
    }

    public long ProductId { get; set; }
}