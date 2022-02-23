using SM.Application.Contracts.Slider.Commands;

namespace SM.Infrastructure.Shared.Validators.Slider.Commands;

public class EditSliderCommandValidator : AbstractValidator<EditSliderCommand>
{
    public EditSliderCommandValidator()
    {
        RuleFor(p => p.Slider.Id)
            .RequiredValidator("شناسه");

        RuleFor(p => p.Slider.Heading)
            .RequiredValidator("عنوان")
            .MaxLengthValidator("عنوان", 100);

        RuleFor(p => p.Slider.Text)
            .RequiredValidator("متن")
            .MaxLengthValidator("متن", 250);

        RuleFor(p => p.Slider.ImageFile)
            .MaxFileSizeValidator((3 * 1024 * 1024), false);

        RuleFor(p => p.Slider.BtnLink)
            .RequiredValidator("لینک")
            .MaxLengthValidator("لینک", 100);

        RuleFor(p => p.Slider.BtnText)
            .RequiredValidator("متن لینک")
            .MaxLengthValidator("متن لینک", 50);
    }
}

