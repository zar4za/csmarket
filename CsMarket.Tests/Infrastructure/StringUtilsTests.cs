using CsMarket.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsMarket.Tests.Infrastructure
{
    public class StringUtilsTests
    {
        [Theory]
        [InlineData("\n")]
        [InlineData("\r")]
        [InlineData("\n\r")]
        [InlineData("\r\n")]
        public void StringHaveDifferentLineSeparators_ShouldParseCorrectly(string separator)
        {
            var input = $"ns:http://specs.openid.net/auth/2.0{separator}is_valid:true";
            var expected = new Dictionary<string, string>
            {
                { "ns", "http://specs.openid.net/auth/2.0" },
                { "is_valid", "true" }
            };

            var result = StringUtils.ParseColonSeparated(input);

            Assert.Equal(expected, result);
        }
    }
}
