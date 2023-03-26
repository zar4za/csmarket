namespace CsMarket.Market
{
    public interface IInitialListing
    {
        decimal Price { get; init; }

        long AssetId { get; init; }
    }
}
