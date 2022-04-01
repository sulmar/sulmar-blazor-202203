using Auth.Api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Api.Controllers
{
    // dotnet add package System.IdentityModel.Tokens.Jwt
    public class JwtTokenService : ITokenService
    {
        public SecurityToken Create(ApplicationUser model)
        {
            string secretKey = "your-256-bit-secret";

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Email, model.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("AccountNumber", model.Account)
            };

            string issuer = "ABC";

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(
                issuer: issuer,
                audience: issuer,
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return securityToken;

        }
    }
}
