namespace DM.Application.Contracts.CustomerDiscount.Commands;

public record RemoveCustomerDiscountCommand
    (long CustomerDiscountId) : IRequest<Response<string>>;