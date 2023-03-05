using CsMarket.Core;
using Microsoft.EntityFrameworkCore;

namespace CsMarket.Data
{
    public class MarketContext : DbContext
    {
        public DbSet<User> Users { get; init; } = null!;
        public DbSet<Listing> Listings { get; init; } = null!;

        public MarketContext(DbContextOptions<MarketContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
