using CsMarket.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CsMarket.Data
{
    public class IdentityContext : DbContext
    {
        public DbSet<User> Users { get; init; } = null!;

        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
            Database.Migrate();
        }
    }
}
