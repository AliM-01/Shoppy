using DM.Application.Contracts.CustomerDiscount.Commands;
using DM.Application.Contracts.CustomerDiscount.DTOs;
using DM.Application.Contracts.CustomerDiscount.Queries;

namespace Shoppy.WebApi.Controllers.Admin.Discount;

public class AdminCustomerDiscountController : BaseApiController
{
    #region Filter Customer Discounts

    /// <summary>
    ///    فیلتر تخفیفات مشتری
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(AdminDiscountApiEndpoints.CustomerDiscount.FilterCustomerDiscounts)]
    public async Task<IActionResult> FilterCustomerDiscounts([FromQuery] FilterCustomerDiscountDto filter)
    {
        var res = await Mediator.Send(new FilterCustomerDiscountsQuery(filter));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get CustomerDiscount Details

    /// <summary>
    ///    دریافت تخفیف مشتری
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(AdminDiscountApiEndpoints.CustomerDiscount.GetCustomerDiscountDetails)]
    public async Task<IActionResult> GetCustomerDiscountDetails([FromRoute] long id)
    {
        var res = await Mediator.Send(new GetCustomerDiscountDetailsQuery(id));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Create Customer Discount

    /// <summary>
    ///    ایجاد تخفیف مشتری
    /// </summary>
    /// <response code="200">Success</response>
    [HttpPost(AdminDiscountApiEndpoints.CustomerDiscount.CreateCustomerDiscount)]
    public async Task<IActionResult> CreateCustomerDiscount([FromForm] CreateCustomerDiscountDto createRequest)
    {
        var res = await Mediator.Send(new CreateCustomerDiscountCommand(createRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Edit Customer Discount

    /// <summary>
    ///    ویرایش تخفیف مشتری
    /// </summary>
    /// <response code="200">Success</response>
    [HttpPut(AdminDiscountApiEndpoints.CustomerDiscount.EditCustomerDiscount)]
    public async Task<IActionResult> EditCustomerDiscount([FromForm] EditCustomerDiscountDto editRequest)
    {
        var res = await Mediator.Send(new EditCustomerDiscountCommand(editRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Remove Customer Discount

    /// <summary>
    ///    حذف تخفیف مشتری
    /// </summary>
    /// <response code="200">Success</response>
    [HttpDelete(AdminDiscountApiEndpoints.CustomerDiscount.RemoveCustomerDiscount)]
    public async Task<IActionResult> RemoveCustomerDiscount([FromRoute] long id)
    {
        var res = await Mediator.Send(new RemoveCustomerDiscountCommand(id));

        return JsonApiResult.Success(res);
    }

    #endregion
}
