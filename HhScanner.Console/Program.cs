

using System.Globalization;
using HhScanner.Console.Services;

namespace HhScanner
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var parser = new SalaryParser();
            var scanner = new HhScanner(parser);

            System.Console.WriteLine($"start scanning hh");
            var salData = await scanner.Scan();

            var curConv = new CurrencyConverterService();
            var cManager = new CountingService(curConv);

            System.Console.WriteLine($"calculating..");
            var res = cManager.DoCount(salData);

            System.Console.WriteLine($"salary: {res.First().Value.ToString("f2", CultureInfo.InvariantCulture)}");
            System.Console.ReadLine();
        }
    }
}