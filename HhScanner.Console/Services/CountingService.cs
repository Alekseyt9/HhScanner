using HhScanner.Console.Model;
using HhScanner.Console.Services.CountingStrategies;

namespace HhScanner.Console.Services
{
    public class CountingService
    {
        private ICollection<ICountingStrategy> _strats;

        public CountingService(CurrencyConverterService convServ)
        {
            _strats = new List<ICountingStrategy>()
            {
                new AverageWithConvertStrategy(convServ)
            };
        }

        public ICollection<CountingResult> DoCount(ICollection<SalaryData> datas)
        {
            var res = new List<CountingResult>();
            foreach (var strat in _strats)
            {
                res.Add(strat.DoWork(datas));
            }

            return res;
        }

    }
}
