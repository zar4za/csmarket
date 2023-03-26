using CsMarket.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CsMarket.Data
{
    public class CsMarketContext : DbContext
    {
        public DbSet<User> Users { get; init; } = null!;
        public DbSet<Listing> Listings { get; init; } = null!;

        public CsMarketContext(DbContextOptions<CsMarketContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //}
    }
}
