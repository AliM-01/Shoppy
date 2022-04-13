namespace DM.Application.Contracts.DiscountCode.Commands;

public record RemoveDiscountCodeCommand
    (string DiscountCodeId) : IRequest<ApiResult>;