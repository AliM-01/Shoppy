using _0_Framework.Application.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace _0_Framework.Api;

public static class JsonApiResult
{
    #region Success

    public static OkObjectResult Success()
    {
        return new OkObjectResult(CustonJsonConverter.Serialize(ApiResponse.Success()));
    }

    public static OkObjectResult Success(ApiResult response)
    {
        return new OkObjectResult(CustonJsonConverter.Serialize(response));
    }

    public static OkObjectResult Success<T>(ApiResult<T> response)
    {
        var res = CustonJsonConverter.Serialize(response);

        return new OkObjectResult(res);
    }

    #endregion

    #region Created

    public static CreatedResult Created(string msg)
    {
        var res = CustonJsonConverter.Serialize(new ApiResult(201, msg));

        return new CreatedResult("", res);
    }

    public static CreatedResult Created(ApiResult response)
    {
        var res = CustonJsonConverter.Serialize(response);

        return new CreatedResult("", res);
    }

    #endregion

    #region Error

    public static BadRequestObjectResult Error(string msg = "عملیات با خطا مواجه شد")
    {
        return new BadRequestObjectResult(CustonJsonConverter.Serialize(ApiResponse.Error(msg)));
    }

    public static BadRequestObjectResult Error<T>(ApiResult<T> response)
    {
        var res = CustonJsonConverter.Serialize(response);

        return new BadRequestObjectResult(res);
    }

    #endregion

    #region Unauthorized

    public static UnauthorizedObjectResult Unauthorized(string msg = "لطفا به حساب کاربری خود وارد شوید")
    {
        return new UnauthorizedObjectResult(CustonJsonConverter.Serialize(ApiResponse.AccessDenied()));
    }

    #endregion
}