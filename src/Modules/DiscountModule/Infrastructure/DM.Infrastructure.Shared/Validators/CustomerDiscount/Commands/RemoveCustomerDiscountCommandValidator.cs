using DM.Application.Contracts.CustomerDiscount.Commands;

namespace DM.Infrastructure.Shared.Validators.CustomerDiscount;

public class RemoveCustomerDiscountCommandValidator : AbstractValidator<RemoveCustomerDiscountCommand>
{
    public RemoveCustomerDiscountCommandValidator()
    {
        RuleFor(p => p.CustomerDiscountId)
            .RequiredValidator("شناسه تخفیف");
    }
}