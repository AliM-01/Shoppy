using DM.Application.Contracts.CustomerDiscount.DTOs;

namespace DM.Application.Contracts.CustomerDiscount.Queries;

public record FilterCustomerDiscountsQuery
    (FilterCustomerDiscountDto Filter) : IRequest<Response<FilterCustomerDiscountDto>>;