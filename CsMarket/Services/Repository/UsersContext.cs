using CsMarket.Models.Core;
using Microsoft.EntityFrameworkCore;

namespace CsMarket.Repository
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
