
using HhScanner.Console.Model;

namespace HhScanner.Console.Services.CountingStrategies
{
    internal class AverageWithConvertStrategy : ICountingStrategy
    {
        private CurrencyConverterService _convServ;

        public AverageWithConvertStrategy(CurrencyConverterService convServ)
        {
            _convServ = convServ;
        }

        public CountingResult DoWork(ICollection<SalaryData> datas)
        {
            double sum = 0;
            foreach (var item in datas)
            {
                double sumLocal = 0;
                if (item.ValueFrom.HasValue && item.ValueTo.HasValue)
                {
                    sumLocal = (item.ValueFrom.Value + item.ValueTo.Value) * 0.5;
                }
                else {
                    if (item.ValueFrom.HasValue)
                    {
                        sumLocal = item.ValueFrom.Value;
                    }
                    if (item.ValueTo.HasValue)
                    {
                        sumLocal = item.ValueTo.Value;
                    }
                }

                if (item.CurType != CurrencyType.RUB)
                {
                    sumLocal = _convServ.ConvertFrom(item.CurType, sumLocal);
                }

                sum += sumLocal;
            }

            return new CountingResult()
            {
                Name = nameof(AverageWithConvertStrategy),
                Value = sum / (double)datas.Count
            };
        }

    }
}
