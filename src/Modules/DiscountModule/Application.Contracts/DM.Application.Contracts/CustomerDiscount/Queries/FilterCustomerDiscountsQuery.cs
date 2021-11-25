using DM.Application.Contracts.CustomerDiscount.DTOs;

namespace DM.Application.Contracts.CustomerDiscount.Queries;

public class FilterCustomerDiscountsQuery : IRequest<Response<FilterCustomerDiscountDto>>
{
    public FilterCustomerDiscountsQuery(FilterCustomerDiscountDto filter)
    {
        Filter = filter;
    }

    public FilterCustomerDiscountDto Filter { get; set; }
}