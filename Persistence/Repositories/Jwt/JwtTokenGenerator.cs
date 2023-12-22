using Application.DTOs.User;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Jwt
{
    public static class JwtTokenGenerator
    {
        public static string GenerateToken(string email, string username, Guid id, string firstname, string lastname, JwtSettings _jwtSettings)
        {
            // var claims = new[]
            // {
            //     new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
            //     new Claim(JwtRegisteredClaimNames.GivenName, firstname),
            //     new Claim(JwtRegisteredClaimNames.FamilyName, lastname),
            //     new Claim(JwtRegisteredClaimNames.Email, email),
            //     new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            // };
            
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.GivenName, firstname),
                new Claim(ClaimTypes.Surname, lastname),
                new Claim(ClaimTypes.NameIdentifier, id.ToString())
            };

            Console.WriteLine(_jwtSettings.Secret);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256);


            var jwtSecurityToken = new JwtSecurityToken
            (
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(_jwtSettings.ExpiryDays),
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
