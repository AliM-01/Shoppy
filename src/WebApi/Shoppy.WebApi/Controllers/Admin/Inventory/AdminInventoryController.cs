using IM.Application.Contracts.Inventory.Commands;
using IM.Application.Contracts.Inventory.DTOs;
using IM.Application.Contracts.Inventory.Queries;

namespace Shoppy.WebApi.Controllers.Admin.Discount;

[SwaggerTag("مدیریت انبار")]
public class AdminInventoryController : BaseAdminApiController
{
    #region Filter Inventory

    [HttpGet(AdminInventoryEndpoints.Inventory.FilterInventories)]
    [SwaggerOperation(Summary = "فیلتر انبار", Tags = new[] { "AdminInventory" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ApiResult<FilterInventoryDto>), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> FilterInventories([FromQuery] FilterInventoryDto filter)
    {
        var res = await Mediator.Send(new FilterInventoryQuery(filter));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get Inventory Details

    [HttpGet(AdminInventoryEndpoints.Inventory.GetInventoryDetails)]
    [SwaggerOperation(Summary = "دریافت انبار", Tags = new[] { "AdminInventory" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ApiResult<EditInventoryDto>), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> GetInventoryDetails([FromRoute] string id)
    {
        var res = await Mediator.Send(new GetInventoryDetailsQuery(id));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Edit Inventory

    [HttpPut(AdminInventoryEndpoints.Inventory.EditInventory)]
    [SwaggerOperation(Summary = "ویرایش انبار", Tags = new[] { "AdminInventory" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> EditInventory([FromForm] EditInventoryDto editRequest)
    {
        var res = await Mediator.Send(new EditInventoryCommand(editRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Increase Inventory

    [HttpPost(AdminInventoryEndpoints.Inventory.IncreaseInventory)]
    [SwaggerOperation(Summary = "افزایش موجودی انبار", Tags = new[] { "AdminInventory" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> IncreaseInventory([FromForm] IncreaseInventoryDto increaseRequest)
    {
        var res = await Mediator.Send(new IncreaseInventoryCommand(increaseRequest,
                                                                   User.GetUserId()));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Reduce Inventory

    [HttpPost(AdminInventoryEndpoints.Inventory.ReduceInventory)]
    [SwaggerOperation(Summary = "کاهش موجودی انبار", Tags = new[] { "AdminInventory" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> ReduceInventory([FromForm] ReduceInventoryDto reduceRequest)
    {
        var res = await Mediator.Send(new ReduceInventoryCommand(reduceRequest,
                                                                 User.GetUserId()));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get Inventory Operation Log

    [HttpGet(AdminInventoryEndpoints.Inventory.GetInventoryOperationLog)]
    [SwaggerOperation(Summary = "دریافت لاگ های انبار", Tags = new[] { "AdminInventory" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ApiResult<InventoryLogsDto>), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> GetInventoryOperationLog([FromRoute] string id)
    {
        var res = await Mediator.Send(new GetInventoryOperationLogQuery(id));

        return JsonApiResult.Success(res);
    }

    #endregion

}
