
using _0_Framework.Presentation.Extensions.Startup;
using DM.Application;
using DM.Infrastructure.Configuration;
using DM.Infrastructure.Shared.Mappings;
using IM.Application;
using IM.Infrastructure.Configuration;
using IM.Infrastructure.Shared.Mappings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Serilog;
using SM.Application;
using SM.Infrastructure.Configuration;
using SM.Infrastructure.Shared.Mappings;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Shoppy.WebApi;

public class Startup
{
    #region Ctor

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    #endregion

    #region ConfigureServices

    public void ConfigureServices(IServiceCollection services)
    {
        #region Configuring Modules

        var connectionString = Configuration.GetConnectionString("DefaultConnection");

        ShopModuletBootstrapper.Configure(services, connectionString);
        DiscountModuleBootstrapper.Configure(services, connectionString);
        InventoryModuletBootstrapper.Configure(services, connectionString);

        #endregion

        #region Mediator And FluentValidation

        services.AddMediatorAndFluentValidationExtension(new List<Type>
            {
                typeof(Startup),
                typeof(ISMAssemblyMarker),
                typeof(IDMAssemblyMarker),
                typeof(IIMAssemblyMarker)
            });

        #endregion

        #region AutoMapper

        services.AddAutoMapperExtension(typeof(Startup), new List<Type>
            {
                typeof(ShopModuleMappingProfile),
                typeof(DiscountModuleMappingProfile),
                typeof(InventoryModuleMappingProfile),
            });

        #endregion

        #region Swagger

        var xmlFile = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

        services.AddSwaggerExtension("Shoppy.WebApi", xmlPath);

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

    #endregion

    #region Configure

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        app.UseErrorHandlingMiddleware();
        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseCors(options =>
        {
            options.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
        });

        app.UseRouting();

        app.UseSwaggerExtension("Shoppy.WebApi");

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseSerilogRequestLogging();
    }

    #endregion
}