using CsMarket.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsMarket.Tests.Steam
{
    public class SteamIdProviderOptionsTests
    {
        [Fact]
        public void BuildRequestQuery_WithValidProps_ShouldReturnCorrectQuery()
        {
            //Arrange
            var expected = "?openid.ns=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0" +
                "&openid.mode=checkid_setup" +
                "&openid.claimed_id=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0%2Fidentifier_select" +
                "&openid.identity=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0%2Fidentifier_select" +
                "&openid.return_to=http%3A%2F%2Flocalhost%3A8080%2Fapi%2FAuth%2Fcomplete" +
                "&openid.realm=http%3A%2F%2Flocalhost%3A8080%2F";

            var options = new SteamIdProviderOptions()
            {
                ReturnTo = "http://localhost:8080/api/Auth/complete",
                Realm = "http://localhost:8080/"
            };

            //Act
            var query = options.BuildRequestQuery();

            //Assert
            Assert.Equal(expected, query);
        }
    }
}
