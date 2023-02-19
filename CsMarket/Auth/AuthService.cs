using CsMarket.Core;
using CsMarket.Data;
using CsMarket.Steam;
using System.Security.Claims;

namespace CsMarket.Auth
{
    public class AuthService
    {
        private const Role DefaultRole = Role.Common;

        private readonly IUserRepository _repository;

        public AuthService(IUserRepository repository)
        {
            _repository = repository;
        }

        public ClaimsIdentity CreateUser(int steamId, string name)
        {
            var user = new User(Guid.NewGuid(), steamId, name, DefaultRole);

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

        public ClaimsIdentity LoginUser(int steamId)
        {
            var user = _repository.GetUser(steamId);

            return GetClaims(user);
        }

        private static ClaimsIdentity GetClaims(User user)
        {
            var format = new SteamIdFormatter(user.SteamId);
            var claims = new List<Claim>
            {
                new Claim(ClaimType.Name, user.Name),
                new Claim(ClaimType.Role, user.Role.ToString()),
                new Claim(ClaimType.Guid, user.Id.ToString()),
                new Claim(ClaimType.SteamId, format.ToSteamId64().ToString())
            };

            return new ClaimsIdentity(claims);
        }
    }
}
