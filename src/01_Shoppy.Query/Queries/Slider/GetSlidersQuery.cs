using _01_Shoppy.Query.Models.Slider;

namespace _01_Shoppy.Query.Queries.Slider;

public record GetSlidersQuery() : IRequest<ApiResult<List<SliderQueryModel>>>;

public class GetSlidersQueryHandler : IRequestHandler<GetSlidersQuery, ApiResult<List<SliderQueryModel>>>
{
    #region Ctor

    private readonly IRepository<SM.Domain.Slider.Slider> _sliderRepository;
    private readonly IMapper _mapper;

    public GetSlidersQueryHandler(IRepository<SM.Domain.Slider.Slider> sliderRepository, IMapper mapper)
    {
        _sliderRepository = Guard.Against.Null(sliderRepository, nameof(_sliderRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public Task<ApiResult<List<SliderQueryModel>>> Handle(GetSlidersQuery request, CancellationToken cancellationToken)
    {
        var sliders = _sliderRepository.AsQueryable()
                                        .ToList()
                                        .Select(slider => _mapper.Map(slider, new SliderQueryModel()))
                                        .ToList();

        return Task.FromResult(ApiResponse.Success<List<SliderQueryModel>>(sliders));
    }
}
