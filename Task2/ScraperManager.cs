using System.Collections.Generic;

namespace HardwareScraper
{
    public class ScraperManager
    {
        List<Scraper> hardwareScrapers = new List<Scraper>();

        public ScraperManager()
        {
            hardwareScrapers.Add(new ScanScraper());
        }

        public void scrape(string searchString)
        {
            foreach(Scraper s in hardwareScrapers)
            {
                s.Search(searchString);
            }
                
        }
    }
}
