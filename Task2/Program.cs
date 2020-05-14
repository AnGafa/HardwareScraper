using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            ScanScraper sm = new ScanScraper();
            //CacheManager cm = new CacheManager();

            Console.Clear();
            Console.Write("Search term: ");
            string searchTerm = Console.ReadLine();


 
            string article = sm.Search(searchTerm);
            


            //string article = cm.Search(searchTerm);
            //if (article == null)
            //{
            //    article = sm.Search(searchTerm);
            //    cm.Save(new Search(searchTerm, article));
            //}

            Console.Clear();
            Console.WriteLine(searchTerm);
            Console.WriteLine();
            Console.WriteLine(article);
            Console.ReadKey();
        }
    }
}

