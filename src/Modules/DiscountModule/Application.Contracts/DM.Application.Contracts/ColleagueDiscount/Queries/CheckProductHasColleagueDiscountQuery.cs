using DM.Application.Contracts.ColleagueDiscount.DTOs;

namespace DM.Application.Contracts.ColleagueDiscount.Queries;

public class CheckProductHasColleagueDiscountQuery : IRequest<Response<CheckProductHasColleagueDiscountResponseDto>>
{
    public CheckProductHasColleagueDiscountQuery(long productId)
    {
        ProductId = productId;
    }

    public long ProductId { get; set; }
}