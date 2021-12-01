using DM.Application.Contracts.CustomerDiscount.DTOs;

namespace DM.Application.Contracts.CustomerDiscount.Commands;

public class DefineCustomerDiscountCommand : IRequest<Response<string>>
{
    public DefineCustomerDiscountCommand(DefineCustomerDiscountDto customerDiscount)
    {
        CustomerDiscount = customerDiscount;
    }

    public DefineCustomerDiscountDto CustomerDiscount { get; set; }
}