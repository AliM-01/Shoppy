using DM.Application.Contracts.CustomerDiscount.DTOs;

namespace DM.Application.Contracts.CustomerDiscount.Commands;

public class CreateCustomerDiscountCommand : IRequest<Response<string>>
{
    public CreateCustomerDiscountCommand(CreateCustomerDiscountDto customerDiscount)
    {
        CustomerDiscount = customerDiscount;
    }

    public CreateCustomerDiscountDto CustomerDiscount { get; set; }
}