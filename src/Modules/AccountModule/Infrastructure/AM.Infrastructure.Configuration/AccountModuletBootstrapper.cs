using AM.Domain.Account;
using AM.Infrastructure.Persistence.Seed;
using AM.Infrastructure.Persistence.Settings;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace AM.Infrastructure.Configuration;

public class AccountModuletBootstrapper
{
    public static async Task Configure(IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration config)
    {
        services.Configure<AccountDbSettings>(config.GetSection("AccountDbSettings"));

        var mongoDbSettings = services.BuildServiceProvider().GetRequiredService<AccountDbSettings>();

        services.AddIdentity<Account, AccountRole>()
            .AddMongoDbStores<Account, AccountRole, Guid>
            (
                mongoDbSettings.ConnectionString, mongoDbSettings.DbName
            );

        services.AddMediatR(typeof(AccountModuletBootstrapper).Assembly);

        using (var scope = services.BuildServiceProvider())
        {
            try
            {
                var roleManager = scope.GetRequiredService<RoleManager<AccountRole>>();
                await SeedDefaultRoles.SeedAsync(roleManager);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
