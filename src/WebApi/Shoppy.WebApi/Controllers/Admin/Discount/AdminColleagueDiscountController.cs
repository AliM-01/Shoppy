using DM.Application.Contracts.ColleagueDiscount.DTOs;
using DM.Application.Contracts.ColleagueDiscount.Queries;

namespace Shoppy.WebApi.Controllers.Admin.Discount;

public class AdminColleagueDiscountController : BaseApiController
{
    #region Filter Colleague Discounts

    /// <summary>
    ///    فیلتر تخفیفات همکار
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(AdminDiscountApiEndpoints.ColleagueDiscount.FilterColleagueDiscounts)]
    public async Task<IActionResult> FilterColleagueDiscounts([FromQuery] FilterColleagueDiscountDto filter)
    {
        var res = await Mediator.Send(new FilterColleagueDiscountsQuery(filter));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get ColleagueDiscount Details

    /// <summary>
    ///    دریافت تخفیف همکار
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(AdminDiscountApiEndpoints.ColleagueDiscount.GetColleagueDiscountDetails)]
    public async Task<IActionResult> GetColleagueDiscountDetails([FromRoute] long id)
    {
        var res = await Mediator.Send(new GetColleagueDiscountDetailsQuery(id));

        return JsonApiResult.Success(res);
    }

    #endregion

}
