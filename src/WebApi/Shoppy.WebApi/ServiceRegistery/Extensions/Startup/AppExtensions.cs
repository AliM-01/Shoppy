using Microsoft.AspNetCore.Builder;
using Shoppy.WebApi.ServiceRegistery.Middlewares;

namespace Shoppy.WebApi.ServiceRegistery.Extensions.Startup;

public static class AppExtensions
{
    public static void UseSwaggerExtension(this IApplicationBuilder app, string title)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.EnableFilter();
            c.DisplayRequestDuration();
            c.SwaggerEndpoint("/swagger/v1/swagger.json", title);
            c.InjectStylesheet("/swagger-ui/css/custom.css");
            c.DefaultModelExpandDepth(3);
            c.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Example);
            c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            c.EnableDeepLinking();
            c.DisplayOperationId();
        });
    }

    public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorHandlerMiddleware>();
    }
}