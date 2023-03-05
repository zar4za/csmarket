using CsMarket.Core;
using CsMarket.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace CsMarket.Data
{
    public class MarketContext : DbContext
    {
        public DbSet<User> Users { get; init; } = null!;
        public DbSet<Listing> Listings { get; init; } = null!;

        public DbSet<Description> Descriptions { get; init; } = null!;

        public MarketContext(DbContextOptions<MarketContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
