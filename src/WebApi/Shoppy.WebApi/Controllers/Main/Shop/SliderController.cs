using SM.Application.Slider.DTOs;
using SM.Application.Slider.Queries;

namespace Shoppy.WebApi.Controllers.Main.Shop;

[SwaggerTag("اسلایدر")]
public class SliderController : BaseApiController
{
    #region Get Sliders

    [HttpGet(MainShopEndpoints.Slider.GetSliders)]
    [SwaggerOperation(Summary = "دریافت اسلایدر ها", Tags = new[] { "Slider" })]
    [SwaggerResponse(200, "success")]
    [ProducesResponseType(typeof(IEnumerable<SiteSliderDto>), 200)]
    public async Task<IActionResult> GetSliders()
    {
        var res = await Mediator.Send(new GetSlidersForSiteQuery());

        return SuccessResult(res);
    }

    #endregion
}