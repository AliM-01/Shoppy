using _01_Shoppy.Query.Contracts.Slider;
using AutoMapper;
using SM.Infrastructure.Persistence.Context;

namespace _01_Shoppy.Query.Query;

public class SliderQuery : ISliderQuery
{
    #region Ctor

    private readonly ShopDbContext _context;
    private readonly IMapper _mapper;

    public SliderQuery(ShopDbContext context, IMapper mapper)
    {
        _context = Guard.Against.Null(context, nameof(_context));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<IEnumerable<SliderQueryModel>> GetSliders()
    {
        return await _context.Sliders
            .Select(slider => _mapper.Map(slider, new SliderQueryModel()))
            .ToListAsync();
    }
}
