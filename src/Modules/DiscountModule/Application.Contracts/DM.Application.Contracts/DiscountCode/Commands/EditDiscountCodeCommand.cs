using DM.Application.Contracts.DiscountCode.DTOs;

namespace DM.Application.Contracts.DiscountCode.Commands;

public record EditDiscountCodeCommand
    (EditDiscountCodeDto DiscountCode) : IRequest<Response<string>>;