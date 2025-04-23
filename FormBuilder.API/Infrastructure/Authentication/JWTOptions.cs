namespace FormBuilder.API.Infrastructure.Authentication;

public class JWTOptions
{
    public string PrivateKey { get; init; }
    public string Issuer { get; init; }

    public string Audience { get; init; }
    public int ExpirationMinutes { get; init; }
}
