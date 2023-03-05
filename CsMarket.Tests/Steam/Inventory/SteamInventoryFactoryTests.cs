using CsMarket.Core;
using CsMarket.Data;
using CsMarket.Data.Entity;
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
            var expectedJson = File.ReadAllText("../../../testdata/steam/inventory/SteamReturnsJsonExpected.json");

            var expectedInventory = JsonSerializer.Deserialize<List<Item>>(expectedJson);
            var handler = new MockHttpMessageHandler();
            handler.When("https://steamcommunity.com/inventory/76561198106556563/730/2")
                .Respond("application/json", json);
            var client = new HttpClient(handler);
            var clientFactory = new Mock<IHttpClientFactory>();
            clientFactory.Setup(x => x.CreateClient(It.IsAny<string>()))
                .Returns(client);

            var dictionary = new Dictionary<long, Description>();
            var repository = new Mock<IDescriptionRepository>();
            repository.Setup(x => x.AddDescription(It.IsAny<Description>()))
                .Callback<Description>((x) =>
                {
                    dictionary.Add(x.ClassId, x);
                });

            repository.Setup(x => x.GetDescription(It.IsAny<long>()))
                .Returns<long>(x => dictionary[x]);
            var factory = new SteamInventoryFactory(clientFactory.Object, repository.Object);


            var inventory = factory.GetInventory(76561198106556563);
            Assert.NotNull(inventory);
            Assert.All(inventory, x => expectedInventory.Contains(x));
        }
    }
}
