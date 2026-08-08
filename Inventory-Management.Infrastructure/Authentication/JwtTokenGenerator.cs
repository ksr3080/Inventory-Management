using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Inventory_Management.Domain.Entities;

namespace Inventory_Management.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IConfiguration _configuration;

    public JwtTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public (string Token, DateTime Expiration) GenerateToken(Users user)
    {
        var secretKey = _configuration["JwtSettings:Secret"] 
            ?? "InventoryManagementERPSecretKeyForJWTAuthentication2026!";
        var issuer = _configuration["JwtSettings:Issuer"] ?? "InventoryManagementApi";
        var audience = _configuration["JwtSettings:Audience"] ?? "InventoryManagementClient";
        var expiryMinutesStr = _configuration["JwtSettings:ExpiryInMinutes"] ?? "120";

        if (!double.TryParse(expiryMinutesStr, out var expiryMinutes))
        {
            expiryMinutes = 120;
        }

        var expiration = DateTime.UtcNow.AddMinutes(expiryMinutes);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.EmailId),
            new("FirstName", user.FirstName),
            new("LastName", user.LastName)
        };

        if (!string.IsNullOrEmpty(user.UserType))
        {
            claims.Add(new Claim(ClaimTypes.Role, user.UserType));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expiration,
            signingCredentials: credentials);

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenString = tokenHandler.WriteToken(tokenDescriptor);

        return (tokenString, expiration);
    }
}
