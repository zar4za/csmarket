using CsMarket.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CsMarket.Data
{
    public class EFCoreRepository : IUserRepository, IListingRepository
    {
        private readonly CsMarketContext _context;

        public EFCoreRepository(CsMarketContext context)
        {
            _context = context;
        }

        public void AddListing(Listing listing)
        {
            _context.Listings.Add(listing);
            _context.SaveChanges();
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public bool FindUser(int steamId, out User user)
        {
            try
            {
                user = _context.Users.First(x => x.SteamId32 == steamId);
                return true;
            }
            catch
            {
                user = null!;
                return false;
            }
        }

        public IQueryable<Listing> GetActiveListings(int count, int offset, string? marketHashName = null)
        {
            var query = _context.Listings.AsNoTracking();

            if (marketHashName != null)
                query = query.Where(l => l.Asset.ClassName.MarketHashName == marketHashName);

            return query
                .Skip(offset)
                .Take(count);
        }

        public Asset? SingleAsset(long assetId)
        {
            try
            {
                return _context.Assets.Where(x => x.AssetId == assetId).First();
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }
    }
}
