using SM.Application.Contracts.ProductFeature.Commands;
using SM.Application.Contracts.ProductFeature.DTOs;
using SM.Application.Contracts.ProductFeature.Queries;

namespace Shoppy.WebApi.Controllers.Admin.Shop;
public class AdminProductFeatureController : BaseApiController
{
    #region Filter Product Features

    /// <summary>
    ///    فیلتر ویژگی محصولات
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(AdminShopApiEndpoints.ProductFeature.FilterProductFeatures)]
    public async Task<IActionResult> FilterProductFeatures([FromQuery] FilterProductFeatureDto filter)
    {
        var res = await Mediator.Send(new FilterProductFeaturesQuery(filter));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get Product Feature Details

    /// <summary>
    ///    دریافت جزییات ویژگی محصول
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(AdminShopApiEndpoints.ProductFeature.GetProductFeatureDetails)]
    public async Task<IActionResult> GetProductFeatureDetails([FromRoute] long id)
    {
        var res = await Mediator.Send(new GetProductFeatureDetailsQuery(id));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Create Product Feature

    /// <summary>
    ///    ایجاد ویژگی محصول
    /// </summary>
    /// <response code="200">Success</response>
    [HttpPost(AdminShopApiEndpoints.ProductFeature.CreateProductFeature)]
    public async Task<IActionResult> CreateProductFeature([FromForm] CreateProductFeatureDto createRequest)
    {
        var res = await Mediator.Send(new CreateProductFeatureCommand(createRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Edit Product Feature

    /// <summary>
    ///    ویرایش ویژگی محصول
    /// </summary>
    /// <response code="200">Success</response>
    [HttpPut(AdminShopApiEndpoints.ProductFeature.EditProductFeature)]
    public async Task<IActionResult> EditProductFeature([FromForm] EditProductFeatureDto editRequest)
    {
        var res = await Mediator.Send(new EditProductFeatureCommand(editRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Delete Product Feature

    /// <summary>
    ///    حذف ویژگی محصول
    /// </summary>
    /// <response code="200">Success</response>
    [HttpDelete(AdminShopApiEndpoints.ProductFeature.DeleteProductFeature)]
    public async Task<IActionResult> DeleteProductFeature([FromRoute] long id)
    {
        var res = await Mediator.Send(new DeleteProductFeatureCommand(id));

        return JsonApiResult.Success(res);
    }

    #endregion
}