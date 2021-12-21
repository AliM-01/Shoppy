using DM.Application.Contracts.CustomerDiscount.DTOs;

namespace DM.Application.Contracts.CustomerDiscount.Queries;

public record CheckProductHasCustomerDiscountQuery
    (long ProductId) : IRequest<Response<CheckProductHasCustomerDiscountResponseDto>>;