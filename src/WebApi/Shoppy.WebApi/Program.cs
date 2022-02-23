using _02_DI_Container;
using _02_DI_Container.Extensions.Startup;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var environment = builder.Environment;

#region logger

builder.Host.UseSerilog();
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

#endregion

builder.Services.RegisterServices(typeof(Program), configuration);

#region swagger

builder.Services.AddSwaggerExtension("Shoppy.WebApi");

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