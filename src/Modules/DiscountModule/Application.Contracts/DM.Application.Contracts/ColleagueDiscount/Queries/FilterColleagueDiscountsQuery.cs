using DM.Application.Contracts.ColleagueDiscount.DTOs;

namespace DM.Application.Contracts.ColleagueDiscount.Queries;

public class FilterColleagueDiscountsQuery : IRequest<Response<FilterColleagueDiscountDto>>
{
    public FilterColleagueDiscountsQuery(FilterColleagueDiscountDto filter)
    {
        Filter = filter;
    }

    public FilterColleagueDiscountDto Filter { get; set; }
}