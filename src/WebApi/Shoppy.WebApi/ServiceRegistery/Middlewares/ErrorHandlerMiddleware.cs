using _0_Framework.Api;
using _0_Framework.Application.Exceptions;
using _0_Framework.Application.Wrappers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Shoppy.WebApi.ServiceRegistery.Middlewares;

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
                    break;
            }

            if (apiResult.Status == 500)
                if (env.IsDevelopment())
                    apiResult = ApiResponse.InternalServerError($"Internal Server Error : {error.Message}");
                else
                    apiResult = ApiResponse.InternalServerError();

            var response = context.Response;

            response.StatusCode = apiResult.Status;

            if (apiResult.Status == 204)
                return;

            response.ContentType = "application/json";

            await response.WriteAsync(JsonSerializer.Serialize(apiResult));
        }
    }
}