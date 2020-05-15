using OpenQA.Selenium;
using System.Collections.Generic;

namespace HardwareScraper
{
    class GamersScraper : Scraper
    {

        public GamersScraper()
        {
            XPathParameters param = new XPathParameters();
            param.XPathNameParameter = "//*[@id='main']/ul/li[1]/div/div/div[1]/a[2]/h2";
            param.XPathPriceParameter = "//*[@id='main']/ul/li[1]/div/div/div[3]/div[1]/span/span/span";
            param.XPathAvailabilityParameter = "";
            this.xPathParams.Add(param);

            param = new XPathParameters();
            param.XPathNameParameter = "//*[@id='main']/ul/li[2]/div/div/div[1]/a[2]/h2";
            param.XPathPriceParameter = "//*[@id='main']/ul/li[2]/div/div/div[3]/div[1]/span/span/span";
            param.XPathAvailabilityParameter = "";
            this.xPathParams.Add(param);

        }

        public override List<ResultItem> Search(string searchTerm)
        {
            List<ResultItem> results = new List<ResultItem>();

            string url = "https://gamerslounge.mt/?s=" + searchTerm + "&post_type=product&dgwt_wcas=1";
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
