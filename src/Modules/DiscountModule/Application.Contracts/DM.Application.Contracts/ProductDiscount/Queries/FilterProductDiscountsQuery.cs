using DM.Application.Contracts.ProductDiscount.DTOs;

namespace DM.Application.Contracts.ProductDiscount.Queries;

public record FilterProductDiscountsQuery
    (FilterProductDiscountDto Filter) : IRequest<Response<FilterProductDiscountDto>>;