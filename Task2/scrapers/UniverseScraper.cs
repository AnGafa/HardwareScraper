using OpenQA.Selenium;
using System.Collections.Generic;

namespace HardwareScraper
{
    class UniverseScraper : Scraper
    {

        public UniverseScraper()
        {
            XPathParameters param = new XPathParameters();
            param.XPathNameParameter = "//*[@id='npacc - catalog']/div[2]/div[9]/div[1]/div[1]/div[2]/div[1]/a";
            param.XPathPriceParameter = "//*[@id='npacc - catalog']/div[2]/div[9]/div[1]/div[2]/div[1]/div[2]";
            param.XPathAvailabilityParameter = "//*[@id='npacc - catalog']/div[2]/div[9]/div[1]/div[2]/div[1]/div[5]";
            this.xPathParams.Add(param);

            param = new XPathParameters();
            param.XPathNameParameter = "//*[@id='npacc - catalog']/div[2]/div[9]/div[2]/div[1]/div[2]/div[1]/a";
            param.XPathPriceParameter = "//*[@id='npacc - catalog']/div[2]/div[9]/div[2]/div[2]/div[1]/div[2]";
            param.XPathAvailabilityParameter = "//*[@id='npacc - catalog']/div[2]/div[9]/div[2]/div[2]/div[1]/div[5]";
            this.xPathParams.Add(param);

        }

        public override List<ResultItem> Search(string searchTerm)
        {
            List<ResultItem> results = new List<ResultItem>();

            string url = "https://www.computeruniverse.net/en/search?query=" + searchTerm;
            client.Navigate().GoToUrl(url);

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
