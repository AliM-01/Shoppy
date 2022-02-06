using SM.Application.Contracts.Slider.Queries;

namespace SM.Infrastructure.Shared.Validators.Slider.Queries;

public class GetSliderDetailsQueryValidator : AbstractValidator<GetSliderDetailsQuery>
{
    public GetSliderDetailsQueryValidator()
    {
        RuleFor(p => p.Id)
            .RangeValidator("شناسه", 1, 100000);
    }
}

