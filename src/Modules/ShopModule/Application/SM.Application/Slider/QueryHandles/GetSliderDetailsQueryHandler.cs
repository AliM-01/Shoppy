using SM.Application.Contracts.Slider.DTOs;
using SM.Application.Contracts.Slider.Queries;

namespace SM.Application.Slider.QueryHandles;
public class GetSliderDetailsQueryHandler : IRequestHandler<GetSliderDetailsQuery, EditSliderDto>
{
    #region Ctor

    private readonly IRepository<Domain.Slider.Slider> _sliderRepository;
    private readonly IMapper _mapper;

    public GetSliderDetailsQueryHandler(IRepository<Domain.Slider.Slider> sliderRepository, IMapper mapper)
    {
        _sliderRepository = Guard.Against.Null(sliderRepository, nameof(_sliderRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<EditSliderDto> Handle(GetSliderDetailsQuery request, CancellationToken cancellationToken)
    {
        var slider = await _sliderRepository.FindByIdAsync(request.Id);

        if (slider is null)
            throw new NotFoundApiException();

        return _mapper.Map<EditSliderDto>(slider);
    }
}