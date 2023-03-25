using CsMarket.Data.Entities;

namespace CsMarket.Data
{
    public class EFCoreRepository : IUserRepository
    {
        private readonly CsMarketContext _context;

        public EFCoreRepository(CsMarketContext context)
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
                user = _context.Users.First(x => x.SteamId32 == steamId);
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
