using CsMarket.Data;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CsMarket.Market
{
    public class MarketService : IMarketService
    {
        private readonly MarketContext _marketContext;

        public MarketService(MarketContext context)
        {
            _marketContext = context;
        }

        public IEnumerable<Listing> GetActiveListings(int count, int offset, string? marketHashName = null)
        {
            return _marketContext.Listings
                .AsNoTracking()
                .Where(x => marketHashName == null || x.Asset.Class.MarketHashName == marketHashName)
                .Skip(offset)
                .Take(count)
                .ProjectToType<Listing>();
        }

        public void ListItem(long SteamId32, long assetId, decimal price)
        {
            if (assetId < 0)
                throw new ArgumentOutOfRangeException(nameof(assetId), assetId, "Must be positive.");
            if (price <= 0)
                throw new ArgumentOutOfRangeException(nameof(price), price, "Must be > 0.");

            var asset = _marketContext.Assets
                .Where(x => x.AssetId == assetId)
                .Single();

            if (asset == null)
                throw new NullReferenceException($"Asset with id {assetId} is not tracked.");

            var listing = new Data.Entities.Listing()
            {
                Id = Guid.NewGuid(),
                Asset = asset,
                Price = price,
                State = ListingState.Listed
            };

            _marketContext.Listings.Add(listing);
            _marketContext.SaveChanges();
        }

        public void UpdateItem(long SteamId32, long assetId, decimal price)
        {
            throw new NotImplementedException();
        }
        public void DeleteItem(long SteamId32, long assetId)
        {
            throw new NotImplementedException();
        }
    }
}
