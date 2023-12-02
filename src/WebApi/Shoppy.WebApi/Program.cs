using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Serilog;
using Shoppy.WebApi.ServiceRegistery;
using Shoppy.WebApi.ServiceRegistery.Extensions.Startup;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, lc) =>
    lc.ReadFrom.Configuration(context.Configuration));

await builder.Services.RegisterServicesAsync(typeof(Program), builder.Configuration);
builder.Services.AddSwaggerExtension("Shoppy.WebApi");

var app = builder.Build();

if (builder.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
    app.UseHsts();

app.UseErrorHandlingMiddleware();
app.UseHttpsRedirection();
app.UseStatusCodePages();
app.UseStaticFiles();
app.UseSwaggerExtension("Shoppy.WebApi");

app.UseRouting();
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseSerilogRequestLogging();

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