using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FCG.Infrastructure.Authentication;

public class JwtTokenGenerator(IConfiguration _configuration) : IJwtTokenGenerator
{
    public Task<string> GenerateTokenAsync(string userName, string role)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["SecretKey"];
        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];

        var claims = new[]
        {
    new Claim(JwtRegisteredClaimNames.Sub, userName),
    new Claim(ClaimTypes.Role, role),
    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds,
            audience: audience
        );

        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
    }
}