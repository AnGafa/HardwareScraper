using System.Collections.Generic;
using System.Linq;

namespace HardwareScraper
{
    public class ScraperManager
    {
        List<Scraper> hardwareScrapers = new List<Scraper>();

        public ScraperManager()
        {
            hardwareScrapers.Add(new ScanScraper());
            hardwareScrapers.Add(new KlikkScraper());
            hardwareScrapers.Add(new GamersScraper());
            hardwareScrapers.Add(new OverclockersScraper());
        }

        public List<ResultItem> scrape(string searchString)
        {

            List<ResultItem> results = new List<ResultItem>();

            foreach (Scraper s in hardwareScrapers)
            {
                results.AddRange(s.Search(searchString));
            }

            ChromeDriverWrapper.Instance.Client.Close();

            return results;

        }
    }
}
