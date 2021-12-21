using DM.Application.Contracts.CustomerDiscount.DTOs;

namespace DM.Application.Contracts.CustomerDiscount.Commands;

public record EditCustomerDiscountCommand
    (EditCustomerDiscountDto CustomerDiscount) : IRequest<Response<string>>;