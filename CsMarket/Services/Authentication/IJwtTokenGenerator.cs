using System.Security.Claims;

namespace CsMarket.Services.Authentication
{
    public interface IJwtTokenGenerator
    {
        string SignToken(ClaimsIdentity identity, DateTime from);
    }
}