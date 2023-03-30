using System.Text.Json.Serialization;

namespace CsMarket.Steam.Inventory
{
    public class Description
    {
        [JsonPropertyName("classid")]
        public long ClassId { get; init; }

        [JsonPropertyName("instanceid")]
        public long InstanceId { get; init; }

        [JsonPropertyName("market_hash_name")]
        public string MarketHashName { get; init; } = string.Empty;

        [JsonPropertyName("icon_url")]
        public string IconUrl { get; set; } = string.Empty;

        [JsonPropertyName("icon_url_large")]
        public string? IconUrlLarge { get; init; }

        [JsonPropertyName("type")]
        public string Type { get; init; } = string.Empty;
    }
}
