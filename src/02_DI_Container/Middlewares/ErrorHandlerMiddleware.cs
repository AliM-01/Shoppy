using _0_Framework.Api;
using _0_Framework.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Net;

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
            var response = context.Response;
            response.ContentType = "application/json";
            var status = "error";
            var errors = new List<string>();
            var errorMessage = error.Message;

            errors.Add(errorMessage);

            switch (error)
            {
                case ApiException e:
                    // custom application error
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorMessage = e.Message;
                    break;

                case NotFoundApiException e:
                    // custom not-found application error
                    status = "not-found";
                    errorMessage = e.Message;
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                case NoContentApiException e:
                    // custom no-content application error
                    errorMessage = e.Message;
                    status = "no-content";
                    response.StatusCode = (int)HttpStatusCode.NoContent;
                    break;

                case ValidationException e:
                    // custom application error
                    errorMessage = e.Message;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    for (int i = 0; i < e.Errors.Count; i++)
                    {
                        errors.Add(e.Errors[i].ToString());
                    }
                    break;

                case SecurityTokenExpiredException e:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    errorMessage = "token expired";
                    break;

                default:
                    // unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = CustonJsonConverter.Serialize(new
            {
                status = status,
                message = errorMessage,
                errors = errors
            });

            await response.WriteAsync(result);
        }
    }
}