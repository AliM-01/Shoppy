using AM.Domain.Account;
using AM.Infrastructure.Persistence.Settings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AM.Infrastructure.Configuration;

public class AccountModuletBootstrapper
{
    public static void Configure(IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration config)
    {
        services.Configure<AccountDbSettings>(config.GetSection("AccountDbSettings"));

        var mongoDbSettings = services.BuildServiceProvider().GetRequiredService<AccountDbSettings>();

        services.AddIdentity<Account, AccountRole>()
            .AddMongoDbStores<Account, AccountRole, Guid>
            (
                mongoDbSettings.ConnectionString, mongoDbSettings.DbName
            );

        services.AddMediatR(typeof(AccountModuletBootstrapper).Assembly);
    }
}
