using System.ComponentModel.DataAnnotations;

namespace CsMarket.Data.Entities
{
    public class AssetClass
    {
        [Key]
        public long ClassId { get; set; }

        [Required]
        public string MarketHashName { get; set; } = null!;

        [Required]
        public string IconUrl { get; init; } = null!;

        [Required]
        public string Rarity { get; set; } = null!;
    }
}
