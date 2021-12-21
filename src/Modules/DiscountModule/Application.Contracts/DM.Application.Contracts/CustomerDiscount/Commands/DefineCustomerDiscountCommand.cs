using DM.Application.Contracts.CustomerDiscount.DTOs;

namespace DM.Application.Contracts.CustomerDiscount.Commands;

public record DefineCustomerDiscountCommand
    (DefineCustomerDiscountDto CustomerDiscount) : IRequest<Response<string>>;