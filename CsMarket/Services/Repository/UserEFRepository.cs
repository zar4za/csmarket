using CsMarket.Models.Core;
using CsMarket.Repository;
using CsMarket.Steam;

namespace CsMarket.Services.Repository
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

        public User GetUser(SteamId steamId)
        {
            return _context.Users.Single(x => x.SteamId.SteamId32 == steamId.SteamId32);
        }

        public bool FindUser(SteamId steamId, out User user)
        {
            try
            {
                user = _context.Users.First(x => x.SteamId.SteamId32 == steamId.SteamId32);
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
