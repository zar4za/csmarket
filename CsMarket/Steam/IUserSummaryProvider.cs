namespace CsMarket.Steam
{
    public interface IUserSummaryProvider
    {
        UserSummary GetSummary(long steamId64);
    }
}
