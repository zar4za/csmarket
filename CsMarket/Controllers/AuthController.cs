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


        [HttpGet("request")]
        public IActionResult RequestChallenge()
        {
            var response = new
            {
                url = _auth.RequestUri
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
