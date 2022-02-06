using SM.Application.Contracts.Slider.Commands;

namespace SM.Infrastructure.Shared.Validators.Slider.Commands;

public class RemoveSliderCommandValidator : AbstractValidator<RemoveSliderCommand>
{
    public RemoveSliderCommandValidator()
    {
        RuleFor(p => p.SliderId)
            .RangeValidator("شناسه دسته بندی", 1, 100000);
    }
}
