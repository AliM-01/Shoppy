using _02_DI_Container;
using _02_DI_Container.Extensions.Startup;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, lc) =>
            lc.ReadFrom.Configuration(context.Configuration));

await builder.Services.RegisterServicesAsync(typeof(Program), builder.Configuration);
builder.Services.AddSwaggerExtension("Shoppy.WebApi");

var app = builder.Build();

#region Configure

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

#endregion

Log.Fatal("Fatal !!!.");
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