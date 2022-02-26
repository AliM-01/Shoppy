namespace AM.Infrastructure.Persistence.Settings;

public class JwtSettings
{
    public string Issuer { get; set; }

    public string Audiance { get; set; }

    public string Secret { get; set; }
}
