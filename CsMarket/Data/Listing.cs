using System.ComponentModel.DataAnnotations;

namespace CsMarket.Data
{
    public class Listing
    {
        [Key]
        public long AssetId { get; init; }
        
        public long InstanceId { get; init; }

        public long ClassId { get; init; }

        public decimal Price { get; set; }
    }
}
