using _0_Framework.Infrastructure;

namespace AM.Domain.Settings;

public class AccountDbSettings : BaseDbSettings
{
    public string UserTokenCollection { get; set; }
}