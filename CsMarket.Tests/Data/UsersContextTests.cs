using CsMarket.Core;
using CsMarket.Data;
using CsMarket.Steam;
using Microsoft.EntityFrameworkCore;

namespace CsMarket.Tests.Data
{
    public class UsersContextTests
    {
        private const string TestConnection = "Host=localhost;Port=5432;Database=csmarket;Username=postgres;Password=postgres";
        private readonly DbContextOptions<UsersContext> _options = new DbContextOptionsBuilder<UsersContext>().UseNpgsql(TestConnection).Options;

        [Fact]
        public void StoredWithEFCore_ShouldHaveSameValues()
        {
            using var context = new UsersContext(_options);
            context.Database.EnsureCreated();
            context.Database.BeginTransaction();

            var expected = new User(new Guid(), "TestName", Role.Common)
            {
                SteamId = new SteamId(76561199118590847)
            };

            context.Users.Add(expected);
            context.SaveChanges();

            var user = context.Users.Single();

            Assert.Equal(expected.Name, user.Name);
            Assert.Equal(expected.SteamId.SteamId32, user.SteamId?.SteamId32);
            Assert.Equal(expected.Id, user.Id);
        }
    }
}
