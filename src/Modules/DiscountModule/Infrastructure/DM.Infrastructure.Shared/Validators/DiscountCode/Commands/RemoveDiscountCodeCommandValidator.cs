using DM.Application.Contracts.DiscountCode.Commands;

namespace DM.Infrastructure.Shared.Validators.DiscountCode;

public class RemoveDiscountCodeCommandValidator : AbstractValidator<RemoveDiscountCodeCommand>
{
    public RemoveDiscountCodeCommandValidator()
    {
        RuleFor(p => p.DiscountCodeId)
            .RequiredValidator("شناسه تخفیف");
    }
}