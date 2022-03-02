using _01_Shoppy.Query;
using _02_DI_Container.Extensions.Startup;
using AM.Application;
using AM.Infrastructure.Configuration;
using AM.Infrastructure.Shared.Mappings;
using BM.Application;
using BM.Infrastructure.Configuration;
using BM.Infrastructure.Shared.Mappings;
using CM.Application;
using CM.Infrastructure.Configuration;
using CM.Infrastructure.Shared.Mappings;
using DM.Application;
using DM.Infrastructure.Configuration;
using DM.Infrastructure.Shared.Mappings;
using IM.Application;
using IM.Infrastructure.Configuration;
using IM.Infrastructure.Shared.Mappings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SM.Application;
using SM.Infrastructure.Configuration;
using SM.Infrastructure.Shared.Mappings;
using System.Reflection;

namespace _02_DI_Container;

public static class DI_Container
{
    public static async Task RegisterServicesAsync(this IServiceCollection services, Type assemblyMarker, IConfiguration config)
    {
        services.AddOptions();

        #region Configuring Modules

        await AccountModuletBootstrapper.ConfigureAsync(services, config);
        ShopModuletBootstrapper.Configure(services, config);
        DiscountModuleBootstrapper.Configure(services, config);
        InventoryModuletBootstrapper.Configure(services, config);
        CommentModuletBootstrapper.Configure(services, config);
        BlogModuletBootstrapper.Configure(services, config);

        #endregion

        #region Mediator And FluentValidation

        services.AddMediatorExtension(new List<Type>
        {
            assemblyMarker,
            typeof(ISMAssemblyMarker),
            typeof(IDMAssemblyMarker),
            typeof(IIMAssemblyMarker),
            typeof(ICMAssemblyMarker),
            typeof(IBMAssemblyMarker),
            typeof(IAMAssemblyMarker),
            typeof(IShoppyQueryAsseblyMarker),
        });

        var assemblies = new List<Assembly>
        {
            typeof(ShopModuleMappingProfile).Assembly,
            typeof(DiscountModuleMappingProfile).Assembly,
            typeof(InventoryModuleMappingProfile).Assembly,
            typeof(CommentModuleMappingProfile).Assembly,
            typeof(BlogModuleMappingProfile).Assembly,
            typeof(AccountModuleMappingProfile).Assembly,
        };

        services.AddFluentValidationExtension(assemblies);

        #endregion

        #region AutoMapper

        services.AddAutoMapperExtension(assemblyMarker, new List<Type>
        {
            typeof(ShopModuleMappingProfile),
            typeof(DiscountModuleMappingProfile),
            typeof(InventoryModuleMappingProfile),
            typeof(CommentModuleMappingProfile),
            typeof(BlogModuleMappingProfile),
            typeof(AccountModuleMappingProfile),
        });

        #endregion

        #region MVC Configuration

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder
                    .WithOrigins("http://localhost:4200") //Note:  The URL must be specified without a trailing slash (/).
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed((host) => true)
                    .AllowCredentials());
        });

        services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
            options.SerializerSettings.MaxDepth = int.MaxValue;
            options.SerializerSettings.Formatting = Formatting.Indented;
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });

        #endregion

    }
}
