using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.IO;


namespace HardwareScraper
{
    public abstract class Scraper
	{
        protected ChromeDriver client;

        protected List<string> xPathName = new List<string>();

        protected List<string> xPathPrice = new List<string>();

        protected List<string> xPathAvailability = new List<string>();

        protected const int searchItems = 2;

        protected List<XPathParameters> xPathParams = new List<XPathParameters>();

        public Scraper()
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(Directory.GetCurrentDirectory());
            service.SuppressInitialDiagnosticInformation = true;
            service.HideCommandPromptWindow = true;
            ChromeOptions options = new ChromeOptions();
            //options.AddArgument("--headless");
            client = new ChromeDriver(service, options);
        }

        abstract public List<ResultItem> Search(string searchTerm);
    }
}