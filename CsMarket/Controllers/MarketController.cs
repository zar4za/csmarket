using CsMarket.Auth;
using CsMarket.Core;
using CsMarket.Market;
using CsMarket.Steam.Inventory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace CsMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketController : ControllerBase
    {
        private readonly IInventoryFactory _inventoryFactory;
        private readonly IMarketService _market;

        public MarketController(IInventoryFactory factory, IMarketService market)
        {
            _inventoryFactory = factory;
            _market = market;
        }

        [HttpPost("list")]
        public IActionResult PostListing(IEnumerable<Listing> listings)
        {
            if (!listings.Any())
            {
                return BadRequest("no listings");
            }

            IEnumerable<Item> inventory = new List<Item>();

            try
            {
                var steamIdClaim = HttpContext.User.FindFirst(ClaimType.SteamId)?.Value;
                var steamId64 = long.Parse(steamIdClaim!);
                inventory = _inventoryFactory.GetInventory(steamId64);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            foreach (var listing in listings)
            {
                var item = inventory.FirstOrDefault(x => x.AssetId == listing.AssetId);

                if (item == null) continue;

                _market.PutOnSale(item, listing.Price);
            }

            return Ok();
        }
    }
}
