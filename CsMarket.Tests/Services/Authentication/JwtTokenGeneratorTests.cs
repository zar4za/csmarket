using CsMarket.Models.Authentication;
using CsMarket.Services.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CsMarket.Tests.Services.Authentication
{
    public class JwtTokenGeneratorTests
    {
        [Fact]
        public void SignedToken_ShouldEncodeCorrectly()
        {
            var settings = new JwtSettings
            {
                Audience = "CsMarket",
                Issuer = "CsMarket",
                ExpirationSeconds = 600,
                Secret = "supersecretpassword"
            };

            var from = DateTime.UtcNow;
            var expected = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "zar4za"),
                new Claim(ClaimTypes.Role, "Common"),
                new Claim(ClaimTypes.Sid, "d0b11228-b987-4b5e-933f-c1e4a6a669cf"),
                new Claim("SteamID64", "76561198106556563")
            };

            var generator = new JwtTokenGenerator(settings);

            var token = generator.SignToken(new ClaimsIdentity(expected), from);
 
            var handler = new JwtSecurityTokenHandler();

            var claims = handler.ValidateToken(
                token,
                new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = settings.Audience,
                    ValidIssuer = settings.Issuer,
                    ValidAlgorithms = new List<string> { SecurityAlgorithms.HmacSha256 },
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret))
                },
                out SecurityToken validToken);


            foreach (var claim in expected)
            {
                Assert.True(claims.HasClaim(claim.Type, claim.Value));
            }

        }
    }
}
