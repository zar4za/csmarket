using CsMarket.Data.Entities;

namespace CsMarket.Data
{
    public interface IUserRepository
    {
        void AddUser(User user);
        bool FindUser(int steamId, out User user);
    }
}
