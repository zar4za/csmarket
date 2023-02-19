using CsMarket.Steam;

namespace CsMarket.Core
{
    public class User
    {
        public Guid Id { get; private set; }

        public SteamId SteamId { get; set; }

        public string Name { get; private set; }

        public Role Role { get; private set; }

        public User(Guid id, string name, Role role)
        {
            Id = id;
            Name = name;
            Role = role;
        }
    }
}
