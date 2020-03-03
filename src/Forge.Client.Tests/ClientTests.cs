using System;
using System.Threading.Tasks;
using Xunit;

namespace Forge.Client.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Authenticate()
        {
            var args = TestHelper.GetClientArgs();
            var cut = new ClientWrapper(args);
            await cut.RefreshToken();

            Assert.False(string.IsNullOrEmpty(cut.Token));
        }
    }
}
