using SM.Application.Contracts.Slider.Commands;
using SM.Application.Contracts.Slider.DTOs;
using SM.Application.Contracts.Slider.Queries;

namespace Shoppy.WebApi.Controllers.Admin.Shop;

[SwaggerTag("مدیریت اسلایدر ها")]
public class AdminSliderController : BaseAdminApiController
{
    #region Get Sliders List

    [HttpGet(AdminShopEndpoints.Slider.GetSlidersList)]
    [SwaggerOperation(Summary = "دریافت لیست اسلایدر ها", Tags = new[] { "AdminSlider" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(IEnumerable<SliderDto>), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> GetSlidersList()
    {
        var res = await Mediator.Send(new GetSlidersListQuery());

        return SuccessResult(res);
    }

    #endregion

    #region Get Slider Details

    [HttpGet(AdminShopEndpoints.Slider.GetSliderDetails)]
    [SwaggerOperation(Summary = "دریافت جزییات اسلایدر", Tags = new[] { "AdminSlider" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(EditSliderDto), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> GetSliderDetails([FromRoute] string id)
    {
        var res = await Mediator.Send(new GetSliderDetailsQuery(id));

        return SuccessResult(res);
    }

    #endregion

    #region Create Slider

    [HttpPost(AdminShopEndpoints.Slider.CreateSlider)]
    [SwaggerOperation(Summary = "ایجاد اسلایدر", Tags = new[] { "AdminSlider" })]
    [SwaggerResponse(201, "success : created")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ApiResult), 201)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> CreateSlider([FromForm] CreateSliderDto createRequest)
    {
        var res = await Mediator.Send(new CreateSliderCommand(createRequest));

        return CreatedResult(res);
    }

    #endregion

    #region Edit Slider

    [HttpPut(AdminShopEndpoints.Slider.EditSlider)]
    [SwaggerOperation(Summary = "ویرایش اسلایدر", Tags = new[] { "AdminSlider" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> EditSlider([FromForm] EditSliderDto editRequest)
    {
        var res = await Mediator.Send(new EditSliderCommand(editRequest));

        return SuccessResult(res);
    }

    #endregion

    #region Remove Slider

    [HttpDelete(AdminShopEndpoints.Slider.RemoveSlider)]
    [SwaggerOperation(Summary = "غیر فعال کردن اسلایدر", Tags = new[] { "AdminSlider" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> RemoveSlider([FromRoute] string id)
    {
        var res = await Mediator.Send(new RemoveSliderCommand(id));

        return SuccessResult(res);
    }

    #endregion

    #region Restore Slider

    [HttpDelete(AdminShopEndpoints.Slider.RestoreSlider)]
    [SwaggerOperation(Summary = "فعال کردن اسلایدر", Tags = new[] { "AdminSlider" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> RestoreSlider([FromRoute] string id)
    {
        var res = await Mediator.Send(new RestoreSliderCommand(id));

        return SuccessResult(res);
    }

    #endregion
}