using DM.Application.Contracts.DiscountCode.DTOs;

namespace DM.Application.Contracts.DiscountCode.Queries;

public record GetDiscountCodeDetailsQuery(string Id) : IRequest<EditDiscountCodeDto>;