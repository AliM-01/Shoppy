using SM.Application.Slider.Commands;

namespace SM.Application.Slider.CommandHandles;

public class CreateSliderCommandHandler : IRequestHandler<CreateSliderCommand, ApiResult>
{
    #region Ctor

    private readonly IRepository<Domain.Slider.Slider> _sliderRepository;
    private readonly IMapper _mapper;

    public CreateSliderCommandHandler(IRepository<Domain.Slider.Slider> sliderRepository, IMapper mapper)
    {
        _sliderRepository = Guard.Against.Null(sliderRepository, nameof(_sliderRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<ApiResult> Handle(CreateSliderCommand request, CancellationToken cancellationToken)
    {
        var slider =
            _mapper.Map(request.Slider, new Domain.Slider.Slider());

        string imagePath = request.Slider.ImageFile.GenerateImagePath();

        request.Slider.ImageFile.AddImageToServer(imagePath, PathExtension.SliderImage,
            200, 200, PathExtension.SliderThumbnailImage);
        slider.ImagePath = imagePath;

        await _sliderRepository.InsertAsync(slider);

        return ApiResponse.Success();
    }
}