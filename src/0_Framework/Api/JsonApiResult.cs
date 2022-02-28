using _0_Framework.Application.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace _0_Framework.Api;

public static class JsonApiResult
{
    #region Success

    public static OkObjectResult Success<T>(Response<T> response)
    {
        var res = CustonJsonConverter.Serialize(response);

        return new OkObjectResult(res);
    }

    #endregion

    #region Created

    public static CreatedResult Created<T>(Response<T> response)
    {
        var res = CustonJsonConverter.Serialize(response);

        return new CreatedResult("", res);
    }

    #endregion

    #region Error

    public static BadRequestObjectResult Error(string msg = "عملیات با خطا مواجه شد")
    {
        var res = CustonJsonConverter.Serialize(new
        {
            status = "error",
            message = msg
        });

        return new BadRequestObjectResult(res);
    }

    public static BadRequestObjectResult Error<T>(Response<T> response)
    {
        var res = CustonJsonConverter.Serialize(response);

        return new BadRequestObjectResult(res);
    }

    #endregion

    #region Unauthorized

    public static UnauthorizedObjectResult Unauthorized(string msg = "لطفا به حساب کاربری خود وارد شوید")
    {
        var res = CustonJsonConverter.Serialize(new
        {
            status = "un-authorized",
            message = msg
        });

        return new UnauthorizedObjectResult(res);
    }

    #endregion
}