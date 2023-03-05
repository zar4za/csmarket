using CsMarket.Core;
using CsMarket.Market;
using CsMarket.Steam.Inventory;
using Moq;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CsMarket.Tests.Steam.Inventory
{
    public class SteamInventoryFactoryTests
    {
        [Fact]
        public void GetInventory_SteamReturnsJson_ShouldReturnInventory()
        {
            var json = File.ReadAllText("../../../testdata/steam/inventory/SteamReturnsJson.json");
            Assert.NotNull(json);
            var expectedJson = File.ReadAllText("../../../testdata/steam/inventory/SteamReturnsJsonExpected.json");
            Assert.NotNull(expectedJson);
            var expectedInventory = JsonSerializer.Deserialize<List<Item>>(expectedJson);
            var handler = new MockHttpMessageHandler();
            handler.When("https://steamcommunity.com/inventory/76561198106556563/730/2")
                .Respond("application/json", json);
            var client = new HttpClient(handler);
            var clientFactory = new Mock<IHttpClientFactory>();
            clientFactory.Setup(x => x.CreateClient(It.IsAny<string>()))
                .Returns(client);
            var factory = new SteamInventoryFactory(clientFactory.Object, new DictionaryDescriptionStorage());


            var inventory = factory.GetInventory(76561198106556563);
            Assert.NotNull(inventory);
            Assert.All(inventory, x => expectedInventory.Contains(x));
        }
    }
}
