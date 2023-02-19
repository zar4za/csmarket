using CsMarket.Auth;
using CsMarket.Auth.Jwt;
using CsMarket.Core;
using CsMarket.Data;
using CsMarket.Steam;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace CsMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenGenerator _tokenGen;
        private readonly IChallengeProvider _provider;
        private readonly IUserRepository _repository;

        private readonly Regex _accountIdRegex = new(@"^https?://steamcommunity\.com/openid/id/(7[0-9]{15,25})$", RegexOptions.Compiled);

        public AuthController(IJwtTokenGenerator generator, IChallengeProvider provider, IUserRepository repository)
        {
            _tokenGen = generator;
            _provider = provider;
            _repository = repository;
        }

        [Authorize(Roles = "Common")]
        [HttpGet("ping")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Ping()
        {
            var response = new
            {
                Message = "pong"
            };

            return Ok(response);
        }

        [HttpGet("request")]
        public IActionResult RequestChallengeUri()
        {
            var response = new
            {
                Url = _provider.RequestUri
            };

            return Ok(response);
        }

        [HttpGet("complete")]
        public IActionResult CompleteChallenge()
        {
            var claims = Request.Query.ToDictionary(c => c.Key, c => c.Value.ToString());

            try
            {
                var isValid = _provider.VerifyOwnership(claims);

                if (!isValid) throw new Exception("Cannot verify OpenID request.");

                var format = new SteamIdFormatter(claims["openid.identity"]);

                if (!_repository.FindUser(format.ToSteamId32(), out User user))
                {
                    user = new User(Guid.NewGuid(), format.ToSteamId32(), "TestName", Role.Common);
                    _repository.AddUser(user);
                }

                var newClaims = new List<Claim>
                {
                    new Claim(ClaimType.Guid, user.Id.ToString()),
                    new Claim(ClaimType.Name, user.Name),
                    new Claim(ClaimType.Role, user.Role.ToString()),
                    new Claim(ClaimType.SteamId, format.ToSteamId64().ToString())
                };

                var token = _tokenGen.SignToken(new ClaimsIdentity(newClaims), DateTime.UtcNow);

                return Ok(new
                {
                    message = "success",
                    token
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    message = e.Message
                });
            }
        }
    }
}
