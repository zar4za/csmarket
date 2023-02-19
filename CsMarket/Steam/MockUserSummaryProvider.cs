namespace CsMarket.Steam
{
    public class MockUserSummaryProvider : IUserSummaryProvider
    {
        public UserSummary GetSummary(long steamId64)
        {
            return new UserSummary
            {
                Name = "zar4za",
                AvatarUri = "uri",
                RegisterTimestamp = 100000
            };
        }
    }
}
