using System.ComponentModel.DataAnnotations;

namespace CsMarket.Data.Entities
{
    public class Asset
    {
        [Key]
        public long AssetId { get; init; }

        [Required]
        public AssetClass ClassName { get; init; } = null!;
    }
}
