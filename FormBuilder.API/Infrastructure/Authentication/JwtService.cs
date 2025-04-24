using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FormBuilder.Domain.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FormBuilder.API.Infrastructure.Authentication;

public interface IJwtService
{
    string GenerateToken(ApplicationUser user);
}

public class JwtService : IJwtService
{
    private readonly JWTOptions _jwtOptions;
    public JwtService(IOptions<JWTOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public string GenerateToken(ApplicationUser user)
    {
        var claims = GenerateClaims(user);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.PrivateKey));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private List<Claim> GenerateClaims(ApplicationUser user)
    {
        List<Claim> claims =
        [
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(ClaimTypes.GivenName, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, user.UserName)
        ];

        return claims;
    }
}