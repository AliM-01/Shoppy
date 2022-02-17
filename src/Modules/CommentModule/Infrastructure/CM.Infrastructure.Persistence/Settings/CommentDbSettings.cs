namespace CM.Infrastructure.Persistence.Settings;

public class CommentDbSettings
{
    public string ConnectionString {
        get {
            return $@"mongodb+srv://{User}:{Password}@{Host}/{DbName}?retryWrites=true&w=majority&connect=replicaSet";
        }
    }

    public string DbName { get; set; }

    public string Host { get; set; }

    public string User { get; set; }

    public string Password { get; set; }

    public string CommentCollection { get; set; }
}
