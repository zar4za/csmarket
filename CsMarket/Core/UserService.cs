using CsMarket.Data;
using CsMarket.Data.Entities;

namespace CsMarket.Core
{
    public class UserService : IUserService
    {
        private readonly MarketContext _identityContext;

        public UserService(MarketContext context)
        {
            _identityContext = context;
        }

        public void UpdateUser(int steamId, string? email = null, string? tradelink = null)
        {
            var user = _identityContext.Users
                .Where(x => x.SteamId32 == steamId)
                .Single();

            if (email != null)
                user.Email = email;
            if (tradelink != null)
                user.TradeLink = tradelink;
        }
    }
}
