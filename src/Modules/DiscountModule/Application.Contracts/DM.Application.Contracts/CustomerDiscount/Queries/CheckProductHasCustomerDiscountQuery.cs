using DM.Application.Contracts.CustomerDiscount.DTOs;

namespace DM.Application.Contracts.CustomerDiscount.Queries;

public record CheckProductHasCustomerDiscountQuery
    (string ProductId) : IRequest<Response<CheckProductHasCustomerDiscountResponseDto>>;