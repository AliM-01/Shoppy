using DM.Application.Contracts.CustomerDiscount.DTOs;

namespace DM.Application.Contracts.CustomerDiscount.Queries;

public class CheckProductHasCustomerDiscountQuery : IRequest<Response<CheckProductHasCustomerDiscountResponseDto>>
{
    public CheckProductHasCustomerDiscountQuery(long productId)
    {
        ProductId = productId;
    }

    public long ProductId { get; set; }
}