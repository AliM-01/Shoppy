using SM.Application.Contracts.ProductPicture.Commands;
using SM.Application.Contracts.ProductPicture.DTOs;
using SM.Application.Contracts.ProductPicture.Queries;

namespace Shoppy.WebApi.Controllers.Admin.Shop;
public class AdminProductPictureController : BaseApiController
{
    #region Get Product Pictures

    /// <summary>
    ///    دریافت تصاویر محصولات
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(AdminShopApiEndpoints.ProductPicture.GetProductPictures)]
    public async Task<IActionResult> GetProductPictures([FromRoute] long productId)
    {
        var res = await Mediator.Send(new GetProductPicturesQuery(productId));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Create Product Picture

    /// <summary>
    ///    ایجاد تصویر محصول
    /// </summary>
    /// <response code="200">Success</response>
    [HttpPost(AdminShopApiEndpoints.ProductPicture.CreateProductPicture)]
    public async Task<IActionResult> CreateProductPicture([FromForm] CreateProductPictureDto createRequest)
    {
        var res = await Mediator.Send(new CreateProductPictureCommand(createRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Remove Product Picture

    /// <summary>
    ///    غیر فعال کردن تصویر محصول
    /// </summary>
    /// <response code="200">Success</response>
    [HttpDelete(AdminShopApiEndpoints.ProductPicture.RemoveProductPicture)]
    public async Task<IActionResult> RemoveProductPicture([FromRoute] long id)
    {
        var res = await Mediator.Send(new RemoveProductPictureCommand(id));

        return JsonApiResult.Success(res);
    }

    #endregion
}