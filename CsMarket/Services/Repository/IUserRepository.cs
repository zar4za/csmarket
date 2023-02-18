using CsMarket.Models.Core;
using CsMarket.Steam;

namespace CsMarket.Services.Repository
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User FindUser(SteamId steamId);
    }
}
