using CsMarket.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CsMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _auth;

        public AuthController(AuthService auth)
        {
            _auth = auth;
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
                Url = _auth.RequestUri
            };

            return Ok(response);
        }

        [HttpGet("complete")]
        public IActionResult CompleteChallenge()
        {
            var claims = Request.Query.ToDictionary(c => c.Key, c => c.Value.ToString());

            try
            {
                var token = _auth.SignInUser(claims);

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
