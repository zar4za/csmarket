using CsMarket.Core;
using CsMarket.Data;
using CsMarket.Data.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CsMarket.Tests.Data
{
    public class UserEFRepositoryTests
    {
        [Fact]
        public void AddUser_UserNotNull_ShouldSaveChanges()
        {
            using var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<CsMarketContext>()
                .UseSqlite(connection)
                .Options;

            using var context = new CsMarketContext(options);

            var user = new CsMarket.Data.Entities.User()
            {
                SteamId32 = 1000,
                Name = "testname",
                Role = Role.Common,
                AvatarHash = "http://uri",
                SignupUnixMilli = 10000
            };

            var repo = new EFCoreRepository(context);

            repo.AddUser(user);

            Assert.Equal(user, context.Users.Single(x => x.SteamId32 == user.SteamId32));
        }

        [Fact]
        public void FindUser_UserExists_ShouldReturnTrue()
        {
            using var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<CsMarketContext>()
                .UseSqlite(connection)
                .Options;

            using var context = new CsMarketContext(options);

            var expected = new CsMarket.Data.Entities.User()
            {
                SteamId32 = 1000,
                Name = "testname",
                Role = Role.Common,
                AvatarHash = "http://uri",
                SignupUnixMilli = 10000
            };

            context.Users.Add(expected);
            context.SaveChanges();

            var repo = new EFCoreRepository(context);

            var hasFound = repo.FindUser(expected.SteamId32, out CsMarket.Data.Entities.User user);

            Assert.True(hasFound);
            Assert.Equal(expected, user);
        }

        [Fact]
        public void FindUser_UserDoesntExist_ShouldReturnFalse()
        {
            using var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<CsMarketContext>()
                .UseSqlite(connection)
                .Options;

            var expected = new CsMarket.Data.Entities.User()
            {
                SteamId32 = 1000,
                Name = "testname",
                Role = Role.Common,
                AvatarHash = "http://uri",
                SignupUnixMilli = 10000
            };

            using var context = new CsMarketContext(options);


            var repo = new EFCoreRepository(context);

            var hasFound = repo.FindUser(expected.SteamId32, out CsMarket.Data.Entities.User user);

            Assert.False(hasFound);
            Assert.Null(user);
        }
    }
}
