namespace CsMarket.Auth.Jwt
{
    public class JwtSettings
    {
        public string Secret { get; set; } = null!;

        public int ExpirationSeconds { get; set; }

        public string Issuer { get; set; } = null!;

        public string Audience { get; set; } = null!;
    }
}