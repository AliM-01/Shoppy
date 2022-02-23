using _0_Framework.Infrastructure;
using _01_Shoppy.Query.Models.Slider;
using AutoMapper;

namespace _01_Shoppy.Query.Queries.Slider;

public record GetSlidersQuery() : IRequest<Response<IEnumerable<SliderQueryModel>>>;

public class GetSlidersQueryHandler : IRequestHandler<GetSlidersQuery, Response<IEnumerable<SliderQueryModel>>>
{
    #region Ctor

    private readonly IGenericRepository<SM.Domain.Slider.Slider> _sliderRepository;
    private readonly IMapper _mapper;

    public GetSlidersQueryHandler(IGenericRepository<SM.Domain.Slider.Slider> sliderRepository, IMapper mapper)
    {
        _sliderRepository = Guard.Against.Null(sliderRepository, nameof(_sliderRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<IEnumerable<SliderQueryModel>>> Handle(GetSlidersQuery request, CancellationToken cancellationToken)
    {
        var sliders = (await _sliderRepository.AsQueryable().ToListAsyncSafe())
             .Select(slider => _mapper.Map(slider, new SliderQueryModel()))
             .ToList();

        return new Response<IEnumerable<SliderQueryModel>>(sliders);
    }
}
