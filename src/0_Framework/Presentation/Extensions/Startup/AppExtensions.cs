using Microsoft.AspNetCore.Builder;

namespace _0_Framework.Presentation.Extensions.Startup
{
    public static class AppExtensions
    {
        public static void UseSwaggerExtension(this IApplicationBuilder app, string title)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", title));
        }
    }
}