namespace CsMarket.Steam
{
    public interface IUserSummaryProvider
    {
        UserSummary GetUserSummary(long steamId64);
    }
}
