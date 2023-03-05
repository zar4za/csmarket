using CsMarket.Core;

namespace CsMarket.Market
{
    public interface IMarketService
    {
        void PutOnSale(Item item, decimal price);

        IEnumerable<Data.Entity.Listing> GetListings(int count);
    }
}
