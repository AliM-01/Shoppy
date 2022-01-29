using SM.Application.Contracts.Product.Commands;
using SM.Application.Contracts.Product.DTOs;
using SM.Application.Contracts.Product.Queries;

namespace Shoppy.WebApi.Controllers.Admin.Shop;

[SwaggerTag("مدیریت محصولات")]
public class AdminProductController : BaseApiController
{
    #region Filter Products

    [HttpGet(AdminShopApiEndpoints.Product.FilterProducts)]
    [SwaggerOperation(Summary = "فیلتر محصولات")]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> FilterProducts([FromQuery] FilterProductDto filter)
    {
        var res = await Mediator.Send(new FilterProductsQuery(filter));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Exists Product Id

    [HttpGet(AdminShopApiEndpoints.Product.ExistsProductId)]
    [SwaggerOperation(Summary = "چک کردن وجود شناسه محصول")]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> ExistsProductId([FromRoute] long id)
    {
        var res = await Mediator.Send(new ExistsProductIdQuery(id));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get Product Details

    [HttpGet(AdminShopApiEndpoints.Product.GetProductDetails)]
    [SwaggerOperation(Summary = "دریافت جزییات محصول")]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> GetProductDetails([FromRoute] long id)
    {
        var res = await Mediator.Send(new GetProductDetailsQuery(id));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Create Product

    [HttpPost(AdminShopApiEndpoints.Product.CreateProduct)]
    [SwaggerOperation(Summary = "ایجاد محصول")]
    [SwaggerResponse(201, "success : created")]
    [SwaggerResponse(400, "error : title is duplicated")]
    public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto createRequest)
    {
        var res = await Mediator.Send(new CreateProductCommand(createRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Edit Product

    [HttpPut(AdminShopApiEndpoints.Product.EditProduct)]
    [SwaggerOperation(Summary = "ویرایش محصول")]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(400, "error : title is duplicated")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> EditProduct([FromForm] EditProductDto editRequest)
    {
        var res = await Mediator.Send(new EditProductCommand(editRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Delete Product

    [HttpDelete(AdminShopApiEndpoints.Product.DeleteProduct)]
    [SwaggerOperation(Summary = "حذف محصول")]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> DeleteProduct([FromRoute] long id)
    {
        var res = await Mediator.Send(new DeleteProductCommand(id));

        return JsonApiResult.Success(res);
    }

    #endregion
}
