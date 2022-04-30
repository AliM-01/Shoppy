using SM.Application.Contracts.Slider.Commands;

namespace SM.Application.Slider.CommandHandles;

public class EditSliderCommandHandler : IRequestHandler<EditSliderCommand, ApiResult>
{
    #region Ctor

    private readonly IRepository<Domain.Slider.Slider> _sliderRepository;
    private readonly IMapper _mapper;

    public EditSliderCommandHandler(IRepository<Domain.Slider.Slider> sliderRepository, IMapper mapper)
    {
        _sliderRepository = Guard.Against.Null(sliderRepository, nameof(_sliderRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<ApiResult> Handle(EditSliderCommand request, CancellationToken cancellationToken)
    {
        var slider = await _sliderRepository.FindByIdAsync(request.Slider.Id);

        if (slider is null)
            throw new NotFoundApiException();

        _mapper.Map(request.Slider, slider);

        if (request.Slider.ImageFile != null)
        {
            string imagePath = request.Slider.ImageFile.GenerateImagePath();

            request.Slider.ImageFile.AddImageToServer(imagePath, PathExtension.SliderImage,
                200, 200, PathExtension.SliderThumbnailImage, slider.ImagePath);
            slider.ImagePath = imagePath;
        }

        await _sliderRepository.UpdateAsync(slider);

        return ApiResponse.Success();
    }
}