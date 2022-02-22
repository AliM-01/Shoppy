using DM.Application.Contracts.ProductDiscount.Commands;

namespace DM.Infrastructure.Shared.Validators.ProductDiscount;

public class EditProductDiscountCommandValidator : AbstractValidator<EditProductDiscountCommand>
{
    public EditProductDiscountCommandValidator()
    {
        RuleFor(p => p.ProductDiscount.Id)
            .RequiredValidator("شناسه تخفیف");

        RuleFor(p => p.ProductDiscount.ProductId)
            .RangeValidator("شناسه محصول", 1, 100000);

        RuleFor(p => p.ProductDiscount.Rate)
            .RangeValidator("درصد", 1, 100);
    }
}