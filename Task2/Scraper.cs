using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.IO;


namespace HardwareScraper
{
    public abstract class Scraper
	{
        protected List<string> xPathName = new List<string>();

        protected List<string> xPathPrice = new List<string>();

        protected List<string> xPathAvailability = new List<string>();

        protected const int searchItems = 2;

        protected List<XPathParameters> xPathParams = new List<XPathParameters>();

        protected string sourceURL = string.Empty; 


        public Scraper()
        {
        }

        abstract public List<ResultItem> Search(string searchTerm);
    }
}