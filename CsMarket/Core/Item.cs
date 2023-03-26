using System.Text.Json.Serialization;

namespace CsMarket.Core
{
    public class Item
    {
        [JsonPropertyName("assetid")]
        public long AssetId { get; init; }

        [JsonPropertyName("classid")]
        public long ClassId { get; init; }

        [JsonPropertyName("instanceid")]
        public long InstanceId { get; init; }

        [JsonPropertyName("market_hash_name")]
        public string MarketHashName { get; init; } = string.Empty;

        [JsonPropertyName("icon_url")]
        public string IconUrl { get; init; } = string.Empty;
    }
}
