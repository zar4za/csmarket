using CsMarket.Steam;
using Microsoft.Extensions.Configuration;
using Moq;
using RichardSzalay.MockHttp;
using System.Text;

namespace CsMarket.Tests.Steam
{
    public class SteamWebApiClientTests
    {
        [Fact]
        public void GetUserSummary_ValidSteamId_ShoulReturnUserSummary()
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes("{\"response\": {\"players\": [{\"steamid\": \"76561199207781376\",\"communityvisibilitystate\": 3,\"profilestate\": 1,\"personaname\": \"Chronoa\",\"commentpermission\": 1,\"profileurl\": \"https://steamcommunity.com/profiles/76561199207781376/\",\"avatar\": \"https://avatars.akamai.steamstatic.com/efe94c226c3013a696c6516f3af29405251991bd.jpg\",\"avatarmedium\": \"https://avatars.akamai.steamstatic.com/efe94c226c3013a696c6516f3af29405251991bd_medium.jpg\",\"avatarfull\": \"https://avatars.akamai.steamstatic.com/efe94c226c3013a696c6516f3af29405251991bd_full.jpg\",\"avatarhash\": \"efe94c226c3013a696c6516f3af29405251991bd\",\"lastlogoff\": 1676898978,\"personastate\": 1,\"primaryclanid\": \"103582791429521408\",\"timecreated\": 1631721009,\"personastateflags\": 0}]}}"));
            var handler = new MockHttpMessageHandler();
            handler.When("https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v2/?key=key&steamids=76561199207781376").Respond("application/json", stream);
            var config = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string> { { "Steam:WebApiKey", "key" } }).Build();
            var client = new HttpClient(handler);

            var mockFactory = new Mock<IHttpClientFactory>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var apiClient = new SteamWebApiClient(mockFactory.Object, config);



            var user = apiClient.GetUserSummary(76561199207781376);

            Assert.NotNull(user);
            Assert.Equal("Chronoa", user.Name);
            Assert.Equal("https://avatars.akamai.steamstatic.com/efe94c226c3013a696c6516f3af29405251991bd.jpg", user.AvatarUri);
            Assert.Equal(1631721009, user.RegisterTimestamp);
        }
    }
}
