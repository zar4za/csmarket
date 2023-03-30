using CsMarket.Core;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace CsMarket.Steam.Inventory
{
    public abstract class SteamInventoryFactory : IInventoryFactory
    {
        public IEnumerable<Item> GetInventory(long steamId64)
        {
            var json = FetchInventoryJson(steamId64).Result;
            var options = new JsonSerializerOptions()
            {
                NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString
            };

            var descriptions = new Dictionary<long, Description>();
            foreach (var desc in json["descriptions"].AsArray())
            {
                descriptions.Add(long.Parse(desc["classid"].GetValue<string>()), desc.Deserialize<Description>(options));
            }

            return json["assets"]!.AsArray().Select(x =>
            {
                var classId = long.Parse(x["classid"].GetValue<string>());
                var instanceId = long.Parse(x["instanceid"].GetValue<string>());
                var description = descriptions[classId];
                return new Item()
                {
                    AssetId = long.Parse(x["assetid"].GetValue<string>()),
                    ClassId = classId,
                    InstanceId = instanceId,
                    IconUrl = description.IconUrlLarge ?? description.IconUrl,
                    MarketHashName = description.MarketHashName,
                    Rarity = description.Type[..description.Type.IndexOf(' ')].Replace("-", "").ToLower()
                };
            });
        }

        protected abstract Task<JsonNode> FetchInventoryJson(long steamId64);
    }
}
