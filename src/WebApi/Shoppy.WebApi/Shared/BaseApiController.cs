using MediatR;
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

    protected OkObjectResult SuccessResult(object obj)
    {
        return new OkObjectResult(JsonSerializer.Serialize(obj));
    }

    #endregion

    #region CreatedResult

    protected CreatedResult CreatedResult(string msg)
    {
        string res = JsonSerializer.Serialize(new ApiResult(201, msg));

        return new CreatedResult("", res);
    }

    protected CreatedResult CreatedResult(ApiResult response)
    {
        string res = JsonSerializer.Serialize(response);

        return new CreatedResult("", res);
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

    #region UnauthorizedResult

    public static UnauthorizedObjectResult UnauthorizedResult(string msg = "لطفا به حساب کاربری خود وارد شوید")
    {
        return new UnauthorizedObjectResult(JsonSerializer.Serialize(ApiResponse.Unauthorized(msg)));
    }

    #endregion
}

//[Authorize(Policy = RoleConstants.Admin)]
public abstract class BaseAdminApiController : BaseApiController
{
}

//[Authorize(Policy = RoleConstants.BasicUser)]
public abstract class BaseUserApiController : BaseApiController
{
}