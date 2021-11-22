using _01_Shoppy.Query.Contracts.Slider;

namespace Shoppy.Admin.WebApi.Controllers;
public class SliderController : BaseApiController
{
    #region Ctor 

    private readonly ISliderQuery _sliderQuery;

    public SliderController(ISliderQuery sliderQuery)
    {
        _sliderQuery = Guard.Against.Null(sliderQuery, nameof(_sliderQuery)); ;
    }

    #endregion


}