﻿namespace CsMarket.Market
{
    public class Listing
    {
        public Guid ListingId { get; init; }


        public decimal Price { get; init; }

        public long AssetId { get; init; }

        public string IconHash { get; init; } = null!;

        public string MarketHashName { get; init; } = null!;
    }
}
