using _0_Framework.Application.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace _0_Framework.Presentation.Http.JsonApiResult;

public static class JsonApiResult
{
    #region Success

    public static OkObjectResult Success<T>(Response<T> response)
    {
        var res = JsonConvert.SerializeObject(response);

        return new OkObjectResult(res);
    }

    #endregion

    #region Created

    public static CreatedResult Created<T>(Response<T> response)
    {
        var res = JsonConvert.SerializeObject(response);

        return new CreatedResult("", res);
    }

    #endregion

    #region Error

    public static BadRequestObjectResult Error()
    {
        var res = JsonConvert.SerializeObject(new
        {
            status = "error",
            message = "عملیات با خطا مواجه شد"
        });

        return new BadRequestObjectResult(res);
    }

    public static BadRequestObjectResult Error<T>(Response<T> response)
    {
        var res = JsonConvert.SerializeObject(response);

        return new BadRequestObjectResult(res);
    }

    #endregion

}