using CsMarket.Auth;
using CsMarket.Auth.Jwt;
using CsMarket.Core;
using CsMarket.Data;
using CsMarket.Steam;
using Moq;
using System.Security.Claims;

namespace CsMarket.Tests.Auth
{
    public class AuthServiceTests
    {
        //[Fact]
        //public void SignInUser_ExistingUser_ShouldReturnCorrectToken()
        //{
        //    var steamid = "https://steamcommunity.com/openid/id/76561199367508734";
        //    var format = new SteamIdFormatter(steamid);
        //    var user = new User(
        //        id: Guid.NewGuid(),
        //        steamId: format.ToSteamId32(),
        //        name: "testname",
        //        role: Role.Common);
        //    var generator = new JwtTokenGenerator(new JwtSettings()
        //    {
        //        Audience = "test",
        //        ExpirationSeconds = 600,
        //        Issuer = "test",
        //        Secret = "test_secret_password"
        //    });
        //    var claims = new Dictionary<string, string>()
        //    {
        //        { "openid.identity", steamid }
        //    };

        //    var provider = new Mock<IUserSummaryProvider>();
        //    provider.Setup(x => x.GetUserSummary(format.ToSteamId64()))
        //        .Returns(new UserSummary()
        //        {
        //            Name = user.Name,
        //            AvatarUri = "uri",
        //            RegisterTimestamp = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        //        });
            
        //    var repository = new Mock<IUserRepository>();
        //    repository.Setup(x => x.FindUser(format.ToSteamId32(), out user))
        //        .Returns(true);

        //    var challenge = new Mock<IChallengeProvider>();
        //    challenge.Setup(x => x.VerifyOwnership(claims))
        //        .Returns(true);
        //    challenge.Setup(x => x.IdClaimName)
        //        .Returns("openid.identity");

        //    var service = new AuthService(challenge.Object, generator, repository.Object, provider.Object);
        //    var expected = generator.SignToken(
        //        new ClaimsIdentity(
        //            new List<Claim>()
        //            {
        //                new Claim(ClaimType.Guid, user.Id.ToString()),
        //                new Claim(ClaimType.Name, user.Name),
        //                new Claim(ClaimType.Role, user.Role.ToString()),
        //                new Claim(ClaimType.SteamId, format.ToSteamId64().ToString())
        //            }), DateTime.UtcNow);


        //    var token = service.SignInUser(claims);
        //    Assert.Equal(expected, token);
        //}

        //[Fact]
        //public void SignInUser_NotValid_ShouldThrowException()
        //{
        //    var steamid = "https://steamcommunity.com/openid/id/76561199367508734";
        //    var format = new SteamIdFormatter(steamid);
        //    var user = new User(
        //        id: Guid.NewGuid(),
        //        steamId: format.ToSteamId32(),
        //        name: "testname",
        //        role: Role.Common);
        //    var generator = new JwtTokenGenerator(new JwtSettings()
        //    {
        //        Audience = "test",
        //        ExpirationSeconds = 600,
        //        Issuer = "test",
        //        Secret = "test_secret_password"
        //    });
        //    var claims = new Dictionary<string, string>()
        //    {
        //        { "openid.identity", steamid }
        //    };

        //    var provider = new Mock<IUserSummaryProvider>();
        //    provider.Setup(x => x.GetUserSummary(format.ToSteamId64()))
        //        .Returns(new UserSummary()
        //        {
        //            Name = user.Name,
        //            AvatarUri = "uri",
        //            RegisterTimestamp = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        //        });

        //    var repository = new Mock<IUserRepository>();
        //    repository.Setup(x => x.FindUser(format.ToSteamId32(), out user))
        //        .Returns(true);

        //    var challenge = new Mock<IChallengeProvider>();
        //    challenge.Setup(x => x.VerifyOwnership(claims))
        //        .Returns(false);

        //    var service = new AuthService(challenge.Object, generator, repository.Object, provider.Object);


        //    Assert.Throws<Exception>(() =>
        //    {
        //        var token = service.SignInUser(claims);
        //    });
        //}
    }
}
