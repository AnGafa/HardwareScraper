using System;
using System.Collections.Generic;

namespace HardwareScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            //ScanScraper sm = new ScanScraper();
            //CacheManager cm = new CacheManager();

            DatabaseManager.Instance.TestConnection();
            ScraperManager sm = new ScraperManager();
            
         
            //Console.Clear();
            Console.Write("Search term: ");
            string searchTerm = Console.ReadLine();

            Int32 id = DatabaseManager.Instance.GetSearchInfoIDByKeyword(searchTerm);
            Console.WriteLine(id);

            if(id==0)
            {
                // goto to internets and serach
                List<ResultItem> results = sm.scrape(searchTerm);
                int searchId = DatabaseManager.Instance.InsertNewSearch(searchTerm);
                DatabaseManager.Instance.InsertNewSearchResults(searchId, results);
            }
            else
            {
                // get timestamp
                DateTime timestamp = DatabaseManager.Instance.GetTimestampByID(id);
                Console.WriteLine(timestamp);

                if (timestamp.AddMinutes(2) > DateTime.Now)
                {
                    Console.WriteLine("can use cache"); 
                }
                else
                {
                    Console.WriteLine("cache limit exceeded");
                    DatabaseManager.Instance.DeleteSearch(id);

                    List<ResultItem> results = sm.scrape(searchTerm);
                    int searchId = DatabaseManager.Instance.InsertNewSearch(searchTerm);
                    DatabaseManager.Instance.InsertNewSearchResults(searchId, results);


                }

                // if timestamp exceeded cache limit re-seach

                // else get all items from database
            }



            //ScraperManager scrapers = new ScraperManager();

            //scrapers.scrape(searchTerm);


            //List<ResultItem> res = sm.Search(searchTerm);



            //string article = cm.Search(searchTerm);
            //if (article == null)
            //{
            //    article = sm.Search(searchTerm);
            //    cm.Save(new Search(searchTerm, article));
            //}

            //Console.Clear();
            //Console.WriteLine(searchTerm);
            //Console.WriteLine();
            Console.ReadKey();
        }
    }
}

