using DM.Application.Contracts.ProductDiscount.Commands;

namespace DM.Infrastructure.Shared.Validators.ProductDiscount;

public class DefineProductDiscountCommandValidator : AbstractValidator<DefineProductDiscountCommand>
{
    public DefineProductDiscountCommandValidator()
    {
        RuleFor(p => p.ProductDiscount.ProductId)
            .RangeValidator("شناسه محصول", 1, 100000);

        RuleFor(p => p.ProductDiscount.StartDate)
            .RequiredValidator("تاریخ شروع");

        RuleFor(p => p.ProductDiscount.EndDate)
            .RequiredValidator("تاریخ پایان");

        RuleFor(p => p.ProductDiscount.Description)
            .RequiredValidator("توضیحات")
            .MaxLengthValidator("توضیحات", 250);

        RuleFor(p => p.ProductDiscount.Rate)
            .RangeValidator("درصد", 1, 100);
    }
}