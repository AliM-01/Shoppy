namespace DM.Application.Contracts.CustomerDiscount.Commands;
public class RemoveCustomerDiscountCommand : IRequest<Response<string>>
{
    public RemoveCustomerDiscountCommand(long customerDiscountId)
    {
        CustomerDiscountId = customerDiscountId;
    }

    public long CustomerDiscountId { get; set; }
}