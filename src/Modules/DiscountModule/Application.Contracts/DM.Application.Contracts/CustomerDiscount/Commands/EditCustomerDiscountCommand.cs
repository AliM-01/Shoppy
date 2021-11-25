using DM.Application.Contracts.CustomerDiscount.DTOs;

namespace DM.Application.Contracts.CustomerDiscount.Commands;

public class EditCustomerDiscountCommand : IRequest<Response<string>>
{
    public EditCustomerDiscountCommand(EditCustomerDiscountDto customerDiscount)
    {
        CustomerDiscount = customerDiscount;
    }

    public EditCustomerDiscountDto CustomerDiscount { get; set; }
}