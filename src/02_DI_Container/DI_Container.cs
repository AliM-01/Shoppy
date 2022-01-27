using _02_DI_Container.Extensions.Startup;
using CM.Application;
using CM.Infrastructure.Configuration;
using DM.Application;
using DM.Infrastructure.Configuration;
using DM.Infrastructure.Shared.Mappings;
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

        #endregion

        #region Mediator And FluentValidation

        services.AddMediatorAndFluentValidationExtension(new List<Type>
        {
            assemblyMarker,
            typeof(ISMAssemblyMarker),
            typeof(IDMAssemblyMarker),
            typeof(IIMAssemblyMarker),
            typeof(ICMAssemblyMarker)
        });

        #endregion

        #region AutoMapper

        services.AddAutoMapperExtension(assemblyMarker, new List<Type>
        {
            typeof(ShopModuleMappingProfile),
            typeof(DiscountModuleMappingProfile),
            typeof(InventoryModuleMappingProfile),
        });

        #endregion

        #region MVC Configuration

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
