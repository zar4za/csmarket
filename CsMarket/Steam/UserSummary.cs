using System.Text.Json.Serialization;

namespace CsMarket.Steam
{
    public class UserSummary
    {
        [JsonPropertyName("personaname")]
        public string Name { get; init; } = string.Empty;

        [JsonPropertyName("avatar")]
        public string AvatarUri { get; init; } = string.Empty;

        [JsonPropertyName("timecreated")]
        public int RegisterTimestamp { get; init; }

        public UserSummary() { }

        [JsonConstructor]
        public UserSummary(string name, string avatarUri, int registerTimestamp)
        {
            Name = name;
            AvatarUri = avatarUri;
            RegisterTimestamp = registerTimestamp;
        }
    }
}
