
using HhScanner.Console.Model;
using HhScanner.Console.Services;
using Xunit;

namespace HhScanner.Tests
{
    public class CountingServiceTest
    {
        [Fact]
        public void Test()
        {
            var convServ = new CurrencyConverterService();
            var countServ = new CountingService(convServ);
            var res = countServ.DoCount(new List<SalaryData>()
            {
                new SalaryData()
                {
                    CurType = CurrencyType.RUB,
                    ValueFrom = 100
                },
                new SalaryData()
                {
                    CurType = CurrencyType.RUB,
                    ValueFrom = 100,
                    ValueTo = 200
                },
                new SalaryData()
                {
                    CurType = CurrencyType.EUR,
                    ValueFrom = 5
                },
            });
            Assert.Equal(res.ToList()[0].Value, 183.51433333333333);
        }

    }

}
