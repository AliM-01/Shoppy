using SM.Application.Contracts.ProductFeature.Commands;
using SM.Application.Contracts.ProductFeature.DTOs;
using SM.Application.Contracts.ProductFeature.Queries;

namespace Shoppy.WebApi.Controllers.Admin.Shop;

[SwaggerTag("مدیریت ویژگی محصولات")]
public class AdminProductFeatureController : BaseAdminApiController
{
    #region Filter Product Features

    [HttpGet(AdminShopApiEndpoints.ProductFeature.FilterProductFeatures)]
    [SwaggerOperation(Summary = "فیلتر ویژگی محصولات", Tags = new[] { "AdminProductFeature" })]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> FilterProductFeatures([FromQuery] FilterProductFeatureDto filter)
    {
        var res = await Mediator.Send(new FilterProductFeaturesQuery(filter));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get Product Feature Details

    [HttpGet(AdminShopApiEndpoints.ProductFeature.GetProductFeatureDetails)]
    [SwaggerOperation(Summary = "دریافت جزییات ویژگی محصول", Tags = new[] { "AdminProductFeature" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> GetProductFeatureDetails([FromRoute] string id)
    {
        var res = await Mediator.Send(new GetProductFeatureDetailsQuery(id));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Create Product Feature

    [HttpPost(AdminShopApiEndpoints.ProductFeature.CreateProductFeature)]
    [SwaggerOperation(Summary = "ایجاد ویژگی محصول", Tags = new[] { "AdminProductFeature" })]
    [SwaggerResponse(201, "success : created")]
    [SwaggerResponse(400, "error : title is duplicated")]
    public async Task<IActionResult> CreateProductFeature([FromForm] CreateProductFeatureDto createRequest)
    {
        var res = await Mediator.Send(new CreateProductFeatureCommand(createRequest));

        return JsonApiResult.Created(res);
    }

    #endregion

    #region Edit Product Feature

    [HttpPut(AdminShopApiEndpoints.ProductFeature.EditProductFeature)]
    [SwaggerOperation(Summary = "ویرایش ویژگی محصول", Tags = new[] { "AdminProductFeature" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(400, "error : title is duplicated")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> EditProductFeature([FromForm] EditProductFeatureDto editRequest)
    {
        var res = await Mediator.Send(new EditProductFeatureCommand(editRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Delete Product Feature

    [HttpDelete(AdminShopApiEndpoints.ProductFeature.DeleteProductFeature)]
    [SwaggerOperation(Summary = "حذف ویژگی محصول", Tags = new[] { "AdminProductFeature" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> DeleteProductFeature([FromRoute] string id)
    {
        var res = await Mediator.Send(new DeleteProductFeatureCommand(id));

        return JsonApiResult.Success(res);
    }

    #endregion
}