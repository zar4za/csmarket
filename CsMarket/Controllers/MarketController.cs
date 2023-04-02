using CsMarket.Auth;
using CsMarket.Market;
using CsMarket.Market.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CsMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketController : ControllerBase
    {
        private readonly IMarketService _market;

        public MarketController(IMarketService market)
        {
            _market = market;
        }

        [HttpPost("list")]
        [Authorize(Roles="Common,Seller,Admin")]
        public IActionResult ListItems(IEnumerable<InitListing> assetPrices)
        {
            var steamId = User.GetSteamId();
            try
            {
                foreach (var asset in assetPrices)
                {
                    _market.ListItem(steamId, asset.AssetId, asset.Price);
                }
                return Ok();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(new
                {
                    error = ex.Message
                });
            }
            catch (InvalidOperationException)
            {
                return BadRequest(new
                {
                    error = $"Asset is not tracked, try refreshing your inventory."
                });
            }
        }

        [HttpGet]
        public IActionResult GetListings(int count = 25, int offset = 0)
        {
            return Ok(new
            {
                listings = _market.GetActiveListings(count, offset)
            });
        }
    }
}
