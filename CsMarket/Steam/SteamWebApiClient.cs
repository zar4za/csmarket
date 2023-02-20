using System.Text.Json;
using System.Text.Json.Nodes;

namespace CsMarket.Steam
{
    public class SteamWebApiClient : IUserSummaryProvider
    {
        private readonly HttpClient _client;
        private readonly string _key;

        public SteamWebApiClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _client = clientFactory.CreateClient();
            _key = configuration["Steam:WebApiKey"];
        }

        public UserSummary GetUserSummary(long steamId64)
        {
            var stream = _client.GetStreamAsync($"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v2/?key={_key}&steamids={steamId64}").Result;
            var user = JsonNode.Parse(stream)?["response"]?["players"]?[0].Deserialize<UserSummary>();

            if (user == null)
                throw new JsonException("Cannot parse steam reponse.");

            return user;
        }
    }
}
