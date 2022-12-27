
using HhScanner.Console.Model;

namespace HhScanner
{
    public class SalaryParser
    {
        public SalaryData Parse(string val)
        {
            double resVal;
            CurrencyType cType;
            var resData = new SalaryData();

            var splitArr = val.Split('–');
            if (splitArr.Length > 1)
            {
                ParseValWithCur(splitArr[0], out resVal, out cType);
                resData.ValueFrom = resVal;

                ParseValWithCur(splitArr[1], out resVal, out cType);
                resData.ValueTo = resVal;

                if (cType != CurrencyType.None)
                {
                    resData.CurType = cType;
                }
            }
            else
            {
                if (val.StartsWith("от"))
                {
                    ParseValWithCur(val.TrimStart(new[] { 'о', 'т' }), out resVal, out cType);
                    resData.ValueFrom = resVal;
                    if (cType != CurrencyType.None)
                    {
                        resData.CurType = cType;
                    }
                }

                if (val.StartsWith("до"))
                {
                    ParseValWithCur(val.TrimStart(new[] { 'д', 'о' }), out resVal, out cType);
                    resData.ValueTo = resVal;
                    if (cType != CurrencyType.None)
                    {
                        resData.CurType = cType;
                    }
                }
            }

            return resData;
        }

        private void ParseValWithCur(string str, out double val, out CurrencyType curType)
        {
            val = 0;
            str = str.Trim();
            var splitArr = str.Split(new []{' ', ' ' });
            var lastPart = splitArr[splitArr.Length - 1];
            var lastIsCur = !IsNumeric(lastPart);
            curType = lastIsCur ? CurToNum(lastPart) : CurrencyType.None;
            var strNum = lastIsCur ? 
                string.Join("", splitArr.Take(splitArr.Length - 1)) : string.Join("", splitArr);
            double.TryParse(strNum, out val);
        }

        private CurrencyType CurToNum(string str)
        {
            switch (str)
            {
                case "руб.": return CurrencyType.RUB;
                case "USD": return CurrencyType.USD;
                case "EUR": return CurrencyType.EUR;
                default: throw new ArgumentException(str);
            }
        }

        private bool IsNumeric(string str)
        {
            int n;
            return int.TryParse(str, out n);
        }

    }

}
