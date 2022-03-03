using DM.Application.Contracts.DiscountCode.Queries;

namespace DM.Infrastructure.Shared.Validators.DiscountCode;

public class GetDiscountCodeDetailsQueryValidator : AbstractValidator<GetDiscountCodeDetailsQuery>
{
    public GetDiscountCodeDetailsQueryValidator()
    {
        RuleFor(p => p.Id)
            .RequiredValidator("شناسه تخفیف");
    }
}