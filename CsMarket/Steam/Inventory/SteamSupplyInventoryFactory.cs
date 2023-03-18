using System.Text.Json.Nodes;

namespace CsMarket.Steam.Inventory
{
    public class SteamSupplyInventoryFactory : SteamInventoryFactory
    {
        private const string InventoryEndpoint = "https://steam.supply/API/{0}/loadinventory?steamid={1}&appid=730&contextid=2";
        private const int MaxRetryAttempts = 3;

        private readonly HttpClient _httpClient;
        private readonly string _apiKey;


        public SteamSupplyInventoryFactory(IHttpClientFactory factory, IConfiguration configuration)
        {
            _httpClient = factory.CreateClient();
            _apiKey = configuration["Steam:SupplyKey"];
        }

        protected override async Task<JsonNode> FetchInventoryJson(long steamId64)
        {
            for (var i = 0; i < MaxRetryAttempts; i++)
            {
                var stream = await _httpClient.GetStreamAsync(string.Format(InventoryEndpoint, _apiKey, steamId64));
                var json = JsonNode.Parse(stream);

                if (json != null && json["fake_redirect"] == null)
                {
                    return json;
                }
            }

            throw new Exception($"Cannot fetch inventory after {MaxRetryAttempts} attempts.");
        }
    }
}
