using _0_Framework.Infrastructure;

namespace AM.Infrastructure.Persistence.Settings;

public class AccountDbSettings : BaseMongoDbSettings
{
    public string UserTokenCollection { get; set; }
}