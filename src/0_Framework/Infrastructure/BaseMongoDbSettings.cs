﻿namespace _0_Framework.Infrastructure;

public abstract class BaseMongoDbSettings
{
    public string ConnectionString { get; set; }

    public string DbName { get; set; }
}

public interface IBaseMongoDbSettings
{
    public string ConnectionString { get; set; }

    public string DbName { get; set; }
}

