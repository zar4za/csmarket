using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CsMarket.Data.Entity
{
    public class Description
    {
        [Key]
        [JsonPropertyName("classid")]
        public long ClassId { get; init; }

        [JsonPropertyName("market_hash_name")]
        public string MarketHashName { get; init; } = string.Empty;

        [JsonPropertyName("icon_url_large")]
        public string IconUrl { get; init; } = string.Empty;
    }
}
