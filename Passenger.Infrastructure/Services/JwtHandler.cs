using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Settings;

namespace Passenger.Infrastructure.Services;

public class JwtHandler : IJwtHandler
{
    private readonly JwtSettings _settings;

    public JwtHandler(JwtSettings settings)
    {
        _settings = settings;
    }

    public JwtDto CreateToken(Guid userId, string role)
    {
        var now = DateTimeOffset.UtcNow;
        var expires = now.AddMinutes(_settings.ExpiryMinutes);
        
        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            // ASP.NET 4.6 added method ToUnixTimeSeconds, extension is unnecessary
            new Claim(JwtRegisteredClaimNames.Iat, now.ToUnixTimeSeconds().ToString(),
                ClaimValueTypes.Integer64),
        };

        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.IssuerSigningKey)),
            SecurityAlgorithms.HmacSha256);

        var jwt = new JwtSecurityToken(
            issuer: _settings.ValidIssuer,
            claims: claims,
            notBefore: now.DateTime,
            expires: expires.DateTime,
            signingCredentials: signingCredentials);

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return new JwtDto(
            Token: token,
            Expiry: expires.ToUnixTimeMilliseconds()
        );
    }
}