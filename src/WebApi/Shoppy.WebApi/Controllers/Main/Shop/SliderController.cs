using _01_Shoppy.Query.Contracts.Slider;

namespace Shoppy.WebApi.Controllers.Main.Shop;

[SwaggerTag("اسلایدر")]
public class SliderController : ControllerBase
{
    #region Ctor 

    private readonly ISliderQuery _sliderQuery;

    public SliderController(ISliderQuery sliderQuery)
    {
        _sliderQuery = Guard.Against.Null(sliderQuery, nameof(_sliderQuery)); ;
    }

    #endregion

    #region Get Sliders

    [HttpGet(MainShopApiEndpoints.Slider.GetSliders)]
    [SwaggerOperation(Summary = "دریافت اسلایدر ها")]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> GetSliders()
    {
        var res = await _sliderQuery.GetSliders();

        return JsonApiResult.Success(res);
    }

    #endregion
}