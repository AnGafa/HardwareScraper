using OpenQA.Selenium;
using System.Collections.Generic;

namespace HardwareScraper
{
    class OverclockersScraper : Scraper
    {

        public OverclockersScraper()
        {
            XPathParameters param = new XPathParameters();
            param.XPathNameParameter = "//*[@id='listing - 4col']/div[1]/div/a[2]/span[2]";
            param.XPathPriceParameter = "//*[@id='listing - 4col']/div[1]/div/p";   //to test -- only parent class specified -- 'WAS' span before 'Current' -- [2]?
            param.XPathAvailabilityParameter = "//*[@id='listing - 4col']/div[1]/div/span/p";
            this.xPathParams.Add(param);

            param = new XPathParameters();
            param.XPathNameParameter = "//*[@id='listing - 4col']/div[2]/div/a[2]/span[2]";
            param.XPathPriceParameter = "//*[@id='listing - 4col']/div[2]/div/p/span";  //only current price
            param.XPathAvailabilityParameter = "//*[@id='listing - 4col']/div[2]/div/span/p";
            this.xPathParams.Add(param);

        }

        public override List<ResultItem> Search(string searchTerm)
        {
            List<ResultItem> results = new List<ResultItem>();

            string url = "https://www.overclockers.co.uk/search?sSearch=" + searchTerm;
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
