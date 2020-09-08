using JwtApiTest.Application.Interfaces;
using JwtApiTest.Domain.Infracstruture.JwtToken;
using JwtApiTest.Domain.JwtToken;
using JwtApiTest.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;

namespace JwtApiTest.Application.Utils
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _jwtSettings;

        public JwtService(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public JsonWebToken CreateJsonWebToken(User user)
        {
            var identity = GetClaimsIdentity(user);
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = identity,
                Issuer = _jwtSettings.Issuer,
                IssuedAt = _jwtSettings.IssuedAt,
                Expires = _jwtSettings.AccessTokenExpiration,
                Audience = _jwtSettings.Audience,
                NotBefore = _jwtSettings.NotBefore,
                SigningCredentials = _jwtSettings.SigningCredentials                
            });

            var accessToken = handler.WriteToken(securityToken);

            return new JsonWebToken
            {
                AccessToken = accessToken,
                RefreshToken = CreateRefreshToken(user.Email),
                ExpiresIn = (long)TimeSpan.FromMinutes(_jwtSettings.ValidForMinutes).TotalSeconds
            };
        }

        private RefreshToken CreateRefreshToken(string username)
        {
            var refreshToken = new RefreshToken
            {
                Username = username,
                ExpirationDate = _jwtSettings.RefreshTokenExpiration
            };

            string token;
            var randomNumber = new byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                token = Convert.ToBase64String(randomNumber);
            }

            refreshToken.Token = token.Replace("+", string.Empty)
                .Replace("=", string.Empty)
                .Replace("/", string.Empty);

            return refreshToken;
        }

        private static ClaimsIdentity GetClaimsIdentity(User user)
        {
            var identity = new ClaimsIdentity(new GenericIdentity(user.Email),
                new[] {new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                       new Claim(JwtRegisteredClaimNames.Sub, user.Name)
                }
                );

            foreach (var roles in user.Roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, roles));
            }

            foreach (var permission in user.Permissions)
            {
                identity.AddClaim(new Claim("permissions", permission));
            }

            return identity;

        }
    }
}
