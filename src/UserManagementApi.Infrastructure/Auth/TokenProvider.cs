using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserManagementApi.Domain.Entities;
using UserManagementApi.Domain.Interfaces;
using Microsoft.IdentityModel.JsonWebTokens;

namespace UserManagementApi.Infrastructure.Auth;

public class TokenProvider(IConfiguration configuration) : ITokenProvider
{
    public string Create(UserEntity userEntity)
    {
        var secretKey = configuration["Jwt:Secret"];
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(JwtRegisteredClaimNames.Sub, userEntity.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, userEntity.Email)
            ]),
            Expires = DateTime.UtcNow.AddMinutes(10),
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"]
        };

        var handler = new JsonWebTokenHandler();
        var token = handler.CreateToken(descriptor);
        return token;
    }
}
