using _0_Framework.Infrastructure;

namespace AM.Infrastructure.Persistence.Settings;

public class AccountDbSettings : IBaseMongoDbSettings
{
    public string ConnectionString { get; set; }

    public string DbName { get; set; }
}