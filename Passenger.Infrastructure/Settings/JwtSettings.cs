namespace Passenger.Infrastructure.Settings;

public class JwtSettings
{
    public string ValidIssuer { get; set; }
    public string IssuerSigningKey { get; set; }
    public int ExpiryMinutes { get; set; }
}