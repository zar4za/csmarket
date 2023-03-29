using System.ComponentModel.DataAnnotations;

namespace CsMarket.Data.Entities
{
    public class Asset
    {
        [Key]
        public long AssetId { get; init; }

        [Required]
        public User Owner { get; init; } = null!;

        [Required]
        public AssetClass ClassName { get; init; } = null!;

        public DateTime? LastUpdate { get; set; }
    }
}
