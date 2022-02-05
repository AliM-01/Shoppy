using DM.Application.Contracts.CustomerDiscount.Commands;

namespace DM.Infrastructure.Shared.Validators.CustomerDiscount;

public class DefineCustomerDiscountCommandValidator : AbstractValidator<DefineCustomerDiscountCommand>
{
    public DefineCustomerDiscountCommandValidator()
    {
        RuleFor(p => p.CustomerDiscount.ProductId)
            .RangeValidator("شناسه محصول", 1, 100000);

        RuleFor(p => p.CustomerDiscount.StartDate)
            .RequiredValidator("تاریخ شروع");

        RuleFor(p => p.CustomerDiscount.EndDate)
            .RequiredValidator("تاریخ پایان");

        RuleFor(p => p.CustomerDiscount.Description)
            .RequiredValidator("توضیحات")
            .MaxLengthValidator("توضیحات", 250);

        RuleFor(p => p.CustomerDiscount.Rate)
            .RangeValidator("درصد", 1, 100);
    }
}