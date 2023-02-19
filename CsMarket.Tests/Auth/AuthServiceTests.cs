using CsMarket.Auth;
using CsMarket.Data;
using Moq;

namespace CsMarket.Tests.Auth
{
    public class AuthServiceTests
    {
        [Fact]
        public void CreateUser_ShouldReturnClaims()
        {
            var repository = new Mock<IUserRepository>();
            var service = new AuthService(repository.Object);
        }
    }
}
