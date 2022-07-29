namespace _0_Framework.Infrastructure;

abstract public class BaseDbSettings
{
    public string DbName { get; set; }

    public string Host { get; set; }

    public string Port { get; set; }

    public string ConnectionString => $@"mongodb://{Host}:{Port}";
}