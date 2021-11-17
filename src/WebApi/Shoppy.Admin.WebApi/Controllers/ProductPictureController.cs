using Microsoft.AspNetCore.Mvc;
using Shoppy.Admin.WebApi.Endpoints;
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
}