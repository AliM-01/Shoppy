using SM.Application.Slider.Commands;

namespace SM.Infrastructure.Shared.Validators.Slider.Commands;

public class RemoveSliderCommandValidator : AbstractValidator<RemoveSliderCommand>
{
    public RemoveSliderCommandValidator()
    {
        RuleFor(p => p.SliderId)
            .RequiredValidator("شناسه دسته بندی");
    }
}
