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
        private readonly IDescriptionStorage _descriptionStorage;

        public MarketController(IInventoryFactory factory, IMarketService market, IDescriptionStorage storage)
        {
            _inventoryFactory = factory;
            _market = market;
            _descriptionStorage = storage;
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

        [HttpGet("listings")]
        public IActionResult GetListings(int count = 25)
        {
            var listings = _market.GetListings(count);

            var result = listings.Select(x => new
            {
                assetid = x.AssetId,
                classid = x.ClassId,
                icon_url = _descriptionStorage.GetDescription(x.InstanceId, x.ClassId).IconUrl,
                instanceid = x.InstanceId,
                market_hash_name = _descriptionStorage.GetDescription(x.InstanceId, x.ClassId).MarketHashName,
                price = x.Price
            });

            return Ok(result);
        }
    }
}
