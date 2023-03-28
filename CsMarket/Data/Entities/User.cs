using CsMarket.Core;
using System.ComponentModel.DataAnnotations;

namespace CsMarket.Data.Entities
{
    public class User
    {
        [Key]
        public int SteamId32 { get; set; }

        public long SignupUnixMilli { get; set; }

        [Required]
        public Role Role { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public string? AvatarHash { get; set; }

        public string? Email { get; set; }

        public string? TradeLink { get; set; }
    }
}
