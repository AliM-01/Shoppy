﻿using _0_Framework.Application.ZarinPal;
using _01_Shoppy.Query;
using _02_DI_Container.Extensions.Startup;
using _03_Reports.Query;
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
        services.AddOptions();
        services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
            options.SerializerSettings.MaxDepth = int.MaxValue;
            options.SerializerSettings.Formatting = Formatting.Indented;
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });
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