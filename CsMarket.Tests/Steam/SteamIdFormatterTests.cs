using CsMarket.Steam;

namespace CsMarket.Tests.Steam
{
    public class SteamIdFormatterTests
    {
        [Theory]
        [InlineData(76561199118590847, 1158325119, "STEAM_0:1:579162559")]
        [InlineData(76561198106556563, 146290835, "STEAM_0:1:73145417")]
        [InlineData(76561199367508734, 1407243006, "STEAM_0:0:703621503")]
        public void CreatedWithSteamId64_ShouldConvertCorrectly(long steamId64, int steamId32Expected, string steamIdExpected)
        {
            var format = new SteamIdFormatter(steamId64);

            Assert.Equal(steamId32Expected, format.ToSteamId32());
            Assert.Equal(steamId64, format.ToSteamId64());
            Assert.Equal(steamIdExpected, format.ToSteamId3());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(76561197960265727)]
        [InlineData(-4346667578888675)]
        public void CreatedWithInvalidSteamId64_ShouldThrowArgumentException(long steamId64)
        {
            Assert.Throws<ArgumentException>(
                () => new SteamIdFormatter(steamId64));
        }

        [Theory]
        [InlineData(1158325119, 76561199118590847, "STEAM_0:1:579162559")]
        [InlineData(146290835, 76561198106556563, "STEAM_0:1:73145417")]
        [InlineData(1407243006, 76561199367508734, "STEAM_0:0:703621503")]
        public void CreatedWithSteamId32_ShouldConvertCorrectly(int steamId32, long steamId64Expected, string steamIdExpected)
        {
            var format = new SteamIdFormatter(steamId32);

            Assert.Equal(steamId32, format.ToSteamId32());
            Assert.Equal(steamId64Expected, format.ToSteamId64());
            Assert.Equal(steamIdExpected, format.ToSteamId3());
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-534534)]
        public void CreatedWithInvalidSteamId32_ShouldThrowArgumentException(int steamId32)
        {
            Assert.Throws<ArgumentException>(
                () => new SteamIdFormatter(steamId32));
        }

        [Theory]
        [InlineData("https://steamcommunity.com/openid/id/76561199118590847", 1158325119, 76561199118590847)]
        [InlineData("https://steamcommunity.com/openid/id/76561198106556563", 146290835, 76561198106556563)]
        [InlineData("https://steamcommunity.com/openid/id/76561199367508734", 1407243006, 76561199367508734)]
        public void CreatedWithSteamIdUri_ShouldConvertCorrectly(string uri, int steamId32Expected, long steamId64Expected)
        {
            var format = new SteamIdFormatter(uri);

            Assert.Equal(steamId32Expected, format.ToSteamId32());
            Assert.Equal(steamId64Expected, format.ToSteamId64());
        }

        [Theory]
        [InlineData("https://steamcommunity.com/openid/id/76561197960265727")]
        public void CreatedWithInvalidSteamIdString_ShouldThrowArgumentException(string uri)
        {
            Assert.Throws<ArgumentException>(
                () => new SteamIdFormatter(uri));
        }
    }
}
