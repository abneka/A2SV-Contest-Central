using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Contracts.Infrastructure;
using Application.Exceptions;
using Application.Models.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.JwtAuthentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtSettings _jwtSettings;

        public AuthenticationService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public LoginResponse Login(LoginRequest user)
        {
            // if (!BCrypt.Net.BCrypt.Verify(user.LoginPassword, user.OriginalPassword))
            // {
            //     throw new PasswordMismatch("User name or password does not match");
            // }
            
            if(user.LoginPassword != user.OriginalPassword)
            {
                throw new PasswordMismatch("User name or password does not match");
            }

            string token = GenerateTokenAsync(user);

            var response = new LoginResponse
            {
                Id = user.Id,
                Token = token,
                Email = user.Email!,
                FirstName = user.FirstName!,
                LastName = user.LastName!,
                UserName = user.UserName!
            };

            return response;
        }

        public string GenerateTokenAsync(LoginRequest user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("http://schemas.example.com/claims/priority", user.Priority.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidateTokenAsync(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);

            try
            {
                tokenHandler.ValidateToken(
                    token,
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = _jwtSettings.Issuer,
                        ValidAudience = _jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    },
                    out SecurityToken validatedToken
                );

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
