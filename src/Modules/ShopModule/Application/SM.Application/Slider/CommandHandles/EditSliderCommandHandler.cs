using _0_Framework.Application.Extensions;
using SM.Application.Contracts.Slider.Commands;
using System.IO;

namespace SM.Application.Slider.CommandHandles;

public class EditSliderCommandHandler : IRequestHandler<EditSliderCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Slider.Slider> _sliderRepository;
    private readonly IMapper _mapper;

    public EditSliderCommandHandler(IGenericRepository<Domain.Slider.Slider> sliderRepository, IMapper mapper)
    {
        _sliderRepository = Guard.Against.Null(sliderRepository, nameof(_sliderRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(EditSliderCommand request, CancellationToken cancellationToken)
    {
        var slider = await _sliderRepository.GetByIdAsync(request.Slider.Id);

        if (slider is null)
            throw new NotFoundApiException();

        _mapper.Map(request.Slider, slider);

        if (request.Slider.ImageFile != null)
        {
            var imagePath = DateTime.Now.ToFileName() + Path.GetExtension(request.Slider.ImageFile.FileName);

            request.Slider.ImageFile.AddImageToServer(imagePath, PathExtension.SliderImage,
                200, 200, PathExtension.SliderThumbnailImage, slider.ImagePath);
            slider.ImagePath = imagePath;
        }

        _sliderRepository.Update(slider);
        await _sliderRepository.SaveChanges();

        return new Response<string>();
    }
}