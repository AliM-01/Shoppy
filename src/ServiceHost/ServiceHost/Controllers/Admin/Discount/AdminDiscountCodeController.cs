using DM.Application.DiscountCode.Commands;
using DM.Application.DiscountCode.DTOs;
using DM.Application.DiscountCode.Queries;

namespace ServiceHost.Controllers.Admin.Discount;

[SwaggerTag("مدیریت کد های تخفیف")]
public class AdminDiscountCodeController : BaseAdminApiController
{
    #region Filter Customer Discounts

    [HttpGet(AdminDiscountEndpoints.DiscountCode.FilterDiscountCodes)]
    [SwaggerOperation(Summary = "فیلتر کد های تخفیف", Tags = new[] { "AdminDiscountCode" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(FilterDiscountCodeDto), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> FilterDiscountCodes([FromQuery] FilterDiscountCodeDto filter)
    {
        var res = await Mediator.Send(new FilterDiscountCodesQuery(filter));

        return SuccessResult(res);
    }

    #endregion

    #region Get DiscountCode Details

    [HttpGet(AdminDiscountEndpoints.DiscountCode.GetDiscountCodeDetails)]
    [SwaggerOperation(Summary = "دریافت کد تخفیف", Tags = new[] { "AdminDiscountCode" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(EditDiscountCodeDto), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> GetDiscountCodeDetails([FromRoute] string id)
    {
        var res = await Mediator.Send(new GetDiscountCodeDetailsQuery(id));

        return SuccessResult(res);
    }

    #endregion

    #region Define Customer Discount

    [HttpPost(AdminDiscountEndpoints.DiscountCode.DefineDiscountCode)]
    [SwaggerOperation(Summary = "تعریف کد تخفیف", Tags = new[] { "AdminDiscountCode" })]
    [SwaggerResponse(201, "success : created")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ApiResult), 201)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> DefineDiscountCode([FromForm] DefineDiscountCodeDto createRequest)
    {
        var res = await Mediator.Send(new DefineDiscountCodeCommand(createRequest));

        return CreatedResult(res);
    }

    #endregion

    #region Edit Customer Discount

    [HttpPut(AdminDiscountEndpoints.DiscountCode.EditDiscountCode)]
    [SwaggerOperation(Summary = "ویرایش کد تخفیف", Tags = new[] { "AdminDiscountCode" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> EditDiscountCode([FromForm] EditDiscountCodeDto editRequest)
    {
        var res = await Mediator.Send(new EditDiscountCodeCommand(editRequest));

        return SuccessResult(res);
    }

    #endregion

    #region Remove Customer Discount

    [HttpDelete(AdminDiscountEndpoints.DiscountCode.RemoveDiscountCode)]
    [SwaggerOperation(Summary = "حذف کد تخفیف", Tags = new[] { "AdminDiscountCode" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> RemoveDiscountCode([FromRoute] string id)
    {
        var res = await Mediator.Send(new RemoveDiscountCodeCommand(id));

        return SuccessResult(res);
    }

    #endregion
}