using _01_Shoppy.Query.Contracts.Slider;
using SM.Infrastructure.Persistence.Context;

namespace _01_Shoppy.Query.Query;

public class SliderQuery : ISliderQuery
{
    #region Ctor

    private readonly ShopDbContext _context;

    public SliderQuery(ShopDbContext context)
    {
        _context = context;
    }

    #endregion

    public async Task<IEnumerable<SliderQueryModel>> GetSliders()
    {
        return await _context.Sliders
            .Select(x => new SliderQueryModel
            {
                Id = x.Id,
                Heading = x.Heading,
                Text = x.Text,
                BtnLink = x.BtnLink,
                BtnText = x.BtnText,
                ImageAlt = x.ImageAlt,
                ImagePath = x.ImagePath,
                ImageTitle = x.ImageTitle
            }).ToListAsync();
    }
}
