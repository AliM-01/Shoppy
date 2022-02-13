using _0_Framework.Application.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace _0_Framework.Presentation.Http.JsonApiResult;

public static class JsonApiResult
{
    #region Success

    public static OkObjectResult Success<T>(Response<T> response)
    {
        var res = JsonConvert.SerializeObject(response, Formatting.Indented);

        return new OkObjectResult(res);
    }

    #endregion

    #region Created

    public static CreatedResult Created<T>(Response<T> response)
    {
        var res = JsonConvert.SerializeObject(response, Formatting.Indented);

        return new CreatedResult("", res);
    }

    #endregion

    #region Error

    public static BadRequestObjectResult Error(string msg = "عملیات با خطا مواجه شد")
    {
        var res = JsonConvert.SerializeObject(new
        {
            status = "error",
            message = msg
        }, Formatting.Indented);

        return new BadRequestObjectResult(res);
    }

    public static BadRequestObjectResult Error<T>(Response<T> response)
    {
        var res = JsonConvert.SerializeObject(response, Formatting.Indented);

        return new BadRequestObjectResult(res);
    }

    #endregion

}