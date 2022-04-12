using _0_Framework.Api;
using _0_Framework.Application.Exceptions;
using _0_Framework.Application.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace _02_DI_Container.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            ApiResult apiResult = ApiResponse.InternalServerError();

            var response = context.Response;
            response.ContentType = "application/json";

            switch (error)
            {
                case ApiException e:
                    apiResult = ApiResponse.Error(e.Message);
                    break;

                case NotFoundApiException e:
                    apiResult = ApiResponse.NotFound(e.Message);
                    break;

                case NoContentApiException:
                    apiResult = ApiResponse.NoContent();
                    break;

                case ValidationException e:
                    apiResult = ApiResponse.Error(e.Message);
                    break;

                case SecurityTokenExpiredException:
                    apiResult = ApiResponse.AccessDenied();
                    break;

                case OperationCanceledException:
                    apiResult = ApiResponse.ClientClosedRequest();
                    break;

                default:
                    apiResult = ApiResponse.InternalServerError();
                    break;
            }

            var result = CustonJsonConverter.Serialize(apiResult);

            await response.WriteAsync(result);
        }
    }
}