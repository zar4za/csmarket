using System.Security.Claims;

namespace CsMarket.Auth.Jwt
{
    public interface IJwtTokenGenerator
    {
        string SignToken(ClaimsIdentity identity, DateTime from);
    }
}