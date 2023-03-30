using System.ComponentModel.DataAnnotations;

namespace CsMarket.Data.Entities
{
    public class Asset
    {
        [Key]
        public long AssetId { get; init; }

        public User Owner { get; init; } = null!;

        public AssetClass Class { get; init; } = null!;

        public bool WasTraded { get; set; }

        public DateTime? LastUpdate { get; set; }
    }
}
