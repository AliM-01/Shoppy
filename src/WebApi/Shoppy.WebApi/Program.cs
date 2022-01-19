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
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Shoppy.WebApi;
using SM.Application;
using SM.Infrastructure.Configuration;
using SM.Infrastructure.Shared.Mappings;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

var connectionString = configuration.GetConnectionString("DefaultConnection");

builder.Host.UseSerilog();
Log.Logger = new LoggerConfiguration()
            .WriteTo.MSSqlServer(
                connectionString:
                connectionString,
                restrictedToMinimumLevel: LogEventLevel.Information,
                sinkOptions: new MSSqlServerSinkOptions
                {
                    TableName = "LogEvents",
                    AutoCreateSqlTable = true
                })
            .WriteTo.Console()
            .CreateLogger();

#region ConfigureServices

#region Configuring Modules

ShopModuletBootstrapper.Configure(builder.Services, connectionString);
DiscountModuleBootstrapper.Configure(builder.Services, connectionString);
InventoryModuletBootstrapper.Configure(builder.Services, connectionString);

#endregion

#region Mediator And FluentValidation

builder.Services.AddMediatorAndFluentValidationExtension(new List<Type>
            {
                typeof(IAssemblyMarker),
                typeof(ISMAssemblyMarker),
                typeof(IDMAssemblyMarker),
                typeof(IIMAssemblyMarker)
            });

#endregion

#region AutoMapper

builder.Services.AddAutoMapperExtension(typeof(IAssemblyMarker), new List<Type>
            {
                typeof(ShopModuleMappingProfile),
                typeof(DiscountModuleMappingProfile),
                typeof(InventoryModuleMappingProfile),
            });

#endregion

#region Swagger

var xmlFile = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

builder.Services.AddSwaggerExtension("Shoppy.WebApi", xmlPath);

#endregion

#region MVC Configuration

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
    options.SerializerSettings.MaxDepth = int.MaxValue;
    options.SerializerSettings.Formatting = Formatting.Indented;
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

#endregion

#endregion

var app = builder.Build();

#region Configure

if (environment.IsDevelopment())
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

#endregion

using (var scope = app.Services.CreateScope())
{
}

try
{
    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "The application failed to start.");
}
finally
{
    Log.CloseAndFlush();
}