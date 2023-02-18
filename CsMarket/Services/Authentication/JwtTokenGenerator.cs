using CsMarket.Models.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CsMarket.Services.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly SigningCredentials _credentials;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expirationSeconds;
        private readonly JwtSecurityTokenHandler _handler = new JwtSecurityTokenHandler();
        public JwtTokenGenerator(JwtSettings settings)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret));
            _credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            _issuer = settings.Issuer;
            _audience = settings.Audience;
            _expirationSeconds = settings.ExpirationSeconds;
        }

        public string SignToken(ClaimsIdentity identity, DateTime from)
        {
            var jwt = new JwtSecurityToken(
                issuer: _issuer, 
                audience: _audience,
                claims: identity.Claims,
                notBefore: from,
                expires: from.AddSeconds(_expirationSeconds),
                signingCredentials: _credentials);

            return _handler.WriteToken(jwt);
        }
    }
}