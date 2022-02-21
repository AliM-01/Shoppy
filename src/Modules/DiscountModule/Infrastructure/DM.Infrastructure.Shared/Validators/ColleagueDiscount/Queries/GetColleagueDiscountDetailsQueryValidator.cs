using DM.Application.Contracts.ColleagueDiscount.Queries;

namespace DM.Infrastructure.Shared.Validators.ColleagueDiscount;

public class GetColleagueDiscountDetailsQueryValidator : AbstractValidator<GetColleagueDiscountDetailsQuery>
{
    public GetColleagueDiscountDetailsQueryValidator()
    {
        RuleFor(p => p.Id)
            .RequiredValidator("شناسه تخفیف");
    }
}