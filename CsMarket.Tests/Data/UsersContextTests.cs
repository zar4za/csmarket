using CsMarket.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CsMarket.Tests.Data
{
    public class UsersContextTests
    {
        [Fact]
        public void Constructor_InMemoryDatabase_UsersNotNull()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<MarketContext>()
                .UseSqlite(connection)
                .Options;

            using var context = new MarketContext(options);

            Assert.NotNull(context.Users);
            connection.Dispose();
        }
    }
}
