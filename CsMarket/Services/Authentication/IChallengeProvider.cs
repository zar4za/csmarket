namespace CsMarket.Services.Authentication
{
    public interface IChallengeProvider
    {
        string RequestUri { get; }
        bool VerifyOwnership(Dictionary<string, string> claims);
    }
}
