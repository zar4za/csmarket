using CsMarket.Market;
using CsMarket.Repository;
using CsMarket.Steam;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsMarket.Tests.Repository
{
    public class UsersContextTests
    {
        private const string TestConnection = "Host=localhost;Port=5432;Database=csmarket;Username=postgres;Password=postgres";
        private readonly DbContextOptions<UsersContext> _options = new DbContextOptionsBuilder<UsersContext>().UseNpgsql(TestConnection).Options;

        [Fact]
        public void StoredUserWithSteamId_ShouldGetCorrectValue()
        {
            using var context = new UsersContext(_options);
            context.Database.BeginTransaction();

            var expected = new User(new Guid(), new SteamId(76561199118590847), "TestName");

            context.Users.Add(expected);
            context.SaveChanges();

            var user = context.Users.Single(x => x.Id == expected.Id);

            Assert.Equal(expected.Id, user.Id);
            Assert.Equal(expected.Name, user.Name);
            Assert.Equal(expected.SteamId.SteamId32, user.SteamId.SteamId32);
        }
    }
}
