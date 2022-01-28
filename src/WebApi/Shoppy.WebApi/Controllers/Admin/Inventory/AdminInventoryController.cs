using IM.Application.Contracts.Inventory.Commands;
using IM.Application.Contracts.Inventory.DTOs;
using IM.Application.Contracts.Inventory.Queries;

namespace Shoppy.WebApi.Controllers.Admin.Discount;

[SwaggerTag("مدیریت انبار")]
public class AdminInventoryController : BaseApiController
{
    #region Filter Inventory

    [HttpGet(AdminInventoryApiEndpoints.Inventory.FilterInventories)]
    [SwaggerOperation(Summary = "فیلتر انبار")]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> FilterInventories([FromQuery] FilterInventoryDto filter)
    {
        var res = await Mediator.Send(new FilterInventoryQuery(filter));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get Inventory Details

    [HttpGet(AdminInventoryApiEndpoints.Inventory.GetInventoryDetails)]
    [SwaggerOperation(Summary = "دریافت انبار")]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> GetInventoryDetails([FromRoute] long id)
    {
        var res = await Mediator.Send(new GetInventoryDetailsQuery(id));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Create Inventory

    /// <summary>
    ///    ایجاد انبار
    /// </summary>
    /// <response code="200">Success</response>
    [HttpPost(AdminInventoryApiEndpoints.Inventory.CreateInventory)]
    [SwaggerOperation(Summary = "ایجاد انبار")]
    [SwaggerResponse(201, "success : created")]
    [SwaggerResponse(400, "error : discount exists for product")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> CreateInventory([FromForm] CreateInventoryDto createRequest)
    {
        var res = await Mediator.Send(new CreateInventoryCommand(createRequest));

        return JsonApiResult.Created(res);
    }

    #endregion

    #region Edit Inventory

    [HttpPut(AdminInventoryApiEndpoints.Inventory.EditInventory)]
    [SwaggerOperation(Summary = "ویرایش انبار")]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> EditInventory([FromForm] EditInventoryDto editRequest)
    {
        var res = await Mediator.Send(new EditInventoryCommand(editRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Increase Inventory

    [HttpPost(AdminInventoryApiEndpoints.Inventory.IncreaseInventory)]
    [SwaggerOperation(Summary = "افزایش موجودی انبار")]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> IncreaseInventory([FromForm] IncreaseInventoryDto increaseRequest)
    {
        var res = await Mediator.Send(new IncreaseInventoryCommand(increaseRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Reduce Inventory

    [HttpPost(AdminInventoryApiEndpoints.Inventory.ReduceInventory)]
    [SwaggerOperation(Summary = "کاهش موجودی انبار")]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> ReduceInventory([FromForm] ReduceInventoryDto reduceRequest)
    {
        var res = await Mediator.Send(new ReduceInventoryCommand(reduceRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get Inventory Operation Log

    [HttpGet(AdminInventoryApiEndpoints.Inventory.GetInventoryOperationLog)]
    [SwaggerOperation(Summary = "دریافت لاگ های انبار")]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> GetInventoryOperationLog([FromRoute] long id)
    {
        var res = await Mediator.Send(new GetInventoryOperationLogQuery(id));

        return JsonApiResult.Success(res);
    }

    #endregion

}
