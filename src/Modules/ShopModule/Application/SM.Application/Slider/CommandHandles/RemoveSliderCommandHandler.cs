using SM.Application.Contracts.Slider.Commands;

namespace SM.Application.Slider.CommandHandles;

public class RemoveSliderCommandHandler : IRequestHandler<RemoveSliderCommand, ApiResult>
{
    #region Ctor

    private readonly IRepository<Domain.Slider.Slider> _sliderRepository;

    public RemoveSliderCommandHandler(IRepository<Domain.Slider.Slider> sliderRepository)
    {
        _sliderRepository = Guard.Against.Null(sliderRepository, nameof(_sliderRepository));
    }

    #endregion

    public async Task<ApiResult> Handle(RemoveSliderCommand request, CancellationToken cancellationToken)
    {
        var slider = await _sliderRepository.FindByIdAsync(request.SliderId);

        if (slider is null)
            throw new NotFoundApiException();

        await _sliderRepository.DeleteAsync(slider.Id);

        return ApiResponse.Success(ApplicationErrorMessage.RecordDeleted);
    }
}