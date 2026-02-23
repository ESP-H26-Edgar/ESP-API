using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ESP.Domain.Interfaces.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ESP.Infrastructure.Security
{

    public class JwtTokenService : IJwtTokenService

    {
        //je n'avais aucune idée de comment généré le token en C#


        private readonly IConfiguration _config;

        public JwtTokenService(IConfiguration config)
        {
            _config = config;
        }
        // aide de chat gpt pour le gestion du token JWT
        public string GenerateToken(string mail, bool isAdmin)
        {
            var jwtKey = _config["Jwt:Key"]!;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            var claims = new[]
            {
            new Claim(ClaimTypes.Email, mail),
            new Claim(ClaimTypes.Role, isAdmin ? "Admin" : "User")
        };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    int.Parse(_config["Jwt:ExpiresMinutes"] ?? "60")),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}