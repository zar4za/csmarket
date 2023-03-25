using CsMarket.Auth;
using CsMarket.Core;
using CsMarket.Steam.Inventory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CsMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IInventoryFactory _inventoryFactory;

        public UserController(IInventoryFactory factory)
        {
            _inventoryFactory = factory;
        }

        [HttpGet]
        public IActionResult GetInventory()
        {
            try
            {
                var steamIdClaim = HttpContext.User.FindFirst(ClaimType.SteamId)?.Value;
                var steamId64 = long.Parse(steamIdClaim!);
                var inventory = _inventoryFactory.GetInventory(steamId64);

                return Ok(inventory);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex);
            }
        }
    }
}
