using DM.Application.Contracts.DiscountCode.Commands;

namespace DM.Infrastructure.Shared.Validators.DiscountCode;

public class EditDiscountCodeCommandValidator : AbstractValidator<EditDiscountCodeCommand>
{
    public EditDiscountCodeCommandValidator()
    {
        RuleFor(p => p.DiscountCode.Id)
            .RequiredValidator("شناسه تخفیف");

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