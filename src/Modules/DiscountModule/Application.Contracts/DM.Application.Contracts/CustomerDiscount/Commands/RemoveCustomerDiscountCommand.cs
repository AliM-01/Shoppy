namespace DM.Application.Contracts.CustomerDiscount.Commands;

public record RemoveCustomerDiscountCommand
    (string CustomerDiscountId) : IRequest<Response<string>>;