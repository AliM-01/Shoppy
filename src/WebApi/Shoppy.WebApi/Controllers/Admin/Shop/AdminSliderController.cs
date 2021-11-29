using SM.Application.Contracts.Slider.Commands;
using SM.Application.Contracts.Slider.DTOs;
using SM.Application.Contracts.Slider.Queries;

namespace Shoppy.WebApi.Controllers.Admin.Shop;
public class AdminSliderController : BaseApiController
{
    #region Get Sliders List

    /// <summary>
    ///    دریافت لیست اسلایدر ها
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(AdminShopApiEndpoints.Slider.GetSlidersList)]
    public async Task<IActionResult> GetSlidersList()
    {
        var res = await Mediator.Send(new GetSlidersListQuery());

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get Slider Details

    /// <summary>
    ///    دریافت جزییات اسلایدر
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(AdminShopApiEndpoints.Slider.GetSliderDetails)]
    public async Task<IActionResult> GetSliderDetails([FromRoute] long id)
    {
        var res = await Mediator.Send(new GetSliderDetailsQuery(id));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Create Slider

    /// <summary>
    ///    ایجاد اسلایدر
    /// </summary>
    /// <response code="200">Success</response>
    [HttpPost(AdminShopApiEndpoints.Slider.CreateSlider)]
    public async Task<IActionResult> CreateSlider([FromForm] CreateSliderDto createRequest)
    {
        var res = await Mediator.Send(new CreateSliderCommand(createRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Edit Slider

    /// <summary>
    ///    ویرایش اسلایدر
    /// </summary>
    /// <response code="200">Success</response>
    [HttpPut(AdminShopApiEndpoints.Slider.EditSlider)]
    public async Task<IActionResult> EditSlider([FromForm] EditSliderDto editRequest)
    {
        var res = await Mediator.Send(new EditSliderCommand(editRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Remove Slider

    /// <summary>
    ///    غیر فعال کردن اسلایدر
    /// </summary>
    /// <response code="200">Success</response>
    [HttpDelete(AdminShopApiEndpoints.Slider.RemoveSlider)]
    public async Task<IActionResult> RemoveSlider([FromRoute] long id)
    {
        var res = await Mediator.Send(new RemoveSliderCommand(id));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Restore Slider

    /// <summary>
    ///    فعال کردن اسلایدر
    /// </summary>
    /// <response code="200">Success</response>
    [HttpDelete(AdminShopApiEndpoints.Slider.RestoreSlider)]
    public async Task<IActionResult> RestoreSlider([FromRoute] long id)
    {
        var res = await Mediator.Send(new RestoreSliderCommand(id));

        return JsonApiResult.Success(res);
    }

    #endregion
}