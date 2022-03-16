using _0_Framework.Infrastructure;

namespace AM.Infrastructure.Persistence.Settings;

public class AccountDbSettings : BaseDbSettings
{
    public string UserTokenCollection { get; set; }
}