using IM.Application.Contracts.Inventory.DTOs;
using IM.Application.Contracts.Inventory.Queries;

namespace Shoppy.WebApi.Controllers.Admin.Discount;

public class AdminInventoryController : BaseApiController
{
    #region Filter Inventory

    /// <summary>
    ///    فیلتر انبار
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(AdminInventoryApiEndpoints.Inventory.FilterInventories)]
    public async Task<IActionResult> FilterInventories([FromQuery] FilterInventoryDto filter)
    {
        var res = await Mediator.Send(new FilterInventoryQuery(filter));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get Inventory Details

    /// <summary>
    ///    دریافت انبار
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(AdminInventoryApiEndpoints.Inventory.GetInventoryDetails)]
    public async Task<IActionResult> GetInventoryDetails([FromRoute] long id)
    {
        var res = await Mediator.Send(new GetInventoryDetailsQuery(id));

        return JsonApiResult.Success(res);
    }

    #endregion

}
