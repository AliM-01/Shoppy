using _0_Framework.Presentation.Extensions.Startup;
using _02_DI_Container;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Shoppy.WebApi;
using System.IO;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var environment = builder.Environment;

var connectionString = configuration.GetConnectionString("DefaultConnection");

#region logger

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

#endregion

builder.Services.RegisterServices(typeof(IAssemblyMarker), connectionString);

#region Swagger

var xmlFile = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

builder.Services.AddSwaggerExtension("Shoppy.WebApi", xmlPath);

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