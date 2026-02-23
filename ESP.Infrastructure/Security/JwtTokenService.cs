using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DotNetEnv;
using ESP.Domain.Interfaces.Security;
using Microsoft.IdentityModel.Tokens;

namespace ESP.Infrastructure.Security
{
    public class JwtTokenService : IJwtTokenService

    {
        
        public string GenerateToken(string mail, bool isAdmin)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("D452qs456453qsdKBHF!;WXD!Hds241F"));

            var claims = new[]
            {
            new Claim(ClaimTypes.Email, mail),
            new Claim(ClaimTypes.Role, isAdmin ? "Admin" : "User")
        };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}