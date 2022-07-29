using Microsoft.AspNetCore.Http;
using SM.Application.Contracts.ProductPicture.Commands;
using SM.Application.Contracts.ProductPicture.DTOs;
using SM.Application.Contracts.ProductPicture.Queries;

namespace Shoppy.WebApi.Controllers.Admin.Shop;

[SwaggerTag("مدیریت تصایر محصول")]
public class AdminProductPictureController : BaseAdminApiController
{
    #region Get Product Pictures

    [HttpGet(AdminShopEndpoints.ProductPicture.GetProductPictures)]
    [SwaggerOperation(Summary = "دریافت تصاویر محصولات", Tags = new[] { "AdminProductPicture" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(IEnumerable<ProductPictureDto>), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> GetProductPictures([FromRoute] string productId)
    {
        var res = await Mediator.Send(new GetProductPicturesQuery(productId));

        return SuccessResult(res);
    }

    #endregion

    #region Create Product Picture

    [HttpPost(AdminShopEndpoints.ProductPicture.CreateProductPicture)]
    [SwaggerOperation(Summary = "ایجاد تصویر محصول", Tags = new[] { "AdminProductPicture" })]
    [SwaggerResponse(201, "success : created")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ApiResult), 201)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> CreateProductPicture([FromRoute] string productId)
    {
        var request = HttpContext.Request.Form;

        var files = request.Files;

        var createData = new CreateProductPictureDto { ImageFiles = (List<IFormFile>)files, ProductId = productId };

        var res = await Mediator.Send(new CreateProductPictureCommand(createData));

        return CreatedResult(res);
    }

    #endregion

    #region Remove Product Picture

    [HttpDelete(AdminShopEndpoints.ProductPicture.RemoveProductPicture)]
    [SwaggerOperation(Summary = "حذف تصویر محصول", Tags = new[] { "AdminProductPicture" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> RemoveProductPicture([FromRoute] string id)
    {
        var res = await Mediator.Send(new RemoveProductPictureCommand(id));

        return SuccessResult(res);
    }

    #endregion
}