

using HhScanner.Console.Model;
using Xunit;

namespace HhScanner.Tests
{
    public class CurrencyConverterServiceTest
    {
        [Fact]
        public void Test()
        {
            var res = true;
            var serv = new CurrencyConverterService();
            try
            {
                var val = serv.ConvertFrom(CurrencyType.USD, 10);
            }
            catch (Exception)
            {
                res = false;
            }
            Assert.True(res);
        }

    }
}
