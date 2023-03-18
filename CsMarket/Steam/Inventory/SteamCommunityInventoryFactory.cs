using System.Text.Json.Nodes;

namespace CsMarket.Steam.Inventory
{
    public class SteamCommunityInventoryFactory : SteamInventoryFactory
    {
        private const string InventoryEndpoint = "https://steamcommunity.com/inventory/{0}/730/2";
        private readonly HttpClient _httpClient;

        public SteamCommunityInventoryFactory(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        protected override async Task<JsonNode> FetchInventoryJson(long steamId64)
        {
            var stream = await _httpClient.GetStreamAsync(string.Format(InventoryEndpoint, steamId64));
            return JsonNode.Parse(stream)!;
        }
    }
}
