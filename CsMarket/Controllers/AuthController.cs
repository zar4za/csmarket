using CsMarket.Models.Core;
using CsMarket.Services;
using CsMarket.Services.Authentication;
using CsMarket.Services.Repository;
using CsMarket.Steam;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [Authorize]
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

                var identity = claims["openid.identity"];
                var accountIdMatch = _accountIdRegex.Match(identity);

                if (!accountIdMatch.Success) throw new Exception("Cannot capture SteamID from OpenID claim.");

                var steamId = new SteamId(long.Parse(accountIdMatch.Groups[1].Value));

                if (!_repository.FindUser(steamId, out User user))
                {
                    user = new User(Guid.NewGuid(), "TestName", Role.Common)
                    {
                        SteamId = steamId
                    };

                    _repository.AddUser(user);
                }

                var newClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.PrimarySid, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                    new Claim("steamid", user.SteamId.SteamId64.ToString())
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
