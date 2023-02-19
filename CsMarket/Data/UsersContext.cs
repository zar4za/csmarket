using CsMarket.Core;
using Microsoft.EntityFrameworkCore;

namespace CsMarket.Data
{
    public class UsersContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
