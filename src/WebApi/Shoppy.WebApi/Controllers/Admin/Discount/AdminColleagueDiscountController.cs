using DM.Application.Contracts.ColleagueDiscount.Commands;
using DM.Application.Contracts.ColleagueDiscount.DTOs;
using DM.Application.Contracts.ColleagueDiscount.Queries;

namespace Shoppy.WebApi.Controllers.Admin.Discount;

[SwaggerTag("مدیریت تخفیفات همکار")]
public class AdminColleagueDiscountController : BaseApiController
{
    #region Filter Colleague Discounts

    [HttpGet(AdminDiscountApiEndpoints.ColleagueDiscount.FilterColleagueDiscounts)]
    [SwaggerOperation(Summary = "فیلتر تخفیفات همکار", Tags = new[] { "AdminColleagueDiscount" })]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> FilterColleagueDiscounts([FromQuery] FilterColleagueDiscountDto filter)
    {
        var res = await Mediator.Send(new FilterColleagueDiscountsQuery(filter));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get ColleagueDiscount Details

    [HttpGet(AdminDiscountApiEndpoints.ColleagueDiscount.GetColleagueDiscountDetails)]
    [SwaggerOperation(Summary = "دریافت تخفیف همکار", Tags = new[] { "AdminColleagueDiscount" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> GetColleagueDiscountDetails([FromRoute] long id)
    {
        var res = await Mediator.Send(new GetColleagueDiscountDetailsQuery(id));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Define Colleague Discount

    [HttpPost(AdminDiscountApiEndpoints.ColleagueDiscount.DefineColleagueDiscount)]
    [SwaggerOperation(Summary = "تعریف تخفیف همکار", Tags = new[] { "AdminColleagueDiscount" })]
    [SwaggerResponse(201, "success : created")]
    [SwaggerResponse(400, "error : discount exists for product")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> DefineColleagueDiscount([FromForm] DefineColleagueDiscountDto createRequest)
    {
        var res = await Mediator.Send(new DefineColleagueDiscountCommand(createRequest));

        return JsonApiResult.Created(res);
    }

    #endregion

    #region Edit Colleague Discount

    [HttpPut(AdminDiscountApiEndpoints.ColleagueDiscount.EditColleagueDiscount)]
    [SwaggerOperation(Summary = "ویرایش تخفیف همکار", Tags = new[] { "AdminColleagueDiscount" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> EditColleagueDiscount([FromForm] EditColleagueDiscountDto editRequest)
    {
        var res = await Mediator.Send(new EditColleagueDiscountCommand(editRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Remove Colleague Discount

    [HttpPost(AdminDiscountApiEndpoints.ColleagueDiscount.RemoveColleagueDiscount)]
    [SwaggerOperation(Summary = "غیر فعال کردن تخفیف همکار", Tags = new[] { "AdminColleagueDiscount" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> RemoveColleagueDiscount([FromRoute] long id)
    {
        var res = await Mediator.Send(new RemoveColleagueDiscountCommand(id));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Restore Colleague Discount

    [HttpPost(AdminDiscountApiEndpoints.ColleagueDiscount.RestoreColleagueDiscount)]
    [SwaggerOperation(Summary = "فعال کردن تخفیف همکار", Tags = new[] { "AdminColleagueDiscount" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> RestoreColleagueDiscount([FromRoute] long id)
    {
        var res = await Mediator.Send(new RestoreColleagueDiscountCommand(id));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Delete Colleague Discount

    [HttpDelete(AdminDiscountApiEndpoints.ColleagueDiscount.DeleteColleagueDiscount)]
    [SwaggerOperation(Summary = "حذف تخفیف همکار", Tags = new[] { "AdminColleagueDiscount" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> DeleteColleagueDiscount([FromRoute] long id)
    {
        var res = await Mediator.Send(new DeleteColleagueDiscountCommand(id));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Check Product Has Colleague Discount

    [HttpGet(AdminDiscountApiEndpoints.ColleagueDiscount.CheckProductHasColleagueDiscount)]
    [SwaggerOperation(Summary = "چک کردن وجود تخفیف همکار برای محصول", Tags = new[] { "AdminColleagueDiscount" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> CheckProductHasColleagueDiscount([FromRoute] long productId)
    {
        var res = await Mediator.Send(new CheckProductHasColleagueDiscountQuery(productId));

        return JsonApiResult.Success(res);
    }

    #endregion
}
