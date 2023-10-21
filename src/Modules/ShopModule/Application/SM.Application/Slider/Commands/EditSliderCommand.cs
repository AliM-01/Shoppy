using FluentValidation;
using SM.Application.Slider.Commands;
using SM.Application.Slider.DTOs;

namespace SM.Application.Slider.Commands;

public record EditSliderCommand(EditSliderDto Slider) : IRequest<ApiResult>;

public class EditSliderCommandValidator : AbstractValidator<EditSliderCommand>
{
    public EditSliderCommandValidator()
    {
        RuleFor(p => p.Slider.Id)
            .RequiredValidator("شناسه");

        RuleFor(p => p.Slider.Heading)
            .RequiredValidator("عنوان")
            .MaxLengthValidator("عنوان", 100);

        RuleFor(p => p.Slider.Text)
            .RequiredValidator("متن")
            .MaxLengthValidator("متن", 250);

        RuleFor(p => p.Slider.ImageFile)
            .MaxFileSizeValidator((3 * 1024 * 1024), false);

        RuleFor(p => p.Slider.BtnLink)
            .RequiredValidator("لینک")
            .MaxLengthValidator("لینک", 100);

        RuleFor(p => p.Slider.BtnText)
            .RequiredValidator("متن لینک")
            .MaxLengthValidator("متن لینک", 50);
    }
}

public class EditSliderCommandHandler : IRequestHandler<EditSliderCommand, ApiResult>
{
    private readonly IRepository<Domain.Slider.Slider> _sliderRepository;
    private readonly IMapper _mapper;

    public EditSliderCommandHandler(IRepository<Domain.Slider.Slider> sliderRepository, IMapper mapper)
    {
        _sliderRepository = Guard.Against.Null(sliderRepository, nameof(_sliderRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<ApiResult> Handle(EditSliderCommand request, CancellationToken cancellationToken)
    {
        var slider = await _sliderRepository.FindByIdAsync(request.Slider.Id);

        NotFoundApiException.ThrowIfNull(slider);

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