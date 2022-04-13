namespace DM.Application.Contracts.ProductDiscount.Commands;

public record RemoveProductDiscountCommand
    (string ProductDiscountId) : IRequest<ApiResult>;