using IM.Application.Inventory.Commands;
using IM.Application.Inventory.DTOs;
using SM.Application.Product.Commands;
using SM.Application.Product.DTOs.Admin;
using SM.Application.Product.Queries;
using SM.Application.Product.Queries.Admin;
using SM.Application.Product.Queries.Site;

namespace ServiceHost.Controllers.Admin.Shop;

[SwaggerTag("مدیریت محصولات")]
public class AdminProductController : BaseAdminApiController
{
    #region Filter Products

    [HttpGet(AdminShopEndpoints.Product.FilterProducts)]
    [SwaggerOperation(Summary = "فیلتر محصولات", Tags = new[] { "AdminProduct" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(FilterProductDto), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> FilterProducts([FromQuery] FilterProductDto filter)
    {
        var res = await Mediator.Send(new FilterProductsQuery(filter));

        return SuccessResult(res);
    }

    #endregion

    #region Exists Product Id

    [HttpGet(AdminShopEndpoints.Product.ExistsProductId)]
    [SwaggerOperation(Summary = "چک کردن وجود شناسه محصول", Tags = new[] { "AdminProduct" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ExistsProductIdResponseDto), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> ExistsProductid([FromRoute] string id)
    {
        var res = await Mediator.Send(new ExistsProductIdQuery(id));

        return SuccessResult(res);
    }

    #endregion

    #region Get Product Details

    [HttpGet(AdminShopEndpoints.Product.GetProductDetails)]
    [SwaggerOperation(Summary = "دریافت جزییات محصول", Tags = new[] { "AdminProduct" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(EditProductDto), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> GetProductDetails([FromRoute] string id)
    {
        var res = await Mediator.Send(new GetProductDetailsSiteQuery(id));

        return SuccessResult(res);
    }

    #endregion

    #region Create Product

    [HttpPost(AdminShopEndpoints.Product.CreateProduct)]
    [SwaggerOperation(Summary = "ایجاد محصول", Tags = new[] { "AdminProduct" })]
    [SwaggerResponse(201, "success : created")]
    [SwaggerResponse(400, "error : title is duplicated")]
    [ProducesResponseType(typeof(ApiResult), 201)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto createRequest)
    {
        var res = await Mediator.Send(new CreateProductCommand(createRequest));

        await Mediator.Send(
            new CreateInventoryCommand(new CreateInventoryDto { ProductId = res.ProductId, UnitPrice = 0 }));

        return CreatedResult("محصول با موفقیت ساخته شد");
    }

    #endregion

    #region Edit Product

    [HttpPut(AdminShopEndpoints.Product.EditProduct)]
    [SwaggerOperation(Summary = "ویرایش محصول", Tags = new[] { "AdminProduct" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(400, "error : title is duplicated")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    [ProducesResponseType(typeof(ApiResult), 400)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> EditProduct([FromForm] EditProductDto editRequest)
    {
        var res = await Mediator.Send(new EditProductCommand(editRequest));

        return SuccessResult(res);
    }

    #endregion

    #region Delete Product

    [HttpDelete(AdminShopEndpoints.Product.DeleteProduct)]
    [SwaggerOperation(Summary = "حذف محصول", Tags = new[] { "AdminProduct" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> DeleteProduct([FromRoute] string id)
    {
        var res = await Mediator.Send(new DeleteProductCommand(id));

        return SuccessResult(res);
    }

    #endregion
}