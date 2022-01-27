using _0_Framework.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

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
            var errorMessage = error?.Message;

            errors.Add(error.Message);

            switch (error)
            {
                case ApiException e:
                    // custom application error
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case NotFoundApiException e:
                    // custom not-found application error
                    status = "not-found";
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                case NoContentApiException e:
                    // custom no-content application error
                    status = "no-content";
                    response.StatusCode = (int)HttpStatusCode.NoContent;
                    break;

                case ValidationException e:
                    // custom application error
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errors.AddRange(e.Errors);
                    break;

                default:
                    // unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(new
            {
                status = status,
                message = errorMessage,
                errors = errors
            });

            await response.WriteAsync(result);
        }
    }
}