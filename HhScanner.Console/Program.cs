

using HhScanner.Console.Services;

namespace HhScanner
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var parser = new SalaryParser();
            var scanner = new HhScanner(parser);
            var salData = await scanner.Scan();

            var curConv = new CurrencyConverterService();
            var cManager = new CountingService(curConv);
            var res = cManager.DoCount(salData);

            //var curServ = new CurrencyConverterService();
            //curServ.UpdateCurrencyRates();

        }
    }
}