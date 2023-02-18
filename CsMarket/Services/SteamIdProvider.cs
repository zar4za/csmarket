using CsMarket.Infrastructure;
using CsMarket.Models.Steam;
using CsMarket.Services.Authentication;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace CsMarket.Services
{
    public class SteamIdProvider : IChallengeProvider
    {
        private const string Provider = "https://steamcommunity.com/openid/login";

        private readonly HttpClient _httpClient;
        private readonly SteamIdProviderOptions _options;

        public SteamIdProvider(IHttpClientFactory factory, IConfiguration configuration)
        {
            _httpClient = factory.CreateClient();
            _options = configuration.GetSection(nameof(SteamIdProvider)).Get<SteamIdProviderOptions>();
        }

        public string RequestUri => Provider + _options.BuildRequestQuery();

        public bool VerifyOwnership(Dictionary<string, string> openIdClaims)
        {
            if (!openIdClaims.ContainsKey("openid.mode"))
                throw new ArgumentException("Claims must have openid.mode", nameof(openIdClaims));

            openIdClaims["openid.mode"] = "check_authentication";

            var builder = new StringBuilder(Provider);
            builder.Append('?');

            foreach (var claim in openIdClaims)
            {
                builder.Append(claim.Key);
                builder.Append('=');
                builder.Append(Uri.EscapeDataString(claim.Value));
                builder.Append('&');
            }

            builder.Remove(builder.Length - 1, 1);

            var uri = builder.ToString();
            var response = _httpClient.GetStringAsync(uri).Result;
            var isValid = Convert.ToBoolean(
                StringUtils.ParseColonSeparated(response)["is_valid"]);

            return isValid; 
        }
    }
}
