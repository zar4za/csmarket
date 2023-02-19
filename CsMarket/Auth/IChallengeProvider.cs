namespace CsMarket.Auth
{
    public interface IChallengeProvider
    {
        string IdClaimName { get; }
        string RequestUri { get; }
        bool VerifyOwnership(Dictionary<string, string> claims);
    }
}
