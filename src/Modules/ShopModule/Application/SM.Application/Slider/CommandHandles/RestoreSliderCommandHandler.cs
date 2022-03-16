using SM.Application.Contracts.Slider.Commands;

namespace SM.Application.Slider.CommandHandles;

public class RestoreSliderCommandHandler : IRequestHandler<RestoreSliderCommand, Response<string>>
{
    #region Ctor

    private readonly IRepository<Domain.Slider.Slider> _sliderRepository;

    public RestoreSliderCommandHandler(IRepository<Domain.Slider.Slider> sliderRepository)
    {
        _sliderRepository = Guard.Against.Null(sliderRepository, nameof(_sliderRepository));
    }

    #endregion

    public async Task<Response<string>> Handle(RestoreSliderCommand request, CancellationToken cancellationToken)
    {
        var slider = await _sliderRepository.GetByIdAsync(request.SliderId);

        if (slider is null)
            throw new NotFoundApiException();

        slider.IsDeleted = false;

        await _sliderRepository.UpdateAsync(slider);

        return new Response<string>(ApplicationErrorMessage.RecordDeletedMessage);
    }
}