using DM.Application.Contracts.CustomerDiscount.Commands;
using DM.Application.Contracts.CustomerDiscount.DTOs;
using DM.Application.Contracts.CustomerDiscount.Queries;

namespace Shoppy.WebApi.Controllers.Admin.Discount;

[SwaggerTag("مدیریت تخفیفات مشتری")]
public class AdminCustomerDiscountController : BaseApiController
{
    #region Filter Customer Discounts

    [HttpGet(AdminDiscountApiEndpoints.CustomerDiscount.FilterCustomerDiscounts)]
    [SwaggerOperation(Summary = "فیلتر تخفیفات مشتری", Tags = new[] { "AdminCustomerDiscount" })]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> FilterCustomerDiscounts([FromQuery] FilterCustomerDiscountDto filter)
    {
        var res = await Mediator.Send(new FilterCustomerDiscountsQuery(filter));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get CustomerDiscount Details

    [HttpGet(AdminDiscountApiEndpoints.CustomerDiscount.GetCustomerDiscountDetails)]
    [SwaggerOperation(Summary = "دریافت تخفیف مشتری", Tags = new[] { "AdminCustomerDiscount" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> GetCustomerDiscountDetails([FromRoute] long id)
    {
        var res = await Mediator.Send(new GetCustomerDiscountDetailsQuery(id));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Define Customer Discount

    [HttpPost(AdminDiscountApiEndpoints.CustomerDiscount.DefineCustomerDiscount)]
    [SwaggerOperation(Summary = "تعریف تخفیف مشتری", Tags = new[] { "AdminCustomerDiscount" })]
    [SwaggerResponse(201, "success : created")]
    [SwaggerResponse(400, "error : discount exists for product")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> DefineCustomerDiscount([FromForm] DefineCustomerDiscountDto createRequest)
    {
        var res = await Mediator.Send(new DefineCustomerDiscountCommand(createRequest));

        return JsonApiResult.Created(res);
    }

    #endregion

    #region Edit Customer Discount

    [HttpPut(AdminDiscountApiEndpoints.CustomerDiscount.EditCustomerDiscount)]
    [SwaggerOperation(Summary = "ویرایش تخفیف مشتری", Tags = new[] { "AdminCustomerDiscount" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> EditCustomerDiscount([FromForm] EditCustomerDiscountDto editRequest)
    {
        var res = await Mediator.Send(new EditCustomerDiscountCommand(editRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Remove Customer Discount

    [HttpDelete(AdminDiscountApiEndpoints.CustomerDiscount.RemoveCustomerDiscount)]
    [SwaggerOperation(Summary = "حذف تخفیف مشتری", Tags = new[] { "AdminCustomerDiscount" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> RemoveCustomerDiscount([FromRoute] long id)
    {
        var res = await Mediator.Send(new RemoveCustomerDiscountCommand(id));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Check Product Has Customer Discount

    [HttpGet(AdminDiscountApiEndpoints.CustomerDiscount.CheckProductHasCustomerDiscount)]
    [SwaggerOperation(Summary = "چک کردن وجود تخفیف مشتری برای محصول", Tags = new[] { "AdminCustomerDiscount" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> CheckProductHasCustomerDiscount([FromRoute] long productId)
    {
        var res = await Mediator.Send(new CheckProductHasCustomerDiscountQuery(productId));

        return JsonApiResult.Success(res);
    }

    #endregion
}
