using CsMarket.Data.Entities;
using System.Linq;

namespace CsMarket.Data
{
    public interface IListingRepository
    {
        IQueryable<Listing> GetActiveListings(int count, int offset, string? marketHashName = null);
    }
}
