using SM.Application.Contracts.Slider.DTOs;
using SM.Application.Contracts.Slider.Queries;

namespace SM.Application.Slider.QueryHandles;
public class GetSliderDetailsQueryHandler : IRequestHandler<GetSliderDetailsQuery, Response<EditSliderDto>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Slider.Slider> _sliderRepository;
    private readonly IMapper _mapper;

    public GetSliderDetailsQueryHandler(IGenericRepository<Domain.Slider.Slider> sliderRepository, IMapper mapper)
    {
        _sliderRepository = Guard.Against.Null(sliderRepository, nameof(_sliderRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<EditSliderDto>> Handle(GetSliderDetailsQuery request, CancellationToken cancellationToken)
    {
        var slider = await _sliderRepository.GetByIdAsync(request.Id);

        if (slider is null)
            throw new NotFoundApiException();

        var mappedSlider = _mapper.Map<EditSliderDto>(slider);

        return new Response<EditSliderDto>(mappedSlider);
    }
}