using CsMarket.Data;
using CsMarket.Data.Entities;

namespace CsMarket.Core
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public void UpdateUser(int steamId, string? email = null, string? tradelink = null)
        {
            if (!_repository.FindUser(steamId, out User user))
                throw new NotImplementedException();

            if (email != null)
                user.Email = email;
            if (tradelink != null)
                user.TradeLink = tradelink;
        }
    }
}
