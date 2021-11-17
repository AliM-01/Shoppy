using Microsoft.AspNetCore.Mvc;
using Shoppy.Admin.WebApi.Endpoints;
using SM.Application.Contracts.ProductPicture.Commands;
using SM.Application.Contracts.ProductPicture.DTOs;
using SM.Application.Contracts.ProductPicture.Queries;

namespace Shoppy.Admin.WebApi.Controllers;
public class ProductPictureController : BaseApiController
{
    /// <summary>
    ///    فیلتر تصاویر محصولات
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(ApiEndpoints.ProductPicture.FilterProductPictures)]
    public async Task<IActionResult> FilterProductPictures([FromQuery] FilterProductPictureDto filter)
    {
        var res = await Mediator.Send(new FilterProductPicturesQuery(filter));

        return JsonApiResult.Success(res);
    }

    /// <summary>
    ///    دریافت جزییات تصویر محصول
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(ApiEndpoints.ProductPicture.GetProductPictureDetails)]
    public async Task<IActionResult> GetProductPictureDetails([FromRoute] long id)
    {
        var res = await Mediator.Send(new GetProductPictureDetailsQuery(id));

        return JsonApiResult.Success(res);
    }

    /// <summary>
    ///    ایجاد تصویر محصول
    /// </summary>
    /// <response code="200">Success</response>
    [HttpPost(ApiEndpoints.ProductPicture.CreateProductPicture)]
    public async Task<IActionResult> CreateProductPicture([FromForm] CreateProductPictureDto createRequest)
    {
        var res = await Mediator.Send(new CreateProductPictureCommand(createRequest));

        return JsonApiResult.Success(res);
    }
}