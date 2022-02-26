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
    public static async Task ConfigureAsync(IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration config)
    {
        services.Configure<AccountDbSettings>(config.GetSection("AccountDbSettings"));

        services.AddIdentity<Account, AccountRole>()
            .AddMongoDbStores<Account, AccountRole, Guid>
            (
                config["AccountDbSettings:ConnectionString"], config["AccountDbSettings:DbName"]
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
