using DM.Application.Contracts.CustomerDiscount.Queries;

namespace DM.Infrastructure.Shared.Validators.CustomerDiscount;

public class GetCustomerDiscountDetailsQueryValidator : AbstractValidator<GetCustomerDiscountDetailsQuery>
{
    public GetCustomerDiscountDetailsQueryValidator()
    {
        RuleFor(p => p.Id)
            .RequiredValidator("شناسه تخفیف");
    }
}