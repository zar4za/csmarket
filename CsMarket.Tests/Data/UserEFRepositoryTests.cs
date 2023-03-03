using CsMarket.Core;
using CsMarket.Data;
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

            var options = new DbContextOptionsBuilder<UsersContext>()
                .UseSqlite(connection)
                .Options;

            using var context = new UsersContext(options);

            var user = new User(Guid.NewGuid(), 1000, "testname", Role.Common)
            {
                AvatarUri = "http://uri",
                RegisterTimestamp = 10000
            };

            var repo = new UserEFRepository(context);

            repo.AddUser(user);

            Assert.Equal(user, context.Users.Single(x => x.Id == user.Id));
        }

        [Fact]
        public void FindUser_UserExists_ShouldReturnTrue()
        {
            using var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<UsersContext>()
                .UseSqlite(connection)
                .Options;

            using var context = new UsersContext(options);

            var expected = new User(Guid.NewGuid(), 1000, "testname", Role.Common)
            {
                AvatarUri = "http://uri",
                RegisterTimestamp = 10000
            };
            context.Users.Add(expected);
            context.SaveChanges();

            var repo = new UserEFRepository(context);

            var hasFound = repo.FindUser(expected.SteamId, out User user);

            Assert.True(hasFound);
            Assert.Equal(expected, user);
        }

        [Fact]
        public void FindUser_UserDoesntExist_ShouldReturnFalse()
        {
            using var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<UsersContext>()
                .UseSqlite(connection)
                .Options;

            var expected = new User(Guid.NewGuid(), 1000, "testname", Role.Common)
            {
                AvatarUri = "http://uri",
                RegisterTimestamp = 10000
            };

            using var context = new UsersContext(options);


            var repo = new UserEFRepository(context);

            var hasFound = repo.FindUser(expected.SteamId, out User user);

            Assert.False(hasFound);
            Assert.Null(user);
        }
    }
}
