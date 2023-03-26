using CsMarket.Market;
using System.ComponentModel.DataAnnotations;

namespace CsMarket.Data.Entities
{
    public class Listing
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public User Owner { get; init; } = null!;

        [Required]
        public Asset Asset { get; init; } = null!;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public ListingState State { get; set; }
    }
}
