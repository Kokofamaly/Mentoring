using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using WordCardsApi.Models;
using System.Text;

namespace WordCardsApi.Services;

public class JwtService
{

    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            
        };
        
        var identity = new ClaimsIdentity(claims);
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), 
            SecurityAlgorithms.HmacSha256
            );

        var token = JwtSecurityToken(            
            issuer: issuer,
            audience: audience,
            claims: identity.Claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenString;

    }
}