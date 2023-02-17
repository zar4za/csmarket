using CsMarket.Steam;
using System.ComponentModel.DataAnnotations;

namespace CsMarket.Market
{
    public class User
    {
        [Key]
        public Guid Id { get; private set; }

        public SteamId SteamId { get; private set; }

        public string Name { get; private set; }

        public User(Guid id, SteamId steamId, string name)
        {
            Id = id;
            SteamId = steamId;
            Name = name;
        }
    }
}
