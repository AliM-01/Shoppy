﻿using Microsoft.AspNetCore.Mvc;
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

    /// <summary>
    ///    ویرایش تصویر محصول
    /// </summary>
    /// <response code="200">Success</response>
    [HttpPut(ApiEndpoints.ProductPicture.EditProductPicture)]
    public async Task<IActionResult> EditProductPicture([FromForm] EditProductPictureDto editRequest)
    {
        var res = await Mediator.Send(new EditProductPictureCommand(editRequest));

        return JsonApiResult.Success(res);
    }

    /// <summary>
    ///    غیر فعال کردن تصویر محصول
    /// </summary>
    /// <response code="200">Success</response>
    [HttpDelete(ApiEndpoints.ProductPicture.RemoveProductPicture)]
    public async Task<IActionResult> RemoveProductPicture([FromRoute] long id)
    {
        var res = await Mediator.Send(new RemoveProductPictureCommand(id));

        return JsonApiResult.Success(res);
    }

    /// <summary>
    ///    فعال کردن تصویر محصول
    /// </summary>
    /// <response code="200">Success</response>
    [HttpPut(ApiEndpoints.ProductPicture.RestoreProductPicture)]
    public async Task<IActionResult> RestoreProductPicture([FromRoute] long id)
    {
        var res = await Mediator.Send(new RestoreProductPictureCommand(id));

        return JsonApiResult.Success(res);
    }
}