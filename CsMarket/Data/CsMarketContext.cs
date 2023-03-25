using CsMarket.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CsMarket.Data
{
    public class CsMarketContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public CsMarketContext(DbContextOptions<CsMarketContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
