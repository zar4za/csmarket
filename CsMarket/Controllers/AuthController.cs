using CsMarket.Models.Core;
using CsMarket.Services;
using CsMarket.Services.Authentication;
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

        private readonly Regex _accountIdRegex = new(@"^https?://steamcommunity\.com/openid/id/(7[0-9]{15,25})$", RegexOptions.Compiled);

        public AuthController(IJwtTokenGenerator generator, IChallengeProvider provider)
        {
            _tokenGen = generator;
            _provider = provider;
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

                var steamId = accountIdMatch.Groups[1].Value;

                var newClaims = new List<Claim>
                {
                    new Claim("steamid", steamId)
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
