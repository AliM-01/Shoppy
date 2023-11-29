using _0_Framework.Application.ZarinPal;
using _01_Shoppy.Query;
using _02_DI_Container.Extensions.Startup;
using _03_Reports.Query;
using AM.Application;
using AM.Infrastructure;
using AM.Application.Mappings;
using BM.Application;
using BM.Infrastructure;
using BM.Application.Mappings;
using CM.Application;
using CM.Infrastructure;
using DM.Application;
using DM.Infrastructure.Configuration;
using IM.Application;
using IM.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OM.Application;
using OM.Infrastructure;
using OM.Application.Mappings;
using SM.Application;
using SM.Infrastructure;
using SM.Infrastructure.Mappings;
using IM.Application.Mappings;
using CM.Application.Mappings;
using DM.Application.Mappings;

namespace _02_DI_Container;

public static class DI_Container
{
    public async static Task RegisterServicesAsync(this IServiceCollection services, Type assemblyMarker, IConfiguration config)
    {
        DI_ContainerTools tools = new();

        await tools.ConfigureModules(services, config);
        tools.AddCors(services);
        tools.AddGeneralSettings(services);
        tools.AddFluentValidation(services);
        tools.AddAutoMapper(services, assemblyMarker);
        tools.AddMediator(services, assemblyMarker);
    }
}


internal class DI_ContainerTools
{
    public async Task ConfigureModules(IServiceCollection services, IConfiguration config)
    {
        await AccountModuleBootstrapper.ConfigureAsync(services, config);
        ShopModuleBootstrapper.Configure(services, config);
        DiscountModuleBootstrapper.Configure(services, config);
        InventoryModuleBootstrapper.Configure(services, config);
        CommentModuleBootstrapper.Configure(services, config);
        BlogModuleBootstrapper.Configure(services, config);
        OrderModuleBootstrapper.Configure(services, config);

        services.AddTransient<IZarinPalFactory, ZarinPalFactory>();
    }

    public void AddGeneralSettings(IServiceCollection services)
    {
        services.AddControllersWithViews().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
            options.SerializerSettings.MaxDepth = int.MaxValue;
            options.SerializerSettings.Formatting = Formatting.Indented;
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });
        services.AddOptions();
        services.AddHttpContextAccessor();
    }

    public void AddCors(IServiceCollection services)
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

    public void AddMediator(IServiceCollection services, Type assemblyMarker)
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
            typeof(IReportAssemblyMarker),
        });
    }

    public void AddFluentValidation(IServiceCollection services)
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

    public void AddAutoMapper(IServiceCollection services, Type assemblyMarker)
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