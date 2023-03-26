using CsMarket.Data;
using Mapster;
using MapsterMapper;

namespace CsMarket.Market
{
    public class MarketService : IMarketService
    {
        private readonly IListingRepository _repository;

        public MarketService(IListingRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Listing> GetActiveListings(int count, int offset, string? marketHashName = null)
        {
            var query = _repository.GetActiveListings(count, offset, marketHashName);
            return query.ProjectToType<Listing>();
        }

        public void ListItem(long SteamId32, Listing listing)
        {
            throw new NotImplementedException();
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
