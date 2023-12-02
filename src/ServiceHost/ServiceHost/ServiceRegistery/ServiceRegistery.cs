using _0_Framework.Application.ZarinPal;
using AM.Infrastructure;
using CM.Infrastructure;
using DM.Infrastructure;
using IM.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OM.Infrastructure;
using SM.Infrastructure;
using BM.Infrastructure;
using Newtonsoft.Json;
using AM.Application.Mappings;
using BM.Application.Mappings;
using CM.Application.Mappings;
using DM.Application.Mappings;
using IM.Application.Mappings;
using OM.Application.Mappings;
using SM.Infrastructure.Mappings;
using ServiceHost.ServiceRegistery.Extensions.Startup;
using AM.Application;
using BM.Application;
using CM.Application;
using DM.Application;
using IM.Application;
using OM.Application;
using SM.Application;

namespace ServiceHost.ServiceRegistery;

public static class ServiceRegistery
{
    public async static Task RegisterServicesAsync(this IServiceCollection services, Type assemblyMarker, IConfiguration config)
    {
        //================================== Modules
        await AccountModuleBootstrapper.ConfigureAsync(services, config);
        ShopModuleBootstrapper.Configure(services, config);
        DiscountModuleBootstrapper.Configure(services, config);
        InventoryModuleBootstrapper.Configure(services, config);
        CommentModuleBootstrapper.Configure(services, config);
        BlogModuleBootstrapper.Configure(services, config);
        OrderModuleBootstrapper.Configure(services, config);

        services.AddTransient<IZarinPalFactory, ZarinPalFactory>();



        //================================== CORS
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



        //================================== General Settings
        services.AddControllersWithViews().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
            options.SerializerSettings.MaxDepth = int.MaxValue;
            options.SerializerSettings.Formatting = Formatting.Indented;
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });
        services.AddOptions();
        services.AddHttpContextAccessor();




        //================================== Fluent Validation
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



        //================================== AutoMapper
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



        //================================== Mediator
        services.AddMediatorExtension(new List<Type>
        {
            assemblyMarker,
            typeof(ISMAssemblyMarker),
            typeof(IDMAssemblyMarker),
            typeof(IIMAssemblyMarker),
            typeof(ICMAssemblyMarker),
            typeof(IBMAssemblyMarker),
            typeof(IAMAssemblyMarker),
            typeof(IOMAssemblyMarker)
        });
    }
}
