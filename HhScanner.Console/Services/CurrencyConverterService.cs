
using HtmlAgilityPack;
using System.Net;
using HhScanner.Console.Model;

namespace HhScanner
{
    public class CurrencyConverterService
    {
        private string s_Url = "https://www.cbr.ru/currency_base/daily/";
        private Dictionary<CurrencyType, double> _curMap = new Dictionary<CurrencyType, double>();
        private HashSet<string> s_ScanningCurs = new HashSet<string>() { "EUR", "USD" };

        public double ConvertFrom(CurrencyType type, double valCur)
        {
            if (!_curMap.ContainsKey(type))
            {
                UpdateCurrencyRates();
            }

            return _curMap[type] * valCur;
        }

        private void UpdateCurrencyRates()
        {
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

            var url = string.Format(s_Url);
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var tableEl =
                doc.DocumentNode.SelectSingleNode("//table[@class='data']");
            var trEls = tableEl.SelectNodes(".//tr");
            foreach (var trEl in trEls)
            {
                var tds = trEl.SelectNodes(".//td");
                if (tds == null)
                    continue;
                var tdList = tds.Select(x => x.InnerText).ToList();
                var curName = tdList[1];
                if (!s_ScanningCurs.Contains(curName))
                    continue;
                var curValStr = tdList[4];
                AddCurData(curName, curValStr);
            }
        }

        private void AddCurData(string curStr, string valStr)
        {
            var curType = StrToCurType(curStr);
            double val;
            if (double.TryParse(valStr, out val))
            {
                _curMap[curType] = val;
            }
        }

        private CurrencyType StrToCurType(string val)
        {
            switch (val)
            {
                case "USD": return CurrencyType.USD;
                case "EUR": return CurrencyType.EUR;
                default: throw new ArgumentException(val);
            }
        }

    }
}
