using _0_Framework.Infrastructure;

namespace CM.Domain.Persistence.Settings;

public class CommentDbSettings : BaseDbSettings
{
    public string CommentCollection { get; set; }
}
