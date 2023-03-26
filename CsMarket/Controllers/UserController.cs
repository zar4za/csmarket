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

        [HttpGet("inventory")]
        public IActionResult GetInventory()
        {
            try
            {
                var inventory = _inventoryFactory.GetInventory(User.GetSteamId());

                return Ok(inventory);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex);
            }
        }
    }
}
