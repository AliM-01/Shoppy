using _0_Framework.Infrastructure.Helpers;
using AM.Domain.Account;
using AM.Domain.Enums;
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
    public static async Task ConfigureAsync(IServiceCollection services, IConfiguration config)
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

        services.AddScoped<IGenericRepository<UserToken>, GenericRepository<UserToken, AccountDbSettings>>();

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

        services.AddAuthorization(options =>
        {
            options.AddPolicy(RoleConstants.Admin, policy => policy.RequireRole(RoleConstants.Admin));
            options.AddPolicy(RoleConstants.BasicUser, policy => policy.RequireRole(RoleConstants.BasicUser));
        });

        var jwtSettings = services.BuildServiceProvider().GetRequiredService<JwtSettings>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(c =>
            {
                c.SaveToken = true;
                c.RequireHttpsMetadata = false;
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
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audiance,
                    IssuerSigningKey = key
                };
            });

        #endregion
    }
}
