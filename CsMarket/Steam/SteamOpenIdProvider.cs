using CsMarket.Auth;
using CsMarket.Infrastructure;
using System.Text;

namespace CsMarket.Steam
{
    public class SteamOpenIdProvider : IChallengeProvider
    {
        private const string Provider = "https://steamcommunity.com/openid/login";
        private const string SteamIdClaimName = "openid.identity";

        private readonly HttpClient _httpClient;
        private readonly SteamIdProviderOptions _options;

        public SteamOpenIdProvider(IHttpClientFactory factory, IConfiguration configuration)
        {
            _httpClient = factory.CreateClient();
            _options = configuration.GetSection(nameof(SteamOpenIdProvider)).Get<SteamIdProviderOptions>();
        }

        public string RequestUri => Provider + _options.BuildRequestQuery();

        public string IdClaimName => SteamIdClaimName;

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
