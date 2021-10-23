using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace _0_Framework.Presentation.Extensions.Startup
{
    public static class ServiceExtensions
    {
        public static void AddSwaggerExtension(this IServiceCollection services, string title, string xmlPath)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = title, Version = "v1" });

                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}