using DM.Application.Contracts.ProductDiscount.Commands;

namespace DM.Infrastructure.Shared.Validators.ProductDiscount;

public class RemoveProductDiscountCommandValidator : AbstractValidator<RemoveProductDiscountCommand>
{
    public RemoveProductDiscountCommandValidator()
    {
        RuleFor(p => p.ProductDiscountId)
            .RequiredValidator("شناسه تخفیف");
    }
}