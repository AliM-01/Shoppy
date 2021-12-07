using DM.Application.Contracts.ColleagueDiscount.DTOs;

namespace DM.Application.Contracts.ColleagueDiscount.Queries;
public class GetColleagueDiscountDetailsQuery : IRequest<Response<EditColleagueDiscountDto>>
{
    public GetColleagueDiscountDetailsQuery(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}