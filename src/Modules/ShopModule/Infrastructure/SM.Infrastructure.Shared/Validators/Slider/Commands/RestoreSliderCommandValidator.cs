using SM.Application.Contracts.Slider.Commands;

namespace SM.Infrastructure.Shared.Validators.Slider.Commands;

public class RestoreSliderCommandValidator : AbstractValidator<RestoreSliderCommand>
{
    public RestoreSliderCommandValidator()
    {
        RuleFor(p => p.SliderId)
            .RequiredValidator("شناسه دسته بندی");
    }
}
