using CsMarket.Core;
using CsMarket.Steam;

namespace CsMarket.Data
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User GetUser(int steamId);
        bool FindUser(int steamId, out User user);
    }
}
