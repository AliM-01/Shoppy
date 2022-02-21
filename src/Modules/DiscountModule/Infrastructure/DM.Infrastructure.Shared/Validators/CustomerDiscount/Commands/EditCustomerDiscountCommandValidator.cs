using DM.Application.Contracts.CustomerDiscount.Commands;

namespace DM.Infrastructure.Shared.Validators.CustomerDiscount;

public class EditCustomerDiscountCommandValidator : AbstractValidator<EditCustomerDiscountCommand>
{
    public EditCustomerDiscountCommandValidator()
    {
        RuleFor(p => p.CustomerDiscount.Id)
            .RequiredValidator("شناسه تخفیف");

        RuleFor(p => p.CustomerDiscount.ProductId)
            .RequiredValidator("شناسه محصول");

        RuleFor(p => p.CustomerDiscount.Rate)
            .RangeValidator("درصد", 1, 100);
    }
}