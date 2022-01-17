using _0_Framework.Application.Extensions;
using SM.Application.Contracts.Slider.Commands;
using System.IO;

namespace SM.Application.Slider.CommandHandles;

public class CreateSliderCommandHandler : IRequestHandler<CreateSliderCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Slider.Slider> _sliderRepository;
    private readonly IMapper _mapper;

    public CreateSliderCommandHandler(IGenericRepository<Domain.Slider.Slider> sliderRepository, IMapper mapper)
    {
        _sliderRepository = Guard.Against.Null(sliderRepository, nameof(_sliderRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(CreateSliderCommand request, CancellationToken cancellationToken)
    {
        var slider =
            _mapper.Map(request.Slider, new Domain.Slider.Slider());

        var imagePath = DateTime.Now.ToFileName() + Path.GetExtension(request.Slider.ImageFile.FileName);

        request.Slider.ImageFile.AddImageToServer(imagePath, PathExtension.SliderImage,
            200, 200, PathExtension.SliderThumbnailImage);
        slider.ImagePath = imagePath;

        await _sliderRepository.InsertEntity(slider);
        await _sliderRepository.SaveChanges();

        return new Response<string>(ApplicationErrorMessage.OperationSucceddedMessage);
    }
}