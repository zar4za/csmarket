using CsMarket.Auth;
using CsMarket.Market;
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

        [HttpPost]
        [Authorize(Roles="Common,Seller,Admin")]
        public IActionResult ListItem(Listing listing)
        {
            _market.ListItem(User.GetSteamId(), listing);

            return Ok();
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
