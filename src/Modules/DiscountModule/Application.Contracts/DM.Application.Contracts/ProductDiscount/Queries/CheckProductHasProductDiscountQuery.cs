using DM.Application.Contracts.ProductDiscount.DTOs;

namespace DM.Application.Contracts.ProductDiscount.Queries;

public record CheckProductHasProductDiscountQuery
    (long ProductId) : IRequest<Response<CheckProductHasProductDiscountResponseDto>>;