using _01_Shoppy.Query.Models.Slider;
using _01_Shoppy.Query.Queries.Slider;

namespace Shoppy.WebApi.Controllers.Main.Shop;

[SwaggerTag("اسلایدر")]
public class SliderController : BaseApiController
{
    #region Get Sliders

    [HttpGet(MainShopEndpoints.Slider.GetSliders)]
    [SwaggerOperation(Summary = "دریافت اسلایدر ها", Tags = new[] { "Slider" })]
    [SwaggerResponse(200, "success")]
    [ProducesResponseType(typeof(ApiResult<List<SliderQueryModel>>), 200)]
    public async Task<IActionResult> GetSliders()
    {
        var res = await Mediator.Send(new GetSlidersQuery());

        return SuccessResult(res);
    }

    #endregion
}