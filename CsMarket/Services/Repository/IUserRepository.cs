using CsMarket.Models.Core;
using CsMarket.Steam;

namespace CsMarket.Services.Repository
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User GetUser(SteamId steamId);
        bool FindUser(SteamId steamId, out User user);
    }
}
