using _02_DI_Container.Extensions.Startup;
using BM.Application;
using BM.Infrastructure.Configuration;
using BM.Infrastructure.Shared.Mappings;
using CM.Application;
using CM.Infrastructure.Configuration;
using CM.Infrastructure.Shared.Mappings;
using DM.Application;
using DM.Infrastructure.Configuration;
using DM.Infrastructure.Shared.Mappings;
using FluentValidation.AspNetCore;
using IM.Application;
using IM.Infrastructure.Configuration;
using IM.Infrastructure.Shared.Mappings;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SM.Application;
using SM.Infrastructure.Configuration;
using SM.Infrastructure.Shared.Mappings;

namespace _02_DI_Container;

public static class DI_Container
{
    public static void RegisterServices(this IServiceCollection services, Type assemblyMarker, string connectionString)
    {
        #region Configuring Modules

        ShopModuletBootstrapper.Configure(services, connectionString);
        DiscountModuleBootstrapper.Configure(services, connectionString);
        InventoryModuletBootstrapper.Configure(services, connectionString);
        CommentModuletBootstrapper.Configure(services, connectionString);
        BlogModuletBootstrapper.Configure(services, connectionString);

        #endregion

        #region Mediator And FluentValidation

        services.AddMediatorAndFluentValidationExtension(new List<Type>
        {
            assemblyMarker,
            typeof(ISMAssemblyMarker),
            typeof(IDMAssemblyMarker),
            typeof(IIMAssemblyMarker),
            typeof(ICMAssemblyMarker),
            typeof(IBMAssemblyMarker)
        });

        #endregion

        #region AutoMapper

        services.AddAutoMapperExtension(assemblyMarker, new List<Type>
        {
            typeof(ShopModuleMappingProfile),
            typeof(DiscountModuleMappingProfile),
            typeof(InventoryModuleMappingProfile),
            typeof(CommentModuleMappingProfile),
            typeof(BlogModuleMappingProfile)
        });

        #endregion

        #region MVC Configuration

        var assemblies = new List<Type>
        {
            assemblyMarker,
            typeof(ISMAssemblyMarker),
            typeof(IDMAssemblyMarker),
            typeof(IIMAssemblyMarker),
            typeof(ICMAssemblyMarker),
            typeof(IBMAssemblyMarker)
        }
        .Select(x => x.Assembly)
        .ToList();

        services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
            options.SerializerSettings.MaxDepth = int.MaxValue;
            options.SerializerSettings.Formatting = Formatting.Indented;
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }).AddFluentValidation(s =>
        {
            s.RegisterValidatorsFromAssemblies(assemblies);
            s.DisableDataAnnotationsValidation = false;
        });

        #endregion

    }
}
