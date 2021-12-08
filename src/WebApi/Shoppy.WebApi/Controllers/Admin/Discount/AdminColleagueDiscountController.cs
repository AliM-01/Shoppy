﻿using DM.Application.Contracts.ColleagueDiscount.Commands;
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

    #region Define Colleague Discount

    /// <summary>
    ///    تعریف تخفیف همکار
    /// </summary>
    /// <response code="200">Success</response>
    [HttpPost(AdminDiscountApiEndpoints.ColleagueDiscount.DefineColleagueDiscount)]
    public async Task<IActionResult> DefineColleagueDiscount([FromForm] DefineColleagueDiscountDto createRequest)
    {
        var res = await Mediator.Send(new DefineColleagueDiscountCommand(createRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Edit Colleague Discount

    /// <summary>
    ///    ویرایش تخفیف مشتری
    /// </summary>
    /// <response code="200">Success</response>
    [HttpPut(AdminDiscountApiEndpoints.ColleagueDiscount.EditColleagueDiscount)]
    public async Task<IActionResult> EditColleagueDiscount([FromForm] EditColleagueDiscountDto editRequest)
    {
        var res = await Mediator.Send(new EditColleagueDiscountCommand(editRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

}
