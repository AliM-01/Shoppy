using SM.Application.Slider.Commands;

namespace SM.Infrastructure.Shared.Validators.Slider.Commands;

public class CreateSliderCommandValidator : AbstractValidator<CreateSliderCommand>
{
    public CreateSliderCommandValidator()
    {
        RuleFor(p => p.Slider.Heading)
            .RequiredValidator("عنوان")
            .MaxLengthValidator("عنوان", 100);

        RuleFor(p => p.Slider.Text)
            .RequiredValidator("متن")
            .MaxLengthValidator("متن", 250);

        RuleFor(p => p.Slider.ImageFile)
            .MaxFileSizeValidator((3 * 1024 * 1024));

        RuleFor(p => p.Slider.BtnLink)
            .RequiredValidator("لینک")
            .MaxLengthValidator("لینک", 100);

        RuleFor(p => p.Slider.BtnText)
            .RequiredValidator("متن لینک")
            .MaxLengthValidator("متن لینک", 50);
    }
}

