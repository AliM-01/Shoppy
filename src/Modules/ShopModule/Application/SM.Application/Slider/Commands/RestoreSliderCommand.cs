using SM.Application.Slider.Commands;

namespace SM.Application.Slider.Commands;

public record RestoreSliderCommand(string SliderId) : IRequest<ApiResult>;

public class RestoreSliderCommandHandler : IRequestHandler<RestoreSliderCommand, ApiResult>
{
    private readonly IRepository<Domain.Slider.Slider> _sliderRepository;

    public RestoreSliderCommandHandler(IRepository<Domain.Slider.Slider> sliderRepository)
    {
        _sliderRepository = Guard.Against.Null(sliderRepository, nameof(_sliderRepository));
    }

    public async Task<ApiResult> Handle(RestoreSliderCommand request, CancellationToken cancellationToken)
    {
        var slider = await _sliderRepository.FindByIdAsync(request.SliderId);

        NotFoundApiException.ThrowIfNull(slider);

        slider.IsDeleted = false;

        await _sliderRepository.UpdateAsync(slider);

        return ApiResponse.Success(ApplicationErrorMessage.RecordDeleted);
    }
}