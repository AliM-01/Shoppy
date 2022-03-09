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
using OM.Application;
using OM.Infrastructure.Configuration;
using OM.Infrastructure.Shared.Mappings;
using SM.Application;
using SM.Infrastructure.Configuration;
using SM.Infrastructure.Shared.Mappings;

namespace _02_DI_Container;

public static class DI_Container
{
    public static async Task RegisterServicesAsync(this IServiceCollection services, Type assemblyMarker, IConfiguration config)
    {
        await ConfigureModules(services, config);
        AddCors(services);
        AddGeneralSettings(services);
        AddFluentValidation(services);
        AddAutoMapper(services, assemblyMarker);
        AddMediator(services, assemblyMarker);
    }

    private static async Task ConfigureModules(IServiceCollection services, IConfiguration config)
    {
        await AccountModuletBootstrapper.ConfigureAsync(services, config);
        ShopModuletBootstrapper.Configure(services, config);
        DiscountModuleBootstrapper.Configure(services, config);
        InventoryModuletBootstrapper.Configure(services, config);
        CommentModuletBootstrapper.Configure(services, config);
        BlogModuletBootstrapper.Configure(services, config);
        OrderModuletBootstrapper.Configure(services, config);
    }

    private static void AddGeneralSettings(IServiceCollection services)
    {
        services.AddOptions();
        services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
            options.SerializerSettings.MaxDepth = int.MaxValue;
            options.SerializerSettings.Formatting = Formatting.Indented;
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });
    }

    private static void AddCors(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed((host) => true)
                    .AllowCredentials());
        });
    }

    private static void AddMediator(IServiceCollection services, Type assemblyMarker)
    {
        services.AddMediatorExtension(new List<Type>
        {
            assemblyMarker,
            typeof(ISMAssemblyMarker),
            typeof(IDMAssemblyMarker),
            typeof(IIMAssemblyMarker),
            typeof(ICMAssemblyMarker),
            typeof(IBMAssemblyMarker),
            typeof(IAMAssemblyMarker),
            typeof(IOMAssemblyMarker),
            typeof(IShoppyQueryAsseblyMarker),
        });
    }

    private static void AddFluentValidation(IServiceCollection services)
    {
        var assemblyTypes = new List<Type>
        {
            typeof(ShopModuleMappingProfile),
            typeof(DiscountModuleMappingProfile),
            typeof(InventoryModuleMappingProfile),
            typeof(CommentModuleMappingProfile),
            typeof(BlogModuleMappingProfile),
            typeof(AccountModuleMappingProfile),
            typeof(OrderModuleMappingProfile),
        };
        services.AddFluentValidationExtension(assemblyTypes);
    }

    private static void AddAutoMapper(IServiceCollection services, Type assemblyMarker)
    {
        services.AddAutoMapperExtension(assemblyMarker, new List<Type>
        {
            typeof(ShopModuleMappingProfile),
            typeof(DiscountModuleMappingProfile),
            typeof(InventoryModuleMappingProfile),
            typeof(CommentModuleMappingProfile),
            typeof(BlogModuleMappingProfile),
            typeof(AccountModuleMappingProfile),
            typeof(OrderModuleMappingProfile),
        });
    }
}
