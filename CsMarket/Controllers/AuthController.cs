using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CsMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
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

        [Authorize]
        [HttpGet("secure-ping")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult SecurePing()
        {
            var response = new
            {
                Message = "secure-pong"
            };

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin-ping")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult AdminPing()
        {
            var response = new
            {
                Message = "admin-pong"
            };

            return Ok(response);
        }
    }
}
