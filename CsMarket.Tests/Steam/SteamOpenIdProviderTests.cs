using CsMarket.Steam;
using Microsoft.Extensions.Configuration;
using Moq;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsMarket.Tests.Steam
{
    public class SteamOpenIdProviderTests
    {
        [Fact]
        public void VerifyOwnership_ClaimsDontContainMode_ShouldThrowArgumentException()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string> 
                {
                    { "ReturnTo", "http://returnuri" },
                    { "Realm", "http://realmuri" }
                })
                .Build();

            var handler = new MockHttpMessageHandler();
            handler.When("https://steamcommunity.com/openid/login?openid.identity=76561199207781376")
                .Respond("text/plain", "is_valid:false");
            var client = new HttpClient(handler);
            var factory = new Mock<IHttpClientFactory>();
            factory.Setup(x => x.CreateClient(It.IsAny<string>()))
                .Returns(client);
            var claims = new Dictionary<string, string>
            {
                { "openid.identity", "76561199207781376" }
            };

            var provider = new SteamOpenIdProvider(factory.Object, configuration);


            Assert.Throws<ArgumentException>(() =>
            {
                provider.VerifyOwnership(claims);
            });
        }

        [Fact]
        public void VerifyOwnership_ValidClaims_ShouldReturnTrue()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "ReturnTo", "http://returnuri" },
                    { "Realm", "http://realmuri" }
                })
                .Build();

            var handler = new MockHttpMessageHandler();
            handler.When("https://steamcommunity.com/openid/login?openid.mode=check_authentication&openid.identity=76561199207781376")
                .Respond("text/plain", "is_valid:true");
            var client = new HttpClient(handler);
            var factory = new Mock<IHttpClientFactory>();
            factory.Setup(x => x.CreateClient(It.IsAny<string>()))
                .Returns(client);
            var claims = new Dictionary<string, string>
            {
                { "openid.mode", "id_res" },
                { "openid.identity", "76561199207781376" }
            };

            var provider = new SteamOpenIdProvider(factory.Object, configuration);


            Assert.True(provider.VerifyOwnership(claims));
        }

        [Fact]
        public void IdClaimName_ShouldReturnOpenidIdentity()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "ReturnTo", "http://returnuri" },
                    { "Realm", "http://realmuri" }
                })
                .Build();
            var provider = new SteamOpenIdProvider(new Mock<IHttpClientFactory>().Object, configuration);

            Assert.Equal("openid.identity", provider.IdClaimName);
        }

        [Fact]
        public void RequestUri_ShouldReturnCorrectUri()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"SteamOpenIdProvider:ReturnTo", "http://returnuri"},
                    {"SteamOpenIdProvider:Realm", "http://realmuri"}
                })
                .Build();
            var provider = new SteamOpenIdProvider(new Mock<IHttpClientFactory>().Object, configuration);

            var expected =
                "https://steamcommunity.com/openid/login?openid.ns=" + Uri.EscapeDataString("http://specs.openid.net/auth/2.0") +
                "&openid.mode=" + Uri.EscapeDataString("checkid_setup") +
                "&openid.claimed_id=" + Uri.EscapeDataString("http://specs.openid.net/auth/2.0/identifier_select") +
                "&openid.identity=" + Uri.EscapeDataString("http://specs.openid.net/auth/2.0/identifier_select") +
                "&openid.return_to=" + Uri.EscapeDataString("http://returnuri") +
                "&openid.realm=" + Uri.EscapeDataString("http://realmuri");

            Assert.Equal(expected, provider.RequestUri);
        }
    }
}
