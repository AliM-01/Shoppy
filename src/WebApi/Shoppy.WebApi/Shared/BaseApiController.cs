using AM.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.DependencyInjection;

namespace Shoppy.WebApi.Shared;

[ApiController]
[EnableCors("CorsPolicy")]
public abstract class BaseApiController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    #region SuccessResult

    protected OkObjectResult SuccessResult()
    {
        return new OkObjectResult(JsonSerializer.Serialize(ApiResponse.Success()));
    }

    protected OkObjectResult SuccessResult(ApiResult response)
    {
        return new OkObjectResult(JsonSerializer.Serialize(response));
    }

    protected OkObjectResult SuccessResult<TData>(ApiResult<TData> response)
    {
        string res = JsonSerializer.Serialize(response.Data);

        return new OkObjectResult(res);
    }

    #endregion

    #region ErrorResult

    protected BadRequestObjectResult ErrorResult(string msg = "عملیات با خطا مواجه شد")
    {
        return new BadRequestObjectResult(JsonSerializer.Serialize(ApiResponse.Error(msg)));
    }

    protected BadRequestObjectResult ErrorResult<T>(ApiResult<T> response)
    {
        string res = JsonSerializer.Serialize(response);

        return new BadRequestObjectResult(res);
    }
    #endregion
}

[ApiController]
[EnableCors("CorsPolicy")]
//[Authorize(Policy = RoleConstants.Admin)]
public abstract class BaseAdminApiController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}

[ApiController]
[EnableCors("CorsPolicy")]
[Authorize(Policy = RoleConstants.BasicUser)]
public abstract class BaseUserApiController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}