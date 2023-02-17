using CsMarket.Steam;
using Microsoft.EntityFrameworkCore;

namespace CsMarket.Tests.Steam
{
    public class SteamIdTests
    {
        [Theory]
        [InlineData(76561199118590847, 1158325119, "STEAM_0:1:579162559")]
        [InlineData(76561198106556563, 146290835, "STEAM_0:1:73145417")]
        [InlineData(76561199367508734, 1407243006, "STEAM_0:0:703621503")]
        public void CreatedWithSteamId64_ShouldConvertCorrectly(long steamId64, int steamId32Expected, string steamIdExpected)
        {
            var steamId = new SteamId(steamId64);

            var steamId32 = steamId.SteamId32;
            var steamIdText = steamId.SteamIdText;

            Assert.Equal(steamId32Expected, steamId32);
            Assert.Equal(steamIdExpected, steamIdText);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(76561197960265728)]
        [InlineData(-4346667578888675)]
        public void CreatedWithInvalidSteamId64_ShouldThrowArgumentException(long steamId64)
        {
            Assert.Throws<ArgumentException>(
                () => new SteamId(steamId64));
        }

        [Theory]
        [InlineData(1158325119, 76561199118590847, "STEAM_0:1:579162559")]
        [InlineData(146290835, 76561198106556563, "STEAM_0:1:73145417")]
        [InlineData(1407243006, 76561199367508734, "STEAM_0:0:703621503")]
        public void CreatedWithSteamId32_ShouldConvertCorrectly(int steamId32, long steamId64Expected, string steamIdExpected)
        {
            var steamId = new SteamId(steamId32);

            var steamId64 = steamId.SteamId64;
            var steamIdText = steamId.SteamIdText;

            Assert.Equal(steamId64Expected, steamId64);
            Assert.Equal(steamIdExpected, steamIdText);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-534534)]
        public void CreatedWithInvalidSteamId32_ShouldThrowArgumentException(int steamId32)
        {
            Assert.Throws<ArgumentException>(
                () => new SteamId(steamId32));
        }

        [Theory]
        [InlineData("STEAM_0:1:579162559", 1158325119, 76561199118590847)]
        [InlineData("STEAM_0:1:73145417", 146290835, 76561198106556563)]
        [InlineData("STEAM_0:0:703621503", 1407243006, 76561199367508734)]
        public void CreatedWithSteamIdString_ShouldConvertCorrectly(string steamIdText, int steamId32Expected, long steamId64Expected)
        {
            var steamId = new SteamId(steamIdText);

            var steamId32 = steamId.SteamId32;
            var steamId64 = steamId.SteamId64;

            Assert.Equal(steamId32Expected, steamId32);
            Assert.Equal(steamId64Expected, steamId64);
            Assert.Equal(steamIdText, steamId.SteamIdText);
        }

        [Theory]
        [InlineData("STEAM_1:1:66138017")]
        [InlineData("STEAM_01579162559")]
        [InlineData("STEAM_0:173145417")]
        [InlineData("STEAM_0:1000000:73145417")]
        [InlineData("STEAM_0:0:-100000")]
        public void CreatedWithInvalidSteamIdString_ShouldThrowArgumentException(string steamIdText)
        {
            Assert.Throws<ArgumentException>(
                () => new SteamId(steamIdText));
        }

        [Fact]
        public void StoredWithEFCore_ShouldHaveSameValues()
        {
            using var context = new SteamIdContext();
            context.Database.EnsureCreated();
            context.Database.BeginTransaction();

            var expected = new SteamId(76561199118590847);

            context.SteamIds.Add(expected);
            context.SaveChanges();

            var steamId = context.SteamIds.Single();

            Assert.Equal(expected.SteamId32, steamId.SteamId32);
            Assert.Equal(expected.SteamId64, steamId.SteamId64);
            Assert.Equal(expected.SteamIdText, steamId.SteamIdText);

            context.Database.EnsureDeleted();
        }
    }

    internal class SteamIdContext : DbContext
    {
        private const string TestConnection = "Host=localhost;Port=5432;Database=csmarket;Username=postgres;Password=postgres";

        public DbSet<SteamId> SteamIds { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(TestConnection);
        }
    }
}
