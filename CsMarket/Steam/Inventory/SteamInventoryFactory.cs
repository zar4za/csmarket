using CsMarket.Core;
using CsMarket.Data.Entity;
using CsMarket.Market;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace CsMarket.Steam.Inventory
{
    public class SteamInventoryFactory : IInventoryFactory
    {
        private const string InventoryEndpoint = "https://steamcommunity.com/inventory/{0}/730/2";
        private readonly HttpClient _httpClient;
        private readonly IDescriptionStorage _descriptionStorage;

        public SteamInventoryFactory(IHttpClientFactory factory, IDescriptionStorage storage)
        {
            _httpClient = factory.CreateClient();
            _descriptionStorage = storage;
        }

        public IEnumerable<Item> GetInventory(long steamId64)
        {
            var response = _httpClient.GetStreamAsync(string.Format(InventoryEndpoint, steamId64)).Result;
            var json = JsonNode.Parse(response);
            var options = new JsonSerializerOptions()
            {
                NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString
            };

            foreach (var desc in json["descriptions"].AsArray())
            {
                _descriptionStorage.AddDescription(
                    description: desc.Deserialize<Description>(options));
            }

            return json["assets"]!.AsArray().Select(x =>
            {
                var asset = x.Deserialize<Asset>(options);
                var description = _descriptionStorage.GetDescription(asset.InstanceId, asset.ClassId);

                return new Item()
                {
                    AssetId = asset.AssetId,
                    ClassId = asset.ClassId,
                    InstanceId = asset.InstanceId,
                    MarketHashName = description.MarketHashName,
                    IconUrl = description.IconUrl
                };
            });
        }
    }
}
