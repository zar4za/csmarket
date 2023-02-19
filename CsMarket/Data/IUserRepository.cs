using CsMarket.Core;
using CsMarket.Steam;

namespace CsMarket.Data
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User GetUser(SteamId steamId);
        bool FindUser(SteamId steamId, out User user);
    }
}
