using System.Text.Json.Serialization;

namespace CsMarket.Steam.Inventory
{
    public class Asset
    {
        [JsonPropertyName("assetid")]
        public long AssetId { get; init; }

        [JsonPropertyName("classid")]
        public long ClassId { get; init; }

        [JsonPropertyName("instanceid")]
        public long InstanceId { get; init; }
    }
}
