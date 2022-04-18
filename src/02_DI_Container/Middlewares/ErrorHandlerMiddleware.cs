using _0_Framework.Api;
using _0_Framework.Application.Exceptions;
using _0_Framework.Application.Wrappers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace _02_DI_Container.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IWebHostEnvironment env)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var apiResult = ApiResponse.InternalServerError();

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
                    apiResult = ApiResponse.Unauthorized();
                    break;

                case OperationCanceledException:
                    apiResult = ApiResponse.ClientClosedRequest();
                    break;

                default:
                    if (env.IsDevelopment())
                    {
                        apiResult = ApiResponse.InternalServerError($"Internal Server Error : {error.Message}");
                    }
                    else
                    {
                        apiResult = ApiResponse.InternalServerError();
                    }
                    break;
            }

            var response = context.Response;

            response.ContentType = "application/json";
            response.StatusCode = apiResult.Status;

            await response.WriteAsync(JsonSerializer.Serialize(apiResult));
        }
    }
}