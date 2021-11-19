using SM.Application.Contracts.Slider.Commands;

namespace SM.Application.Slider.CommandHandles;

public class RemoveSliderCommandHandler : IRequestHandler<RemoveSliderCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Slider.Slider> _sliderRepository;

    public RemoveSliderCommandHandler(IGenericRepository<Domain.Slider.Slider> sliderRepository)
    {
        _sliderRepository = Guard.Against.Null(sliderRepository, nameof(_sliderRepository));
    }

    #endregion

    public async Task<Response<string>> Handle(RemoveSliderCommand request, CancellationToken cancellationToken)
    {
        var slider = await _sliderRepository.GetEntityById(request.SliderId);

        if (slider is null)
            throw new NotFoundApiException();

        await _sliderRepository.SoftDelete(slider.Id);
        await _sliderRepository.SaveChanges();

        return new Response<string>(ApplicationErrorMessage.RecordDeletedMessage);
    }
}