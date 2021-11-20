using SM.Application.Contracts.Slider.Queries;

namespace Shoppy.Admin.WebApi.Controllers;
public class SliderController : BaseApiController
{
    /// <summary>
    ///    دریافت لیست اسلایدر ها
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(ApiEndpoints.Slider.GetSlidersList)]
    public async Task<IActionResult> GetSlidersList()
    {
        var res = await Mediator.Send(new GetSlidersListQuery());

        return JsonApiResult.Success(res);
    }

    /// <summary>
    ///    دریافت جزییات اسلایدر
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(ApiEndpoints.Slider.GetSliderDetails)]
    public async Task<IActionResult> GetSliderDetails([FromRoute] long id)
    {
        var res = await Mediator.Send(new GetSliderDetailsQuery(id));

        return JsonApiResult.Success(res);
    }
}