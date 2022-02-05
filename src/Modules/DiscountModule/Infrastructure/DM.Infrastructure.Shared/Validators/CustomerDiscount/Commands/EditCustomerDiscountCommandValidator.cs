using DM.Application.Contracts.CustomerDiscount.Commands;

namespace DM.Infrastructure.Shared.Validators.CustomerDiscount;

public class EditCustomerDiscountCommandValidator : AbstractValidator<EditCustomerDiscountCommand>
{
    public EditCustomerDiscountCommandValidator()
    {
        RuleFor(p => p.CustomerDiscount.Id)
            .RangeValidator("شناسه تخفیف", 1, 100000);

        RuleFor(p => p.CustomerDiscount.ProductId)
            .RangeValidator("شناسه محصول", 1, 100000);

        RuleFor(p => p.CustomerDiscount.Rate)
            .RangeValidator("درصد", 1, 100);
    }
}