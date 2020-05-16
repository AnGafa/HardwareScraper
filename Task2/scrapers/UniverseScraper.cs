using OpenQA.Selenium;
using System.Collections.Generic;
using OpenQA.Selenium.Chrome;


namespace HardwareScraper
{
    class UniverseScraper : Scraper
    {

        public UniverseScraper()
        {
            XPathParameters param = new XPathParameters();
            param.XPathNameParameter = "/html/body/div[7]/div[4]/div/div/div[2]/div[9]/div[1]/div[1]/div[2]/div[1]/a";
            param.XPathPriceParameter = "/html/body/div[7]/div[4]/div/div/div[2]/div[9]/div[1]/div[2]/div[1]/div[2]";
            param.XPathAvailabilityParameter = "/html/body/div[7]/div[4]/div/div/div[2]/div[9]/div[1]/div[2]/div[1]/div[5]";
            this.xPathParams.Add(param);

            param = new XPathParameters();
            param.XPathNameParameter = "/html/body/div[7]/div[4]/div/div/div[2]/div[9]/div[2]/div[1]/div[2]/div[1]/a";
            param.XPathPriceParameter = "/html/body/div[7]/div[4]/div/div/div[2]/div[9]/div[2]/div[2]/div[1]/div[2]";
            param.XPathAvailabilityParameter = "/html/body/div[7]/div[4]/div/div/div[2]/div[9]/div[2]/div[2]/div[1]/div[5]";
            this.xPathParams.Add(param);

        }

        public override List<ResultItem> Search(string searchTerm)
        {
            ChromeDriver client = ChromeDriverWrapper.Instance.Client;


            List<ResultItem> results = new List<ResultItem>();

            string url = "https://www.computeruniverse.net/en/search?query=" + searchTerm;
            client.Navigate().GoToUrl(url);

            System.Threading.Thread.Sleep(3000);

            foreach (XPathParameters param in this.xPathParams)
            {
                // start loop
                ResultItem result = new ResultItem();

                IWebElement resultNameElement = client.FindElement(By.XPath(param.XPathNameParameter));
                result.ProductName = resultNameElement.Text;

                IWebElement resultPriceElement = client.FindElement(By.XPath(param.XPathPriceParameter));
                result.Price = resultPriceElement.Text;

                IWebElement resultAvailabilityElement = client.FindElement(By.XPath(param.XPathAvailabilityParameter));
                result.Availability = resultAvailabilityElement.Text;

                results.Add(result);
            }

            return results;
        }

    }
}
