namespace CsMarket.Core
{
    public class User
    {
        public Guid Id { get; private set; }

        public int SteamId { get; private set; }

        public string Name { get; private set; }

        public Role Role { get; private set; }

        public string AvatarUri { get; init; } = null!;
        public int RegisterTimestamp { get; init; }

        public User(Guid id, int steamId, string name, Role role)
        {
            Id = id;
            SteamId = steamId;
            Name = name;
            Role = role;
        }
    }
}
