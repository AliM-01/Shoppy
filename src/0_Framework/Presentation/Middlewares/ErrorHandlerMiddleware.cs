using _0_Framework.Application.Exceptions;
using _0_Framework.Application.Wrappers;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace _0_Framework.Presentation.Middlewares;

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
            var responseModel = new Response<string>() { Status = "error", Message = error?.Message };

            errors.Add(error.Message);

            switch (error)
            {
                case ApiException e:
                    // custom application error
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseModel.Message = e.Message;
                    errors.Add(e.Message);
                    break;

                case NotFoundApiException e:
                    // custom not-found application error
                    status = "not-found";
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    responseModel.Message = e.Message;
                    errors.Add(e.Message);
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
                message = responseModel.Message,
                errors = errors
            });

            await response.WriteAsync(result);
        }
    }
}