using System.Text.Json.Serialization;

namespace CsMarket.Market
{
    public class Listing : IInitialListing
    {
        public Guid? ListingId { get; init; }


        public decimal Price { get; init; }

        public long AssetId { get; init; }

        public string? IconHash { get; init; }

        public string? MarketHashName { get; init; }
    }
}
