using CsMarket.Models.Core;
using CsMarket.Services.Repository;
using CsMarket.Steam;
using System.Security.Claims;

namespace CsMarket.Services.Authentication
{
    public class AuthService
    {
        private const Role DefaultRole = Role.Common;

        private readonly IUserRepository _repository;

        public AuthService(IUserRepository repository)
        {
            _repository = repository;
        }

        public ClaimsIdentity CreateUser(SteamId steamId, string name)
        {
            var user = new User(new Guid(), name, DefaultRole)
            {
                SteamId = steamId
            };

            try
            {
                _repository.AddUser(user);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Failed to create user with such SteamID.", nameof(steamId), e);
            }

            return GetClaims(user);
        }

        public ClaimsIdentity LoginUser(SteamId steamId)
        {
            var user = _repository.FindUser(steamId);

            return GetClaims(user);
        }

        private static ClaimsIdentity GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim("SteamID64", user.SteamId!.SteamId64.ToString())
            };

            return new ClaimsIdentity(claims);
        }
    }
}
