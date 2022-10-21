using HhScanner.Console.Model;

namespace HhScanner.Console.Services.CountingStrategies
{
    internal interface ICountingStrategy
    {
        CountingResult DoWork(ICollection<SalaryData> datas);
    }
}
