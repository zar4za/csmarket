using CsMarket.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CsMarket.Data
{
    public class MarketContext : DbContext
    {
        public DbSet<Listing> Listings { get; init; } = null!;
        public DbSet<Asset> Assets { get; init; } = null!;

        public DbSet<User> Users { get; init; } = null!;

        public MarketContext(DbContextOptions<MarketContext> options) : base(options)
        {
        }
    }
}
