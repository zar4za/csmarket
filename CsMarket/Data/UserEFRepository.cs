using CsMarket.Core;
using CsMarket.Steam;

namespace CsMarket.Data
{
    public class UserEFRepository : IUserRepository
    {
        private readonly UsersContext _context;

        public UserEFRepository(UsersContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User GetUser(int steamId)
        {
            return _context.Users.Single(x => x.SteamId == steamId);
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
