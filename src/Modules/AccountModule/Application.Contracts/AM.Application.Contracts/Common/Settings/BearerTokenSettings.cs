namespace AM.Application.Contracts.Common.Settings;

public class BearerTokenSettings
{
    public string Secret { set; get; }
    public string Issuer { set; get; }
    public string Audiance { set; get; }
    public int AccessTokenExpirationMinutes { set; get; }
    public int RefreshTokenExpirationHours { set; get; }
    public bool AllowMultipleLoginsFromTheSameUser { set; get; }
    public bool AllowSignoutAllUserActiveClients { set; get; }
}
