using SM.Application.Contracts.Slider.Commands;
using SM.Application.Contracts.Slider.DTOs;
using SM.Application.Contracts.Slider.Queries;

namespace Shoppy.WebApi.Controllers.Admin.Shop;

[SwaggerTag("مدیریت اسلایدر ها")]
public class AdminSliderController : BaseApiController
{
    #region Get Sliders List

    [HttpGet(AdminShopApiEndpoints.Slider.GetSlidersList)]
    [SwaggerOperation(Summary = "دریافت لیست اسلایدر ها", Tags = new[] { "AdminSlider" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> GetSlidersList()
    {
        var res = await Mediator.Send(new GetSlidersListQuery());

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get Slider Details

    [HttpGet(AdminShopApiEndpoints.Slider.GetSliderDetails)]
    [SwaggerOperation(Summary = "دریافت جزییات اسلایدر", Tags = new[] { "AdminSlider" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> GetSliderDetails([FromRoute] string id)
    {
        var res = await Mediator.Send(new GetSliderDetailsQuery(id));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Create Slider

    [HttpPost(AdminShopApiEndpoints.Slider.CreateSlider)]
    [SwaggerOperation(Summary = "ایجاد اسلایدر", Tags = new[] { "AdminSlider" })]
    [SwaggerResponse(201, "success : created")]
    public async Task<IActionResult> CreateSlider([FromForm] CreateSliderDto createRequest)
    {
        var res = await Mediator.Send(new CreateSliderCommand(createRequest));

        return JsonApiResult.Created(res);
    }

    #endregion

    #region Edit Slider

    [HttpPut(AdminShopApiEndpoints.Slider.EditSlider)]
    [SwaggerOperation(Summary = "ویرایش اسلایدر", Tags = new[] { "AdminSlider" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> EditSlider([FromForm] EditSliderDto editRequest)
    {
        var res = await Mediator.Send(new EditSliderCommand(editRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Remove Slider

    [HttpDelete(AdminShopApiEndpoints.Slider.RemoveSlider)]
    [SwaggerOperation(Summary = "غیر فعال کردن اسلایدر", Tags = new[] { "AdminSlider" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> RemoveSlider([FromRoute] string id)
    {
        var res = await Mediator.Send(new RemoveSliderCommand(id));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Restore Slider

    [HttpDelete(AdminShopApiEndpoints.Slider.RestoreSlider)]
    [SwaggerOperation(Summary = "فعال کردن اسلایدر", Tags = new[] { "AdminSlider" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> RestoreSlider([FromRoute] string id)
    {
        var res = await Mediator.Send(new RestoreSliderCommand(id));

        return JsonApiResult.Success(res);
    }

    #endregion
}