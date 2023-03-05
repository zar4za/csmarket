using CsMarket.Core;

namespace CsMarket.Data
{
    public class UserEFRepository : IUserRepository
    {
        private readonly MarketContext _context;

        public UserEFRepository(MarketContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public bool FindUser(int steamId, out User user)
        {
            try
            {
                user = _context.Users.First(x => x.SteamId == steamId);
                return true;
            }
            catch
            {
                user = null!;
                return false;
            }
        }
    }
}
