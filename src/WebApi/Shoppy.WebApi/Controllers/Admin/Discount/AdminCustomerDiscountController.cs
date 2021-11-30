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

}
