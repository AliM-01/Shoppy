using IM.Application.Contracts.Inventory.Commands;
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

    #region Create Inventory

    /// <summary>
    ///    ایجاد انبار
    /// </summary>
    /// <response code="200">Success</response>
    [HttpPost(AdminInventoryApiEndpoints.Inventory.CreateInventory)]
    public async Task<IActionResult> CreateInventory([FromForm] CreateInventoryDto createRequest)
    {
        var res = await Mediator.Send(new CreateInventoryCommand(createRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Edit Inventory

    /// <summary>
    ///    ویرایش انبار
    /// </summary>
    /// <response code="200">Success</response>
    [HttpPut(AdminInventoryApiEndpoints.Inventory.EditInventory)]
    public async Task<IActionResult> EditInventory([FromForm] EditInventoryDto editRequest)
    {
        var res = await Mediator.Send(new EditInventoryCommand(editRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Increase Inventory

    /// <summary>
    ///    افزایش موجودی انبار
    /// </summary>
    /// <response code="200">Success</response>
    [HttpPost(AdminInventoryApiEndpoints.Inventory.IncreaseInventory)]
    public async Task<IActionResult> IncreaseInventory([FromRoute] IncreaseInventoryDto increaseRequest)
    {
        var res = await Mediator.Send(new IncreaseInventoryCommand(increaseRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Reduce Inventory

    /// <summary>
    ///    کاهش موجودی انبار
    /// </summary>
    /// <response code="200">Success</response>
    [HttpPost(AdminInventoryApiEndpoints.Inventory.ReduceInventory)]
    public async Task<IActionResult> ReduceInventory([FromRoute] ReduceInventoryDto reduceRequest)
    {
        var res = await Mediator.Send(new ReduceInventoryCommand(reduceRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

}
