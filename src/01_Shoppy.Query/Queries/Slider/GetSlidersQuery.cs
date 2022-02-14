using _01_Shoppy.Query.Models.Slider;
using AutoMapper;
using SM.Infrastructure.Persistence.Context;

namespace _01_Shoppy.Query.Queries.Slider;

public record GetSlidersQuery() : IRequest<Response<IEnumerable<SliderQueryModel>>>;

public class GetSlidersQueryHandler : IRequestHandler<GetSlidersQuery, Response<IEnumerable<SliderQueryModel>>>
{
    #region Ctor

    private readonly ShopDbContext _context;
    private readonly IMapper _mapper;

    public GetSlidersQueryHandler(ShopDbContext context, IMapper mapper)
    {
        _context = Guard.Against.Null(context, nameof(_context));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<IEnumerable<SliderQueryModel>>> Handle(GetSlidersQuery request, CancellationToken cancellationToken)
    {
        var sliders = await _context.Sliders
             .Select(slider => _mapper.Map(slider, new SliderQueryModel()))
             .ToListAsync();

        return new Response<IEnumerable<SliderQueryModel>>(sliders);
    }
}
