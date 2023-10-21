using SM.Application.ProductFeature.Commands;
using SM.Application.ProductFeature.DTOs;
using SM.Application.ProductFeature.Queries;

namespace Shoppy.WebApi.Controllers.Admin.Shop;

[SwaggerTag("مدیریت ویژگی محصولات")]
public class AdminProductFeatureController : BaseAdminApiController
{
    #region Filter Product Features

    [HttpGet(AdminShopEndpoints.ProductFeature.FilterProductFeatures)]
    [SwaggerOperation(Summary = "فیلتر ویژگی محصولات", Tags = new[] { "AdminProductFeature" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(FilterProductFeatureDto), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> FilterProductFeatures([FromQuery] FilterProductFeatureDto filter)
    {
        var res = await Mediator.Send(new FilterProductFeaturesQuery(filter));

        return SuccessResult(res);
    }

    #endregion

    #region Get Product Feature Details

    [HttpGet(AdminShopEndpoints.ProductFeature.GetProductFeatureDetails)]
    [SwaggerOperation(Summary = "دریافت جزییات ویژگی محصول", Tags = new[] { "AdminProductFeature" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(EditProductFeatureDto), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> GetProductFeatureDetails([FromRoute] string id)
    {
        var res = await Mediator.Send(new GetProductFeatureDetailsQuery(id));

        return SuccessResult(res);
    }

    #endregion

    #region Create Product Feature

    [HttpPost(AdminShopEndpoints.ProductFeature.CreateProductFeature)]
    [SwaggerOperation(Summary = "ایجاد ویژگی محصول", Tags = new[] { "AdminProductFeature" })]
    [SwaggerResponse(201, "success : created")]
    [SwaggerResponse(400, "error : title is duplicated")]
    [ProducesResponseType(typeof(ApiResult), 201)]
    [ProducesResponseType(typeof(ApiResult), 400)]
    public async Task<IActionResult> CreateProductFeature([FromForm] CreateProductFeatureDto createRequest)
    {
        var res = await Mediator.Send(new CreateProductFeatureCommand(createRequest));

        return CreatedResult(res);
    }

    #endregion

    #region Edit Product Feature

    [HttpPut(AdminShopEndpoints.ProductFeature.EditProductFeature)]
    [SwaggerOperation(Summary = "ویرایش ویژگی محصول", Tags = new[] { "AdminProductFeature" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(400, "error : title is duplicated")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    [ProducesResponseType(typeof(ApiResult), 400)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> EditProductFeature([FromForm] EditProductFeatureDto editRequest)
    {
        var res = await Mediator.Send(new EditProductFeatureCommand(editRequest));

        return SuccessResult(res);
    }

    #endregion

    #region Delete Product Feature

    [HttpDelete(AdminShopEndpoints.ProductFeature.DeleteProductFeature)]
    [SwaggerOperation(Summary = "حذف ویژگی محصول", Tags = new[] { "AdminProductFeature" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> DeleteProductFeature([FromRoute] string id)
    {
        var res = await Mediator.Send(new DeleteProductFeatureCommand(id));

        return SuccessResult(res);
    }

    #endregion
}