using Shoppy.WebApi.Endpoints.Admin.Shop;
using SM.Application.Contracts.Product.Commands;
using SM.Application.Contracts.Product.DTOs;
using SM.Application.Contracts.Product.Queries;

namespace Shoppy.WebApi.Controllers.Admin.Shop;

public class AdminProductController : BaseApiController
{
    #region Filter Products

    /// <summary>
    ///    فیلتر محصولات
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(AdminShopApiEndpoints.Product.FilterProducts)]
    public async Task<IActionResult> FilterProducts([FromQuery] FilterProductDto filter)
    {
        var res = await Mediator.Send(new FilterProductsQuery(filter));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get Product Details

    /// <summary>
    ///    دریافت جزییات محصول
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(AdminShopApiEndpoints.Product.GetProductDetails)]
    public async Task<IActionResult> GetProductDetails([FromRoute] long id)
    {
        var res = await Mediator.Send(new GetProductDetailsQuery(id));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Create Product

    /// <summary>
    ///    ایجاد محصول
    /// </summary>
    /// <response code="200">Success</response>
    [HttpPost(AdminShopApiEndpoints.Product.CreateProduct)]
    public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto createRequest)
    {
        var res = await Mediator.Send(new CreateProductCommand(createRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Edit Product

    /// <summary>
    ///    ویرایش محصول
    /// </summary>
    /// <response code="200">Success</response>
    [HttpPut(AdminShopApiEndpoints.Product.EditProduct)]
    public async Task<IActionResult> EditProduct([FromForm] EditProductDto editRequest)
    {
        var res = await Mediator.Send(new EditProductCommand(editRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Delete Product

    /// <summary>
    ///    حذف محصول
    /// </summary>
    /// <response code="200">Success</response>
    [HttpDelete(AdminShopApiEndpoints.Product.DeleteProduct)]
    public async Task<IActionResult> DeleteProduct([FromRoute] long id)
    {
        var res = await Mediator.Send(new DeleteProductCommand(id));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Update Product Is In Stock

    /// <summary>
    ///    ثبت موجودی محصول
    /// </summary>
    /// <response code="200">Success</response>
    [HttpPut(AdminShopApiEndpoints.Product.UpdateProductIsInStock)]
    public async Task<IActionResult> UpdateProductIsInStock([FromRoute] long id)
    {
        var res = await Mediator.Send(new UpdateProductIsInStockCommand(id));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Update Product Not In Stock

    /// <summary>
    ///    ثبت نا موجودی محصول
    /// </summary>
    /// <response code="200">Success</response>
    [HttpDelete(AdminShopApiEndpoints.Product.UpdateProductNotInStock)]
    public async Task<IActionResult> UpdateProductNotInStock([FromRoute] long id)
    {
        var res = await Mediator.Send(new UpdateProductNotInStockCommand(id));

        return JsonApiResult.Success(res);
    }

    #endregion
}
