using DM.Application.Contracts.ColleagueDiscount.DTOs;

namespace DM.Application.Contracts.ColleagueDiscount.Queries;

public record FilterColleagueDiscountsQuery
    (FilterColleagueDiscountDto Filter) : IRequest<Response<FilterColleagueDiscountDto>>;