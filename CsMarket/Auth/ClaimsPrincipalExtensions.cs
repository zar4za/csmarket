using System.Security.Claims;

namespace CsMarket.Auth
{
    public static class ClaimsPrincipalExtensions
    {
        public static long GetSteamId(this ClaimsPrincipal principal)
        {
            return long.Parse(principal.FindFirstValue(ClaimType.SteamId));
        }
    }
}
