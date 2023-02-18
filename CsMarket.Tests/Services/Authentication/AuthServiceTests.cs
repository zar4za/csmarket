using CsMarket.Services.Authentication;
using CsMarket.Services.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CsMarket.Tests.Services.Authentication
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
