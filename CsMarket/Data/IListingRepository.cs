namespace CsMarket.Data
{
    public interface IListingRepository
    {
        void AddListing(Listing listing);

        IEnumerable<Listing> GetListings(int count);
    }
}
