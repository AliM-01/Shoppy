using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.IO;

namespace Shoppy.WebApi;
public class Program
{
    public static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json",
                   optional: false,
                   reloadOnChange: true)
               .AddJsonFile(string.Format("appsettings.{0}.json",
                       Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                       ?? "Development"),
                   optional: true,
                   reloadOnChange: true)
               .AddUserSecrets<Startup>(optional: true, reloadOnChange: true)
               .Build();

        Log.Logger = new LoggerConfiguration()
            .WriteTo.MSSqlServer(
                connectionString:
                configuration.GetConnectionString("DefaultConnection"),
                restrictedToMinimumLevel: LogEventLevel.Information,
                sinkOptions: new MSSqlServerSinkOptions
                {
                    TableName = "LogEvents",
                    AutoCreateSqlTable = true
                })
            .WriteTo.Console()
            .CreateLogger();

        var webHost = CreateHostBuilder(args)
            .Build();

        using (var scope = webHost.Services.CreateScope())
        {
        }

        try
        {
            await webHost.RunAsync();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "The application failed to start.");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
              .UseSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
