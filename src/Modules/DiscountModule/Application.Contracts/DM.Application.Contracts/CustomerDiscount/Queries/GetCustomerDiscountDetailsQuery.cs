using DM.Application.Contracts.CustomerDiscount.DTOs;

namespace DM.Application.Contracts.CustomerDiscount.Queries;

public record GetCustomerDiscountDetailsQuery
    (long Id) : IRequest<Response<EditCustomerDiscountDto>>;