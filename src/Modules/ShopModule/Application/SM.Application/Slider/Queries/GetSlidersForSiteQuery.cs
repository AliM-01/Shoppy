using SM.Application.Slider.DTOs;

namespace SM.Application.Slider.Queries;

public record GetSlidersForSiteQuery() : IRequest<IEnumerable<SiteSliderDto>>;

public class GetSlidersForSiteQueryHandler : IRequestHandler<GetSlidersForSiteQuery, IEnumerable<SiteSliderDto>>
{
    private readonly IRepository<Domain.Slider.Slider> _sliderRepository;
    private readonly IMapper _mapper;

    public GetSlidersForSiteQueryHandler(IRepository<Domain.Slider.Slider> sliderRepository, IMapper mapper)
    {
        _sliderRepository = Guard.Against.Null(sliderRepository, nameof(_sliderRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public Task<IEnumerable<SiteSliderDto>> Handle(GetSlidersForSiteQuery request, CancellationToken cancellationToken)
    {
        var sliders = _sliderRepository.AsQueryable()
                                        .ToList()
                                        .Select(slider => _mapper.Map(slider, new SiteSliderDto()));

        return Task.FromResult(sliders);
    }
}
