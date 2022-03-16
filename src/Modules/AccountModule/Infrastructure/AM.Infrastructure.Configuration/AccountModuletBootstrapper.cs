using _0_Framework.Api;
using _0_Framework.Application.Wrappers;
using _0_Framework.Infrastructure.Helpers;
using AM.Application.Contracts.Common.Settings;
using AM.Application.Contracts.Services;
using AM.Application.Services;
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
using System;
using System.Text;
using System.Threading.Tasks;

namespace AM.Infrastructure.Configuration;

public class AccountModuletBootstrapper
{
    public static async Task ConfigureAsync(IServiceCollection services, IConfiguration config)
    {
        #region db config

        var accountDbSettings = (AccountDbSettings)config.GetSection("AccountDbSettings").Get(typeof(AccountDbSettings));

        services.Configure<AccountDbSettings>(config.GetSection("AccountDbSettings"));

        services.AddIdentity<Account, AccountRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 0;
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = true;
        })
            .AddMongoDbStores<Account, AccountRole, Guid>
            (
               accountDbSettings.ConnectionString, accountDbSettings.DbName
            )
            .AddDefaultTokenProviders()
            .AddErrorDescriber<PersianIdentity.PersianIdentityErrorDescriber>();

        services.AddScoped<IRepository<UserToken>, BaseRepository<UserToken, AccountDbSettings>>();

        services.AddMediatR(typeof(AccountModuletBootstrapper).Assembly);

        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            try
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AccountRole>>();
                await SeedDefaultRoles.SeedAsync(roleManager);

            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        services.AddScoped<ISecurityService, SecurityService>();
        services.AddScoped<ITokenFactoryService, TokenFactoryService>();
        services.AddScoped<ITokenStoreService, TokenStoreService>();
        services.AddScoped<ITokenValidatorService, TokenValidatorService>();


        #region auth config

        var bearerTokenSettings = (BearerTokenSettings)config.GetSection("BearerTokenSettings").Get(typeof(BearerTokenSettings));

        services.Configure<BearerTokenSettings>(config.GetSection("BearerTokenSettings"));

        services.AddAuthorization(options =>
        {
            options.AddPolicy(RoleConstants.Admin, policy => policy.RequireRole(RoleConstants.Admin));
            options.AddPolicy(RoleConstants.BasicUser, policy => policy.RequireRole(RoleConstants.BasicUser));
        });

        services
                .AddAuthentication(options =>
                {
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = bearerTokenSettings.Issuer,
                        ValidateIssuer = true,
                        ValidAudience = bearerTokenSettings.Audiance,
                        ValidateAudience = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(bearerTokenSettings.Secret)),
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                    cfg.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            return context.Response.WriteAsync(ProduceUnAuthorizedResponse(context.Exception.Message));
                        },
                        OnTokenValidated = context =>
                        {
                            var tokenValidatorService = context.HttpContext.RequestServices.GetRequiredService<ITokenValidatorService>();
                            return tokenValidatorService.ValidateAsync(context);
                        },
                        OnMessageReceived = context =>
                        {
                            return Task.CompletedTask;
                        },
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            return context.Response.WriteAsync(ProduceUnAuthorizedResponse());
                        },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            return context.Response.WriteAsync(ProduceUnAuthorizedResponse());
                        }
                    };
                });

        #endregion
    }

    private static string ProduceUnAuthorizedResponse(string message = "لطفا به حساب کاربری خود وارد شوید")
    {
        return CustonJsonConverter.Serialize(new Response<string>().Unauthorized(message));
    }
}
