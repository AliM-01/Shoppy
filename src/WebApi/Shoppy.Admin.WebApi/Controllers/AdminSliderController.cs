using SM.Application.Contracts.Slider.Commands;
using SM.Application.Contracts.Slider.DTOs;
using SM.Application.Contracts.Slider.Queries;

namespace Shoppy.Admin.WebApi.Controllers;
public class AdminSliderController : BaseApiController
{
    /// <summary>
    ///    دریافت لیست اسلایدر ها
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(ApiEndpoints.AdminSlider.GetSlidersList)]
    public async Task<IActionResult> GetSlidersList()
    {
        var res = await Mediator.Send(new GetSlidersListQuery());

        return JsonApiResult.Success(res);
    }

    /// <summary>
    ///    دریافت جزییات اسلایدر
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(ApiEndpoints.AdminSlider.GetSliderDetails)]
    public async Task<IActionResult> GetSliderDetails([FromRoute] long id)
    {
        var res = await Mediator.Send(new GetSliderDetailsQuery(id));

        return JsonApiResult.Success(res);
    }

    /// <summary>
    ///    ایجاد اسلایدر
    /// </summary>
    /// <response code="200">Success</response>
    [HttpPost(ApiEndpoints.AdminSlider.CreateSlider)]
    public async Task<IActionResult> CreateSlider([FromForm] CreateSliderDto createRequest)
    {
        var res = await Mediator.Send(new CreateSliderCommand(createRequest));

        return JsonApiResult.Success(res);
    }

    /// <summary>
    ///    ویرایش اسلایدر
    /// </summary>
    /// <response code="200">Success</response>
    [HttpPut(ApiEndpoints.AdminSlider.EditSlider)]
    public async Task<IActionResult> EditSlider([FromForm] EditSliderDto editRequest)
    {
        var res = await Mediator.Send(new EditSliderCommand(editRequest));

        return JsonApiResult.Success(res);
    }

    /// <summary>
    ///    غیر فعال کردن اسلایدر
    /// </summary>
    /// <response code="200">Success</response>
    [HttpDelete(ApiEndpoints.AdminSlider.RemoveSlider)]
    public async Task<IActionResult> RemoveSlider([FromRoute] long id)
    {
        var res = await Mediator.Send(new RemoveSliderCommand(id));

        return JsonApiResult.Success(res);
    }

    /// <summary>
    ///    فعال کردن اسلایدر
    /// </summary>
    /// <response code="200">Success</response>
    [HttpDelete(ApiEndpoints.AdminSlider.RestoreSlider)]
    public async Task<IActionResult> RestoreSlider([FromRoute] long id)
    {
        var res = await Mediator.Send(new RestoreSliderCommand(id));

        return JsonApiResult.Success(res);
    }
}