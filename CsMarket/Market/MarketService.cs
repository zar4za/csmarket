using CsMarket.Core;
using CsMarket.Data;

namespace CsMarket.Market
{
    public class MarketService : IMarketService
    {
        private readonly IListingRepository _repository;

        public MarketService(IListingRepository repository)
        {
            _repository = repository;
        }

        public void PutOnSale(Item item, decimal price)
        {
            var listing = new Data.Listing()
            {
                AssetId = item.AssetId,
                ClassId = item.ClassId,
                InstanceId = item.InstanceId,
                Price = price,
            };

            _repository.AddListing(listing);
        }
    }
}
