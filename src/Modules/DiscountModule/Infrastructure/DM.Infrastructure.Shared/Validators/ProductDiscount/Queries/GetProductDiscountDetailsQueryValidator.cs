using DM.Application.Contracts.ProductDiscount.Queries;

namespace DM.Infrastructure.Shared.Validators.ProductDiscount;

public class GetProductDiscountDetailsQueryValidator : AbstractValidator<GetProductDiscountDetailsQuery>
{
    public GetProductDiscountDetailsQueryValidator()
    {
        RuleFor(p => p.Id)
            .RequiredValidator("شناسه تخفیف");
    }
}