using DM.Application.Contracts.CustomerDiscount.DTOs;

namespace DM.Application.Contracts.CustomerDiscount.Queries;
public class GetCustomerDiscountDetailsQuery : IRequest<Response<EditCustomerDiscountDto>>
{
    public GetCustomerDiscountDetailsQuery(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}