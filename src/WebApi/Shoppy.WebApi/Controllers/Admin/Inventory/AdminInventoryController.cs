using IM.Application.Contracts.Inventory.Commands;
using IM.Application.Contracts.Inventory.DTOs;
using IM.Application.Contracts.Inventory.Queries;

namespace Shoppy.WebApi.Controllers.Admin.Discount;

[SwaggerTag("مدیریت انبار")]
public class AdminInventoryController : BaseAdminApiController
{
    #region Filter Inventory

    [HttpGet(AdminInventoryApiEndpoints.Inventory.FilterInventories)]
    [SwaggerOperation(Summary = "فیلتر انبار", Tags = new[] { "AdminInventory" })]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> FilterInventories([FromQuery] FilterInventoryDto filter)
    {
        var res = await Mediator.Send(new FilterInventoryQuery(filter));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get Inventory Details

    [HttpGet(AdminInventoryApiEndpoints.Inventory.GetInventoryDetails)]
    [SwaggerOperation(Summary = "دریافت انبار", Tags = new[] { "AdminInventory" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> GetInventoryDetails([FromRoute] string id)
    {
        var res = await Mediator.Send(new GetInventoryDetailsQuery(id));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Create Inventory

    [HttpPost(AdminInventoryApiEndpoints.Inventory.CreateInventory)]
    [SwaggerOperation(Summary = "ایجاد انبار", Tags = new[] { "AdminInventory" })]
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
    [SwaggerOperation(Summary = "ویرایش انبار", Tags = new[] { "AdminInventory" })]
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
    [SwaggerOperation(Summary = "افزایش موجودی انبار", Tags = new[] { "AdminInventory" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> IncreaseInventory([FromForm] IncreaseInventoryDto increaseRequest)
    {
        var res = await Mediator.Send(new IncreaseInventoryCommand(increaseRequest, User.GetUserId()));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Reduce Inventory

    [HttpPost(AdminInventoryApiEndpoints.Inventory.ReduceInventory)]
    [SwaggerOperation(Summary = "کاهش موجودی انبار", Tags = new[] { "AdminInventory" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> ReduceInventory([FromForm] ReduceInventoryDto reduceRequest)
    {
        var res = await Mediator.Send(new ReduceInventoryCommand(reduceRequest, User.GetUserId()));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get Inventory Operation Log

    [HttpGet(AdminInventoryApiEndpoints.Inventory.GetInventoryOperationLog)]
    [SwaggerOperation(Summary = "دریافت لاگ های انبار", Tags = new[] { "AdminInventory" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> GetInventoryOperationLog([FromRoute] string id)
    {
        var res = await Mediator.Send(new GetInventoryOperationLogQuery(id));

        return JsonApiResult.Success(res);
    }

    #endregion

}
