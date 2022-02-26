using AM.Domain.Account;
using AM.Infrastructure.Persistence.Seed;
using AM.Infrastructure.Persistence.Settings;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace AM.Infrastructure.Configuration;

public class AccountModuletBootstrapper
{
    public static async Task ConfigureAsync(IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration config)
    {
        #region db config

        services.AddSingleton<AccountDbSettings>(sp =>
        {
            return (AccountDbSettings)config.GetSection("AccountDbSettings").Get(typeof(AccountDbSettings));
        });

        var accountDbSettings = services.BuildServiceProvider().GetRequiredService<AccountDbSettings>();

        services.AddIdentity<Account, AccountRole>()
            .AddMongoDbStores<Account, AccountRole, Guid>
            (
               accountDbSettings.ConnectionString, accountDbSettings.DbName
            );

        services.AddMediatR(typeof(AccountModuletBootstrapper).Assembly);

        using (var sp = services.BuildServiceProvider())
        {
            try
            {
                var roleManager = sp.GetRequiredService<RoleManager<AccountRole>>();
                await SeedDefaultRoles.SeedAsync(roleManager);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region auth config

        services.AddSingleton<JwtSettings>(sp =>
        {
            return (JwtSettings)config.GetSection("JwtSettings").Get(typeof(JwtSettings));
        });

        var jwtSettings = services.BuildServiceProvider().GetRequiredService<JwtSettings>();

        services.AddAuthentication("OAuth")
            .AddJwtBearer("OAuth", c =>
            {
                var secretBytes = Encoding.UTF8.GetBytes(jwtSettings.Secret);
                var key = new SymmetricSecurityKey(secretBytes);

                c.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new
                        {
                            status = "un-authorized",
                            message = context.Exception.Message.ToString()
                        }); ;
                        return context.Response.WriteAsync(result);
                    },
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new
                        {
                            status = "un-authorized",
                            message = "لطفا به حساب کاربری خود وارد شوید"
                        });
                        return context.Response.WriteAsync(result);
                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new
                        {
                            status = "un-authorized",
                            message = "لطفا به حساب کاربری خود وارد شوید"
                        });
                        return context.Response.WriteAsync(result);
                    },
                };

                c.TokenValidationParameters = new TokenValidationParameters()
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audiance,
                    IssuerSigningKey = key,
                };
            });

        #endregion
    }
}
