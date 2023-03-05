﻿namespace CsMarket.Data
{
    public class ListingEFRepository : IListingRepository
    {
        private readonly MarketContext _context;

        public ListingEFRepository(MarketContext context)
        {
            _context = context;
        }

        public void AddListing(Listing listing)
        {
            _context.Listings.Add(listing);
            _context.SaveChanges();
        }
    }
}
