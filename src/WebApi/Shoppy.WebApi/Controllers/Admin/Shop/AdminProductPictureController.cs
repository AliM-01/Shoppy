using Microsoft.AspNetCore.Http;
using SM.Application.Contracts.ProductPicture.Commands;
using SM.Application.Contracts.ProductPicture.DTOs;
using SM.Application.Contracts.ProductPicture.Queries;

namespace Shoppy.WebApi.Controllers.Admin.Shop;

[SwaggerTag("مدیریت تصایر محصول")]
public class AdminProductPictureController : BaseApiController
{
    #region Get Product Pictures

    [HttpGet(AdminShopApiEndpoints.ProductPicture.GetProductPictures)]
    [SwaggerOperation(Summary = "دریافت تصاویر محصولات", Tags = new[] { "AdminProductPicture" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> GetProductPictures([FromRoute] string ProductId)
    {
        var res = await Mediator.Send(new GetProductPicturesQuery(productId));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Create Product Picture

    [HttpPost(AdminShopApiEndpoints.ProductPicture.CreateProductPicture)]
    [SwaggerOperation(Summary = "ایجاد تصویر محصول", Tags = new[] { "AdminProductPicture" })]
    [SwaggerResponse(201, "success : created")]
    public async Task<IActionResult> CreateProductPicture([FromRoute] string ProductId)
    {
        var request = HttpContext.Request.Form;

        var files = request.Files;

        var createData = new CreateProductPictureDto
        {
            ImageFiles = (System.Collections.Generic.List<IFormFile>)files,
            ProductId = ProductId
        };

        var res = await Mediator.Send(new CreateProductPictureCommand(createData));

        return JsonApiResult.Created(res);
    }

    #endregion

    #region Remove Product Picture

    [HttpDelete(AdminShopApiEndpoints.ProductPicture.RemoveProductPicture)]
    [SwaggerOperation(Summary = "حذف تصویر محصول", Tags = new[] { "AdminProductPicture" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> RemoveProductPicture([FromRoute] string id)
    {
        var res = await Mediator.Send(new RemoveProductPictureCommand(id));

        return JsonApiResult.Success(res);
    }

    #endregion
}