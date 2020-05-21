using System;

namespace HardwareScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            //ScanScraper sm = new ScanScraper();
            //CacheManager cm = new CacheManager();

            DatabaseManager.Instance.TestConnection();
            
            
            Console.Clear();
            Console.Write("Search term: ");
            string searchTerm = Console.ReadLine();





            ScraperManager scrapers = new ScraperManager();

            scrapers.scrape(searchTerm);


            //List<ResultItem> res = sm.Search(searchTerm);
            
            

            //string article = cm.Search(searchTerm);
            //if (article == null)
            //{
            //    article = sm.Search(searchTerm);
            //    cm.Save(new Search(searchTerm, article));
            //}

            Console.Clear();
            Console.WriteLine(searchTerm);
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}

