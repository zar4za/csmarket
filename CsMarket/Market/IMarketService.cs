using CsMarket.Core;

namespace CsMarket.Market
{
    public interface IMarketService
    {
        void ListItem(long SteamId32, IInitialListing listing);

        void UpdateItem(long SteamId32, long assetId, decimal price);

        void DeleteItem(long SteamId32, long assetId);

        IEnumerable<Listing> GetActiveListings(int count, int offset, string? marketHashName = null);
    }
}