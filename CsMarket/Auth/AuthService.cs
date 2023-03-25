using CsMarket.Auth.Jwt;
using CsMarket.Core;
using CsMarket.Data;
using CsMarket.Data.Entities;
using CsMarket.Steam;
using System.Security.Claims;

namespace CsMarket.Auth
{
    public class AuthService
    {


        private readonly IChallengeProvider _provider;
        private readonly IJwtTokenGenerator _tokenGen;
        private readonly IUserRepository _repository;
        private readonly IUserSummaryProvider _userProvider;

        public string RequestUri => _provider.RequestUri;

        public AuthService(IChallengeProvider provider, IJwtTokenGenerator generator, IUserRepository repository, IUserSummaryProvider userProvider)
        {
            _provider = provider;
            _tokenGen = generator;
            _repository = repository;
            _userProvider = userProvider;
        }

        public string SignInUser(Dictionary<string, string> claims)
        {
            var isValid = _provider.VerifyOwnership(claims);

            if (!isValid)
                throw new Exception("Cannot verify OpenID request.");

            var format = new SteamIdFormatter(claims[_provider.IdClaimName]);

            if (!_repository.FindUser(format.ToSteamId32(), out Data.Entities.User user))
            {
                var summary = _userProvider.GetUserSummary(format.ToSteamId64());

                user = new Data.Entities.User()
                {
                    SteamId32 = format.ToSteamId32(),
                    Name = summary.Name,
                    Role = Role.Common,
                    AvatarHash = summary.AvatarUri,
                    SignupUnixMilli = summary.RegisterTimestamp
                };

                _repository.AddUser(user);
            }

            var newClaims = new List<Claim>
            {
                new Claim(ClaimType.Name, user.Name),
                new Claim(ClaimType.Role, user.Role.ToString()),
                new Claim(ClaimType.SteamId, format.ToSteamId64().ToString())
            };

            var token = _tokenGen.SignToken(new ClaimsIdentity(newClaims), DateTime.UtcNow);

            return token;
        }
    }
}
