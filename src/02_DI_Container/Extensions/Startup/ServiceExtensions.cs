using _0_Framework.Application.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace _02_DI_Container.Extensions.Startup;

public static class ServiceExtensions
{
    public static void AddMediatorExtension(this IServiceCollection services, List<Type> asembliesTypes)
    {
        services.AddMediatR(asembliesTypes, configuration => configuration.AsScoped());
    }

    public static void AddFluentValidationExtension(this IServiceCollection services, List<Assembly> asemblies)
    {
        services.AddValidatorsFromAssemblies(asemblies);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }

    public static void AddAutoMapperExtension(this IServiceCollection services,
         Type mainAssembly, List<Type> mappingProfiles)
    {
        services.AddAutoMapper((serviceProvider, autoMapper) =>
        {
            foreach (var profile in mappingProfiles)
            {
                autoMapper.AddProfile(profile);
            }
        }, mainAssembly.Assembly);
    }

    public static void AddSwaggerExtension(this IServiceCollection services, string title)
    {

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = title, Version = "v1" });
            c.EnableAnnotations();
            c.SchemaFilter<EnumSchemaFilter>();
        });
    }
}

public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema model, SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            model.Type = "string";
            model.Enum.Clear();
            Enum.GetNames(context.Type)
                .ToList()
                .ForEach(name => model.Enum.Add(new OpenApiString(name)));
        }
    }
}