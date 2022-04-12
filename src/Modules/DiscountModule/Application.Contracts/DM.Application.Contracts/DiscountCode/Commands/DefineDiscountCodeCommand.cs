using DM.Application.Contracts.DiscountCode.DTOs;

namespace DM.Application.Contracts.DiscountCode.Commands;

public record DefineDiscountCodeCommand
    (DefineDiscountCodeDto DiscountCode) : IRequest<ApiResult>;