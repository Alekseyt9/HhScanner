

using HhScanner.Console;
using HtmlAgilityPack;

namespace HhScanner
{
    internal class HhScanner
    {
        private const string s_UrlPattern = @"https://hh.ru/search/vacancy?text={0}&from=suggest_post&salary=&clusters=true&area=1&ored_clusters=true&enable_snippets=true&page={1}&hhtmFrom=vacancy_search_list";
        private const float s_DelayTime = 0.5f;

        private SalaryParser _salaryParser;

        public HhScanner(SalaryParser salaryParser)
        {
            _salaryParser = salaryParser;
        }

        public async Task Scan()
        {
            var text =
                "%D1%81%D1%82%D0%B0%D1%80%D1%88%D0%B8+%D1%80%D0%B0%D0%B7%D1%80%D0%B0%D0%B1%D0%BE%D1%82%D1%87%D0%B8%D0%BA";
            var page = 0;
            var url = string.Format(s_UrlPattern, text, page.ToString());
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var list = new List<string>();

            var nodesEl = 
                doc.DocumentNode.SelectNodes("//div[@class='vacancy-serp-item__layout']");
            foreach (var nodeEl in nodesEl)
            {
                var titleEl = nodeEl.SelectSingleNode(".//a[@class='serp-item__title']");
                var titleStr = titleEl.InnerText;

                var salaryEl = nodeEl.SelectSingleNode(".//span[@data-qa='vacancy-serp__vacancy-compensation']");
                if (salaryEl != null)
                {
                    var salaryStr = salaryEl.InnerText;
                    var salData = _salaryParser.Parse(salaryStr);
                    list.Add(salaryStr);
                }
            }
        }

        private async Task ScanPage()
        {
            await Task.Delay(TimeSpan.FromSeconds(s_DelayTime));
        }

    }

}
