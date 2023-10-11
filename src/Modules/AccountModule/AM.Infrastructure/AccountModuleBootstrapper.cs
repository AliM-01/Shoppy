using _0_Framework.Api;
using _0_Framework.Application.Wrappers;
using _0_Framework.Infrastructure.IRepository;
using AM.Application.Services;
using AM.Domain.Account;
using AM.Domain.Enums;
using AM.Infrastructure.Seed;
using AM.Domain.Settings;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;

namespace AM.Infrastructure;

public class AccountModuleBootstrapper
{
    public async static Task ConfigureAsync(IServiceCollection services, IConfiguration config)
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

        services.AddTransient<IRepository<UserToken>, BaseRepository<UserToken, AccountDbSettings>>();

        services.AddMediatR(typeof(AccountModuleBootstrapper).Assembly);

        #region Db Seed

        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            var sp = scope.ServiceProvider;
            var logger = sp.GetRequiredService<ILogger<AccountModuleBootstrapper>>();

            try
            {
                var roleManager = sp.GetRequiredService<RoleManager<AccountRole>>();
                var userManager = sp.GetRequiredService<UserManager<Account>>();
                await SeedDefaultRoles.SeedAsync(roleManager);
                await SeedDefaultUsers.SeedAdminAsync(userManager);
                await SeedDefaultUsers.SeedBasicUserAsync(userManager);
                logger.LogInformation("Account Module Db Seed Finished Successfully");
            }
            catch (Exception ex)
            {
                logger.LogError("Account Module Db Seed Was Unsuccessfull. Execption : {0}", ex.Message);
            }
        }

        #endregion

        #endregion

        #region services

        services.AddTransient<IEmailSenderService, EmailSenderService>();
        services.AddTransient<IViewRenderService, ViewRenderService>();
        services.AddTransient<ISecurityService, SecurityService>();
        services.AddTransient<ITokenFactoryService, TokenFactoryService>();
        services.AddTransient<ITokenStoreService, TokenStoreService>();
        services.AddTransient<ITokenValidatorService, TokenValidatorService>();

        #endregion

        #region auth config

        services.Configure<EmailSettings>(config.GetSection("EmailSettings"));

        var bearerTokenSettings = (BearerTokenSettings)config.GetSection("BearerTokenSettings").Get(typeof(BearerTokenSettings));

        services.Configure<BearerTokenSettings>(config.GetSection("BearerTokenSettings"));

        #region Authentication


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
                            return context.Response.WriteAsync(ProduceUnAuthorizedResponse());
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
                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/json";
                            return context.Response.WriteAsync(ProduceAccessDeniedResponse());
                        }
                    };
                })
                .AddGoogle(cfg =>
                {
                    cfg.ClientId = config["Google_OAuth:Client_Id"];
                    cfg.ClientSecret = config["Google_OAuth:Client_Secret"];
                });


        #endregion

        #region Authorization

        services.AddAuthorization(options =>
        {
            options.AddPolicy(RoleConstants.Admin, policy => policy.RequireRole(RoleConstants.Admin));
            options.AddPolicy(RoleConstants.BasicUser, policy => policy.RequireRole(RoleConstants.BasicUser));
        });

        #endregion


        #endregion
    }

    private static string ProduceUnAuthorizedResponse()
    {
        return JsonSerializer.Serialize(ApiResponse.Unauthorized());
    }

    private static string ProduceAccessDeniedResponse()
    {
        return JsonSerializer.Serialize(ApiResponse.AccessDenied());
    }
}
