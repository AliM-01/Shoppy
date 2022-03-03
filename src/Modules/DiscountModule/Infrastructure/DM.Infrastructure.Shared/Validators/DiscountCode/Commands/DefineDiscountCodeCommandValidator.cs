using DM.Application.Contracts.DiscountCode.Commands;

namespace DM.Infrastructure.Shared.Validators.DiscountCode;

public class DefineDiscountCodeCommandValidator : AbstractValidator<DefineDiscountCodeCommand>
{
    public DefineDiscountCodeCommandValidator()
    {
        RuleFor(p => p.DiscountCode.Code)
            .RequiredValidator("کد");

        RuleFor(p => p.DiscountCode.StartDate)
            .RequiredValidator("تاریخ شروع");

        RuleFor(p => p.DiscountCode.EndDate)
            .RequiredValidator("تاریخ پایان");

        RuleFor(p => p.DiscountCode.Description)
            .RequiredValidator("توضیحات")
            .MaxLengthValidator("توضیحات", 250);

        RuleFor(p => p.DiscountCode.Rate)
            .RangeValidator("درصد", 1, 100);
    }
}