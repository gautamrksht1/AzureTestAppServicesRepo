﻿using ECommerce.Services.AuthAPI.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerce.Services.AuthAPI.Services.Implementations
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOptions _jwtOptions;

        public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }
        public string GenerateToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);
            var claims = new List<Claim> {
             new Claim(JwtRegisteredClaimNames.Email, user.Email),
             new Claim(JwtRegisteredClaimNames.Sub, user.Id),
             new Claim(JwtRegisteredClaimNames.Name, user.Name)
            };

            var tokenDescriptor = new SecurityTokenDescriptor()
            { 
             Audience = _jwtOptions.Audience,
             Issuer = _jwtOptions.Issuer,
             Subject = new ClaimsIdentity(claims),
             Expires = DateTime.UtcNow.AddDays(1),
             SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
