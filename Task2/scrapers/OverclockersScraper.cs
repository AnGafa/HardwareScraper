using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;

namespace HardwareScraper
{
    class OverclockersScraper : Scraper
    {

        public OverclockersScraper()
        {
            XPathParameters param = new XPathParameters();
            param.XPathNameParameter = "/html/body/div[4]/div/div/div/div/div[3]/div[5]/div[1]/div[1]/div/a[2]/span[2]";
            param.XPathPriceParameter = "/html/body/div[4]/div/div/div/div/div[3]/div[5]/div[1]/div[1]/div/p";
            param.XPathAvailabilityParameter = "/html/body/div[4]/div/div/div/div/div[3]/div[5]/div[1]/div[1]/div/span/p";
            this.xPathParams.Add(param);

            param = new XPathParameters();
            param.XPathNameParameter = "/html/body/div[4]/div/div/div/div/div[3]/div[5]/div[1]/div[2]/div/a[2]/span[2]";
            param.XPathPriceParameter = "/html/body/div[4]/div/div/div/div/div[3]/div[5]/div[1]/div[2]/div/p";
            param.XPathAvailabilityParameter = "/html/body/div[4]/div/div/div/div/div[3]/div[5]/div[1]/div[2]/div/span/p";
            this.xPathParams.Add(param);

            this.sourceURL = "https://www.overclockers.co.uk/";
        }

        public override List<ResultItem> Search(string searchTerm)
        {
            List<ResultItem> results = new List<ResultItem>();

            ChromeDriver client = ChromeDriverWrapper.Instance.Client;

            string url = this. sourceURL + "search?sSearch=" + searchTerm;
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
