using CsMarket.Core;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace CsMarket.Steam.Inventory
{
    public class SteamInventoryFactory : IInventoryFactory
    {
        private const string InventoryEndpoint = "https://steamcommunity.com/inventory/{0}/730/2";
        private readonly HttpClient _httpClient;

        public SteamInventoryFactory(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        public IEnumerable<Item> GetInventory(long steamId64)
        {
            var response = _httpClient.GetStreamAsync(string.Format(InventoryEndpoint, steamId64)).Result;
            var json = JsonNode.Parse(response);
            var options = new JsonSerializerOptions()
            {
                NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString
            };

            var descriptions = new Dictionary<(long, long), Description>();
            foreach (var desc in json["descriptions"].AsArray())
            {
                descriptions.Add((desc["instanceid"].GetValue<long>(), desc["classid"].GetValue<long>()), desc.Deserialize<Description>(options));
            }

            return json["assets"]!.AsArray().Select(x =>
            {
                var classId = x["classid"].GetValue<long>();
                var instanceId = x["instanceid"].GetValue<long>();
                var description = descriptions[(instanceId, classId)];
                return new Item()
                {
                    AssetId = x["assetid"].GetValue<long>(),
                    ClassId = classId,
                    InstanceId = instanceId,
                    IconUrl = description.IconUrl,
                    MarketHashName = description.MarketHashName
                };
            });
        }
    }
}
