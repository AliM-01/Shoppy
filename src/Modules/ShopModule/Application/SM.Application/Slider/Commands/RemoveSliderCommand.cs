using FluentValidation;
using SM.Application.Slider.Commands;

namespace SM.Application.Slider.Commands;

public record RemoveSliderCommand(string SliderId) : IRequest<ApiResult>;

public class RemoveSliderCommandValidator : AbstractValidator<RemoveSliderCommand>
{
    public RemoveSliderCommandValidator()
    {
        RuleFor(p => p.SliderId)
            .RequiredValidator("شناسه دسته بندی");
    }
}

public class RemoveSliderCommandHandler : IRequestHandler<RemoveSliderCommand, ApiResult>
{
    private readonly IRepository<Domain.Slider.Slider> _sliderRepository;

    public RemoveSliderCommandHandler(IRepository<Domain.Slider.Slider> sliderRepository)
    {
        _sliderRepository = Guard.Against.Null(sliderRepository, nameof(_sliderRepository));
    }

    public async Task<ApiResult> Handle(RemoveSliderCommand request, CancellationToken cancellationToken)
    {
        var slider = await _sliderRepository.FindByIdAsync(request.SliderId);

        NotFoundApiException.ThrowIfNull(slider);

        await _sliderRepository.DeleteAsync(slider.Id);

        return ApiResponse.Success(ApplicationErrorMessage.RecordDeleted);
    }
}