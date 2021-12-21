using DM.Application.Contracts.ColleagueDiscount.DTOs;

namespace DM.Application.Contracts.ColleagueDiscount.Queries;

public record GetColleagueDiscountDetailsQuery(long Id) : IRequest<Response<EditColleagueDiscountDto>>;