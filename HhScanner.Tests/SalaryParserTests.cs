
using DeepEqual.Syntax;
using HhScanner.Console.Model;
using Xunit;

namespace HhScanner.Tests
{
    public class SalaryParserTests
    {
        [Theory]
        [MemberData(nameof(TestData))]
        public void ParseTest(string source, SalaryData target)
        {
            var curParser = new SalaryParser();
            var parsed = curParser.Parse(source);
            Assert.True(parsed.IsDeepEqual(target));
        }

        public static IEnumerable<object[]> TestData()
        {
            return new List<object[]>()
            {
                new object[]{"от 50 000 руб.", 
                    new SalaryData() { CurType = CurrencyType.RUB, ValueFrom = 50000, ValueTo = null } },
                new object[]{"от 6 500 USD",
                    new SalaryData() { CurType = CurrencyType.USD, ValueFrom = 6500, ValueTo = null } },
                new object[]{"до 300 000 руб.",
                    new SalaryData() { CurType = CurrencyType.RUB, ValueFrom = null, ValueTo = 300000 } },
                new object[]{"250 000 – 300 000 руб.",
                    new SalaryData() { CurType = CurrencyType.RUB, ValueFrom = 250000, ValueTo = 300000 } },
                new object[]{"4 000 – 7 000 USD",
                    new SalaryData() { CurType = CurrencyType.USD, ValueFrom = 4000, ValueTo = 7000 } },
            };
        }

    }
}
