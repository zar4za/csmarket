using CsMarket.Steam;

namespace CsMarket.Market
{
    public class User
    {
        public Guid Id { get; private set; }

        public SteamId? SteamId { get; set; }

        public string Name { get; private set; }

        public User(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
