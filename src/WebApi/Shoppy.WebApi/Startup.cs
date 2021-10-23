using System;
using System.IO;
using System.Reflection;
using _0_Framework.Presentation.Extensions.Startup;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using SM.Infrastructure.Configuration;
using SM.Infrastructure.Shared.Mappings;

namespace Shoppy.WebApi
{
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
            #region MediatR

            services.AddMediatR(typeof(Startup).Assembly);

            #endregion

            #region Configuring Modules

            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            ShopManagementBootstrapper.Configure(services, connectionString);

            #endregion

            #region AutoMapper

            services.AddAutoMapper((serviceProvider, automapper) =>
            {
                automapper.AddProfile(new ShopManagementMappingProfile());
            }, typeof(Startup).Assembly);

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

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseSwaggerExtension("Shoppy.WebApi");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        #endregion
    }
}
