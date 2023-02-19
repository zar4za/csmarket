namespace CsMarket.Auth
{
    public interface IChallengeProvider
    {
        string RequestUri { get; }
        bool VerifyOwnership(Dictionary<string, string> claims);
    }
}
