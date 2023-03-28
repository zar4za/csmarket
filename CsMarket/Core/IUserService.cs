namespace CsMarket.Core
{
    public interface IUserService
    {
        public void UpdateUser(int steamId, string? email = null, string? tradelink = null);
    }
}
