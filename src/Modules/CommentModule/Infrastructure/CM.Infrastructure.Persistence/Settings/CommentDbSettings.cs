using _0_Framework.Infrastructure;

namespace CM.Infrastructure.Persistence.Settings;

public class CommentDbSettings : BaseMongoDbSettings
{
    public string CommentCollection { get; set; }
}
