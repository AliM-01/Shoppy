using _0_Framework.Presentation.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace _02_DI_Container.Extensions.Startup;

public static class AppExtensions
{
    public static void UseSwaggerExtension(this IApplicationBuilder app, string title)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", title));
    }

    public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorHandlerMiddleware>();
    }
}